using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using crmSeries.Core.Data;
using crmSeries.Core.Extensions;
using crmSeries.Core.Features.DocuSign.Dtos;
using crmSeries.Core.Features.OutputTemplateFields.Utility;
using crmSeries.Core.Logic.Queries;
using crmSeries.Core.Mediator.Decorators;
using DocuSign.eSign.Api;
using DocuSign.eSign.Client;
using DocuSign.eSign.Client.Auth;
using DocuSign.eSign.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MimeMapping;
using org.mariuszgromada.math.mxparser;

namespace crmSeries.Core.Features.DocuSign.Utility
{
    [HeavyEquipmentContext]
    public class DocuSignClient : IDocuSignClient
    {
        private const int EarlyRefreshSeconds = 5 * 60;

        private readonly IConfiguration _configuration;
        private readonly DocuSignContext _docuSignContext;
        private readonly DocuSignClientAccessor _docuSignClient;
        private readonly HeavyEquipmentContext _context;

        public DocuSignClient(IConfiguration configuration,
            DocuSignContext docuSignContext,
            DocuSignClientAccessor docuSignClient,
            HeavyEquipmentContext context)
        {
            _configuration = configuration;
            _docuSignContext = docuSignContext;
            _docuSignClient = docuSignClient;
            _context = context;
        }

        public async Task<List<DocuSignTemplate>> GetTemplates()
        {
            if (!Authenticate())
                return new List<DocuSignTemplate>();

            var templateApi = new TemplatesApi(_docuSignClient.Client.Configuration);

            var templates = await templateApi.ListTemplatesAsync(_docuSignContext.Account.AccountId);

            return templates.EnvelopeTemplates.MapTo<List<DocuSignTemplate>>();
        }

        public async Task<GetTemplateFullDto> GetTemplateById(string templateId)
        {
            if (!Authenticate())
                return new GetTemplateFullDto();

            var templateApi = new TemplatesApi(_docuSignClient.Client.Configuration);
            var templates = (await templateApi.ListTemplatesAsync(_docuSignContext.Account.AccountId,
                new TemplatesApi.ListTemplatesOptions { include = "folders,documents,notifications,recipients" }))
                .EnvelopeTemplates;

            var template = templates.SingleOrDefault(x => x.TemplateId == templateId);

            var fullTemplate = template?.MapTo<DocuSignTemplate>().MapTo<GetTemplateFullDto>() ?? new GetTemplateFullDto();

            foreach(var signer in template.Recipients.Signers)
            {
                int order = 0;
                int.TryParse(signer.RoutingOrder, out order);

                fullTemplate.Roles.Add(new TemplateRoleDto
                {
                    RoleName = signer.RoleName,
                    RoutingOrder = order
                });
            }

            return fullTemplate;
        }

        public async Task<EnvelopeTemplateResult> GetDocuSignTemplateById(string templateId)
        {
            if (!Authenticate())
                return new EnvelopeTemplateResult();

            var templateApi = new TemplatesApi(_docuSignClient.Client.Configuration);
            var templates = (await templateApi.ListTemplatesAsync(_docuSignContext.Account.AccountId,
                new TemplatesApi.ListTemplatesOptions { include = "folders,documents,notifications,recipients" }))
                .EnvelopeTemplates;

            var template = templates.SingleOrDefault(x => x.TemplateId == templateId);

            return template;
        }

        public async Task<List<TemplateFieldDto>> GetTemplateFields(string templateId)
        {
            var fields = new List<TemplateFieldDto>();

            if (!Authenticate())
                return fields;

            var templateApi = new TemplatesApi(_docuSignClient.Client.Configuration);
            var documents = await templateApi.ListDocumentsAsync(_docuSignContext.Account.AccountId, templateId);
            foreach (var doc in documents.TemplateDocuments)
            {
                var documentTabs = templateApi.GetDocumentTabs(
                    _docuSignContext.Account.AccountId,
                    templateId,
                    doc.DocumentId);

                fields.AddRange(GetDocumentFields(documentTabs, doc.Name));
            }

            return fields;
        }

        public async Task<Tabs> GetDocumentTabs(string templateId, string documentId)
        {
            if (!Authenticate())
                return new Tabs();

            var templateApi = new TemplatesApi(_docuSignClient.Client.Configuration);
            var documentTabs = await templateApi.GetDocumentTabsAsync
            (
                _docuSignContext.Account.AccountId,
                templateId,
                documentId
            );

            return documentTabs;
        }

        private IEnumerable<TemplateFieldDto> GetDocumentFields(Tabs documentTabs, string docName)
        {
            var fields = new List<TemplateFieldDto>();

            fields.AddRange(documentTabs.ApproveTabs?.MapTo<List<TemplateFieldDto>>() ?? new List<TemplateFieldDto>());
            fields.AddRange(documentTabs.CheckboxTabs?.MapTo<List<TemplateFieldDto>>() ?? new List<TemplateFieldDto>());
            fields.AddRange(documentTabs.CompanyTabs?.MapTo<List<TemplateFieldDto>>() ?? new List<TemplateFieldDto>());
            fields.AddRange(documentTabs.DateSignedTabs?.MapTo<List<TemplateFieldDto>>() ?? new List<TemplateFieldDto>());
            fields.AddRange(documentTabs.DateTabs?.MapTo<List<TemplateFieldDto>>() ?? new List<TemplateFieldDto>());
            fields.AddRange(documentTabs.DeclineTabs?.MapTo<List<TemplateFieldDto>>() ?? new List<TemplateFieldDto>());
            fields.AddRange(documentTabs.EmailAddressTabs?.MapTo<List<TemplateFieldDto>>() ?? new List<TemplateFieldDto>());
            fields.AddRange(documentTabs.EmailTabs?.MapTo<List<TemplateFieldDto>>() ?? new List<TemplateFieldDto>());
            fields.AddRange(documentTabs.EnvelopeIdTabs?.MapTo<List<TemplateFieldDto>>() ?? new List<TemplateFieldDto>());
            fields.AddRange(documentTabs.FirstNameTabs?.MapTo<List<TemplateFieldDto>>() ?? new List<TemplateFieldDto>());
            fields.AddRange(documentTabs.FormulaTabs?.MapTo<List<TemplateFieldDto>>() ?? new List<TemplateFieldDto>());
            fields.AddRange(documentTabs.FullNameTabs?.MapTo<List<TemplateFieldDto>>() ?? new List<TemplateFieldDto>());
            fields.AddRange(documentTabs.InitialHereTabs?.MapTo<List<TemplateFieldDto>>() ?? new List<TemplateFieldDto>());
            fields.AddRange(documentTabs.LastNameTabs?.MapTo<List<TemplateFieldDto>>() ?? new List<TemplateFieldDto>());
            fields.AddRange(documentTabs.ListTabs?.MapTo<List<TemplateFieldDto>>() ?? new List<TemplateFieldDto>());
            fields.AddRange(documentTabs.NotarizeTabs?.MapTo<List<TemplateFieldDto>>() ?? new List<TemplateFieldDto>());
            fields.AddRange(documentTabs.NoteTabs?.MapTo<List<TemplateFieldDto>>() ?? new List<TemplateFieldDto>());
            fields.AddRange(documentTabs.NumberTabs?.MapTo<List<TemplateFieldDto>>() ?? new List<TemplateFieldDto>());
            fields.AddRange(documentTabs.RadioGroupTabs?.MapTo<List<TemplateFieldDto>>() ?? new List<TemplateFieldDto>());
            fields.AddRange(documentTabs.SignerAttachmentTabs?.MapTo<List<TemplateFieldDto>>() ?? new List<TemplateFieldDto>());
            fields.AddRange(documentTabs.SignHereTabs?.MapTo<List<TemplateFieldDto>>() ?? new List<TemplateFieldDto>());
            fields.AddRange(documentTabs.SmartSectionTabs?.MapTo<List<TemplateFieldDto>>() ?? new List<TemplateFieldDto>());
            fields.AddRange(documentTabs.SsnTabs?.MapTo<List<TemplateFieldDto>>() ?? new List<TemplateFieldDto>());
            fields.AddRange(documentTabs.TextTabs?.MapTo<List<TemplateFieldDto>>() ?? new List<TemplateFieldDto>());
            fields.AddRange(documentTabs.TitleTabs?.MapTo<List<TemplateFieldDto>>() ?? new List<TemplateFieldDto>());
            fields.AddRange(documentTabs.ViewTabs?.MapTo<List<TemplateFieldDto>>() ?? new List<TemplateFieldDto>());
            fields.AddRange(documentTabs.ZipTabs?.MapTo<List<TemplateFieldDto>>() ?? new List<TemplateFieldDto>());

            fields.ForEach(x =>
            {
                x.DocumentName = docName;
            });

            return fields;
        }

        public async Task SendTemplate(string templateId, IEnumerable<DocuSignTemplateRecipient> recipients, int recordId)
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
                Status: DocuSignConstants.Statuses.Created);

            var envelopeApi = new EnvelopesApi(_docuSignClient.Client.Configuration);

            var result = await envelopeApi
                .CreateEnvelopeAsync(_docuSignContext.Account.AccountId, envelope)
                .ConfigureAwait(false);

            var draftRecipients = await envelopeApi.ListRecipientsAsync(_docuSignContext.Account.AccountId,
                result.EnvelopeId);

            foreach (var signer in draftRecipients.Signers)
            {
                try
                {
                    var signerTabs = await envelopeApi
                        .ListTabsAsync(_docuSignContext.Account.AccountId,
                        result.EnvelopeId,
                        signer.RecipientId);

                    SetDocumentTabValues(recordId, templateId, signerTabs);

                    envelopeApi.UpdateTabs
                    (
                        _docuSignContext.Account.AccountId,
                        result.EnvelopeId,
                        signer.RecipientId,
                        signerTabs
                    );
                }
                catch
                {
                    //TODO: Find out for sure that this is true, and then just set it for the first signer
                    // Only first signer needs values set?
                }
            }

            envelopeApi.Update(_docuSignContext.Account.AccountId, result.EnvelopeId, new Envelope
            {
                Status = DocuSignConstants.Statuses.Sent
            });

            return;
        }

        private void SetDocumentTabValues(int recordId, string templateId, Tabs tabs)
        {
            var outputTemplate = _context.OutputTemplate.SingleOrDefault(x => x.SourceId == templateId);

            if (outputTemplate != null)
            {
                var crmSeriesTableName = _context.OutputTemplateCategory
                    .SingleOrDefault(x => x.CategoryId == outputTemplate.CategoryId)
                    ?.RecordType;

                var crmSeriesFields = _context.OutputTemplateField.Where(x => x.TemplateId == outputTemplate.TemplateId);

                foreach(var field in crmSeriesFields)
                {
                    if (string.IsNullOrEmpty(field.CrmSeriesField))
                        continue;

                    string fieldValue = GetFieldValue(field.Calculation, field.CrmSeriesField, crmSeriesTableName, recordId);
                    
                    if(field.Calculation)
                    {
                        var exp = new Expression(fieldValue);
                        fieldValue = exp.calculate().ToString();
                    }

                    switch (field.FieldType)
                    {
                        case OutputTemplateFieldsConstants.FieldTypes.Approve:
                            break;
                        case OutputTemplateFieldsConstants.FieldTypes.Checkbox:
                            break;
                        case OutputTemplateFieldsConstants.FieldTypes.Company:
                            break;
                        case OutputTemplateFieldsConstants.FieldTypes.Date:
                            break;
                        case OutputTemplateFieldsConstants.FieldTypes.DateSigned:
                            break;
                        case OutputTemplateFieldsConstants.FieldTypes.Decline:
                            break;
                        case OutputTemplateFieldsConstants.FieldTypes.Email:
                            break;
                        case OutputTemplateFieldsConstants.FieldTypes.EmailAddress:
                            break;
                        case OutputTemplateFieldsConstants.FieldTypes.EnvelopeId:
                            break;
                        case OutputTemplateFieldsConstants.FieldTypes.FirstName:
                            break;
                        case OutputTemplateFieldsConstants.FieldTypes.Formula:
                            break;
                        case OutputTemplateFieldsConstants.FieldTypes.FullName:
                            break;
                        case OutputTemplateFieldsConstants.FieldTypes.InitialHere:
                            break;
                        case OutputTemplateFieldsConstants.FieldTypes.LastName:
                            break;
                        case OutputTemplateFieldsConstants.FieldTypes.List:
                            break;
                        case OutputTemplateFieldsConstants.FieldTypes.Notarize:
                            break;
                        case OutputTemplateFieldsConstants.FieldTypes.Note:
                            break;
                        case OutputTemplateFieldsConstants.FieldTypes.Number:
                            break;
                        case OutputTemplateFieldsConstants.FieldTypes.RadioGroup:
                            break;
                        case OutputTemplateFieldsConstants.FieldTypes.SignerAttachment:
                            break;
                        case OutputTemplateFieldsConstants.FieldTypes.SignHere:
                            break;
                        case OutputTemplateFieldsConstants.FieldTypes.SmartSection:
                            break;
                        case OutputTemplateFieldsConstants.FieldTypes.Ssn:
                            break;
                        case OutputTemplateFieldsConstants.FieldTypes.Text:
                            var tab = tabs.TextTabs?.FirstOrDefault(x => x.TabLabel == field.TemplateField);
                            if (tab != null)
                            {
                                tab.OriginalValue = fieldValue;
                                tab.Value = fieldValue;
                            }
                            break;
                        case OutputTemplateFieldsConstants.FieldTypes.Title:
                            break;
                        case OutputTemplateFieldsConstants.FieldTypes.View:
                            break;
                        case OutputTemplateFieldsConstants.FieldTypes.Zip:
                            break;
                    }
                }
            }
        }

        private string GetFieldValue(bool calculation, string crmSeriesField, string crmSeriesTableName, int recordId)
        {
            var tableName = new SqlParameter("tableName", crmSeriesTableName);
            var pkName = _context.StringValue
                .FromSql(
                    "select 1 as Id, column_name as Value " + 
                    "from information_schema.columns " +
                    "where table_name = @tableName and ordinal_position = 1", tableName
                )
                .AsNoTracking()
                .First()
                .Value;

            string stringValue = crmSeriesField;
            var tableFields = GetTableFields(stringValue);
            foreach(var field in tableFields)
            {
                var fieldName = new SqlParameter("fieldName", field);
                var id = new SqlParameter("id", recordId);

                var fieldValue = _context.StringValue
                    .FromSql(
                        $"select 1 as Id, CONVERT(varchar(max), {field}) as Value from {tableName.Value} where {pkName} = @id",
                        id)
                    .AsNoTracking()
                    .First()
                    .Value;

                stringValue = stringValue.Replace(field, fieldValue);
            }

            return stringValue;
        }

        private List<string> GetTableFields(string str)
        {
            Regex regex = new Regex(@"(?<=\[)[^]]*(?=\])", RegexOptions.IgnoreCase);
            MatchCollection matches = regex.Matches(str);
            return matches.Cast<Match>().Select(m => "[" + m.Value + "]").Distinct().ToList();
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
                .ForMember(x => x.LastModifiedDate, opt => opt.MapFrom(x => DateTime.Parse(x.LastModified)))
                .ForMember(x => x.AbsoluteUri, opt => opt.MapFrom(x => x.Documents.First().Uri))
                .ForMember(x => x.FileName, opt => opt.MapFrom(x => x.Documents.First().Name))
                .ForMember(x => x.ContentType, opt => opt.MapFrom(x => MimeUtility.GetMimeMapping(x.Documents.First().Name)))
                .ForMember(x => x.TemplateType, opt => opt.MapFrom(x => ""));
            
        }
    }
}
