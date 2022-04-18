using System.Threading.Tasks;
using FormsCreator.Domain.Core.Exceptions;
using FormsCreator.Domain.Core.Security.Dto;
using FormsCreator.Domain.Core.Security.Services;
using FormsCreator.Domain.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FormsCreator.Domain.Security
{
    public class AuthService : IAuthService
    {
        private readonly Context _context;
        private readonly ILogger<AuthService> _logger;

        public AuthService(Context context, ILogger<AuthService> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task<long> Login(SignInDto signInDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Login == signInDto.Login);

            if (user == null)
            {
                _logger.LogError($"User with login: {signInDto.Login} was not found.");
                throw new DomainException(DomainException.AuthExceptionCodes.NotFound);
            }
            
            if (string.IsNullOrEmpty(signInDto.Password))
            {
                throw new DomainException(DomainException.AuthExceptionCodes.InvalidCredentials);
            }
            
            return user.Id;
        }
    }
}