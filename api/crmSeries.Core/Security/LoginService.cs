using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using crmSeries.Core.Data;
using crmSeries.Core.Domain.Admin;
using crmSeries.Core.Extensions;
using Microsoft.EntityFrameworkCore;

namespace crmSeries.Core.Security
{
    public interface ILoginService
    {
        Task<LoginDto> GetLoginByEmail(string email);

        Task<LoginDto> GetLoginByEmail(string email, string password);

        Task<LoginDto> GetLoginById(int id);
    }

    public class LoginDto
    {
        public int LoginId { get; set; }

        public int DealerId { get; set; }

        public string ApiKey { get; set; }

        public string Email { get; set; }
    }

    public class LoginService : ILoginService
    {
        private readonly AdminContext _context;
        private readonly IPasswordService _passwordService;

        public LoginService(AdminContext context, IPasswordService passwordService)
        {
            _context = context;
            _passwordService = passwordService;
        }

        public async Task<LoginDto> GetLoginByEmail(string email)
        {
            var login = await _context
                .Set<Login>()
                .Where(x => x.Active == true && x.Email == email)
                .ProjectTo<LoginDto>()
                .FirstOrDefaultAsync();
            
            return await MapDealer(login);
        }

        public async Task<LoginDto> GetLoginByEmail(string email, string password)
        {
            var login = await _context
                .Set<Login>()
                .Where(x => x.Active == true && x.Email == email)
                .ProjectTo<LoginValidationDto>()
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);

            if (login == null || login.HashPassword != _passwordService.CreateHash(password, login.Salt))
                return null;
            
            return await MapDealer(login.MapTo<LoginDto>());
        }

        public async Task<LoginDto> GetLoginById(int id)
        {
            var login = await _context
                .Set<Login>()
                .Where(x => x.Active == true && x.LoginId == id)
                .ProjectTo<LoginDto>()
                .FirstOrDefaultAsync();

            return await MapDealer(login);
        }

        private async Task<LoginDto> MapDealer(LoginDto login)
        {
            if (login != null)
            {
                var dealer = await _context
                    .Set<Dealer>()
                    .Where(x => x.DealerId == login.DealerId)
                    .ProjectTo<LoginDealerDto>()
                    .SingleOrDefaultAsync() ?? new LoginDealerDto();

                return Mapper.Map(dealer, login);
            }

            return login;
        }
        
        private class LoginValidationDto
        {
            public int LoginId { get; set; }

            public int DealerId { get; set; }
            
            public string Email { get; set; }

            public string HashPassword { get; set; }

            public string Salt { get; set; }
        }

        private class LoginDealerDto
        {
            public string ApiKey { get; set; }
        }

        public class LoginServiceProfile : Profile
        {
            public LoginServiceProfile()
            {
                CreateMap<Login, LoginDto>()
                    .Ignore(x => x.ApiKey);

                CreateMap<LoginValidationDto, LoginDto>()
                    .Ignore(x => x.ApiKey);

                CreateMap<LoginDealerDto, LoginDto>()
                    .MapMember(x => x.ApiKey)
                    .IgnoreRest();
            }
        }
    }
}
