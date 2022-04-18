using System.Threading.Tasks;
using FormsCreator.Domain.Core.Security.Dto;

namespace FormsCreator.Domain.Core.Security.Services
{
    public interface IAuthService
    {
        /// <summary>
        /// Logowanie
        /// </summary>
        /// <returns>Dodaje ciastka</returns>
        Task<long> Login(SignInDto signInDto);
    }
}