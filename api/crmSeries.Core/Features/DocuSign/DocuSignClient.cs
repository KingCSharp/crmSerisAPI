using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using crmSeries.Core.Extensions;
using DocuSign.eSign.Api;
using DocuSign.eSign.Client;
using DocuSign.eSign.Client.Auth;
using DocuSign.eSign.Model;
using Microsoft.Extensions.Configuration;

namespace crmSeries.Core.Features.DocuSign
{
    public class DocuSignClient : IDocuSignClient
    {
        private const int EarlyRefreshSeconds = 5 * 60;

        private readonly IConfiguration _configuration;
        private readonly DocuSignContext _docuSignContext;
        private readonly DocuSignClientAccessor _docuSignClient;

        public DocuSignClient(IConfiguration configuration,
            DocuSignContext docuSignContext,
            DocuSignClientAccessor docuSignClient)
        {
            _configuration = configuration;
            _docuSignContext = docuSignContext;
            _docuSignClient = docuSignClient;
        }

        public async Task<List<DocuSignTemplate>> GetTemplates()
        {
            if (!Authenticate())
                return new List<DocuSignTemplate>();

            var templateApi = new TemplatesApi(_docuSignClient.Client.Configuration);

            var templates = await templateApi.ListTemplatesAsync(_docuSignContext.Account.AccountId);

            return templates.EnvelopeTemplates.MapTo<List<DocuSignTemplate>>();
        }

        public async Task SendTemplate(string templateId, IEnumerable<DocuSignTemplateRecipient> recipients)
        {
            if (!Authenticate())
                return;

            var roles = recipients
                .Select((role, i) =>
                {
                    var templateRole = role.MapTo<TemplateRole>();
                    templateRole.RoutingOrder = $"{i + 1}";
                    return templateRole;
                })
                .ToList();

            var envelope = new EnvelopeDefinition(
                TemplateId: templateId, 
                TemplateRoles: roles,
                Status: DocuSignStatus.Sent);

            var envelopeApi = new EnvelopesApi(_docuSignClient.Client.Configuration);

            var result = await envelopeApi
                .CreateEnvelopeAsync(_docuSignContext.Account.AccountId, envelope)
                .ConfigureAwait(false);

            return;
        }
        
        private bool Authenticate()
        {
            if (_docuSignContext.AccessToken != null &&
                DateTime.UtcNow < _docuSignContext.AccessToken.Expiration.AddSeconds(-EarlyRefreshSeconds))
                return true;

            OAuth.OAuthToken authToken = null;

            authToken = _docuSignClient.Client.RequestJWTUserToken(
                    _configuration["DocuSign:ClientId"],
                    _configuration["DocuSign:UserGuid"],
                    _configuration["DocuSign:AuthServer"],
                    Encoding.UTF8.GetBytes(_configuration["DocuSign:PrivateKey"]),
                    1,
                    new List<string> { "signature", "impersonation" });
              
            _docuSignContext.AccessToken = new DocuSignToken
            {
                Token = authToken.access_token,
                Expiration = authToken.expires_in.HasValue
                    ? DateTime.UtcNow.AddSeconds(authToken.expires_in.Value)
                    : DateTime.UtcNow.AddSeconds(EarlyRefreshSeconds)
            };
            
            if (!GetAccountInfo())
                return false;
            
            _docuSignClient.Client = new ApiClient($"{_docuSignContext.Account.BaseUri}/restapi");

            return true;
        }

        private bool GetAccountInfo()
        {
            if (_docuSignContext.Account != null)
                return true;

            if (string.IsNullOrEmpty(_docuSignContext.AccessToken.Token))
                return false;

            var userInfo = _docuSignClient.Client.GetUserInfo(_docuSignContext.AccessToken.Token);
            if (userInfo == null)
                return false;

            _docuSignContext.Account = 
                !string.IsNullOrEmpty(_configuration["DocuSign:TargetUserId"])
                    ? userInfo.Accounts.FirstOrDefault(x => x.AccountId == _configuration["DocuSign:TargetUserId"])
                        ?? throw new Exception($"Could not access account '{_configuration["DocuSign:TargetUserId"]}'")
                    : userInfo.Accounts.FirstOrDefault(x => x.IsDefault == "true");

            return _docuSignContext.Account != null;
        }
    }

    public class DocuSignModelProfiles : Profile
    {
        public DocuSignModelProfiles()
        {
            CreateMap<DocuSignTemplateRecipient, TemplateRole>()
                .ForMember(x => x.RoleName, opt => opt.MapFrom(x => x.Role))
                .MapMember(x => x.Name)
                .MapMember(x => x.Email)
                .IgnoreRest();

            CreateMap<EnvelopeTemplateResult, DocuSignTemplate>()
                .MapMember(x => x.TemplateId)
                .MapMember(x => x.Name)
                .MapMember(x => x.Description)
                .MapMember(x => x.FolderId)
                .MapMember(x => x.FolderName)
                .ForMember(x => x.CreatedDate, opt => opt.MapFrom(x => DateTime.Parse(x.Created)))
                .ForMember(x => x.LastModifiedDate, opt => opt.MapFrom(x => DateTime.Parse(x.LastModified)));
        }
    }
}
