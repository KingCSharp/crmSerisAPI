using System.Linq;
using System.Threading.Tasks;
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

        public Task<LoginDto> GetLoginByEmail(string email)
        {
            return _context
                .Set<Login>()
                .Where(x => x.Active == true && x.Email == email)
                .ProjectTo<LoginDto>()
                .FirstOrDefaultAsync();
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

            return login.MapTo<LoginDto>();
        }

        public Task<LoginDto> GetLoginById(int id)
        {
            return _context
                .Set<Login>()
                .Where(x => x.Active == true && x.LoginId == id)
                .ProjectTo<LoginDto>()
                .FirstOrDefaultAsync();
        }
        
        private class LoginValidationDto
        {
            public int LoginId { get; set; }

            public int DealerId { get; set; }
            
            public string Email { get; set; }

            public string HashPassword { get; set; }

            public string Salt { get; set; }
        }
    }
}
