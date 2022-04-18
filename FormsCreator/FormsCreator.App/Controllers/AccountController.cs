using System.Threading.Tasks;
using FormsCreator.Domain.Core.Account.Dto;
using FormsCreator.Domain.Core.Account.Queries;
using FormsCreator.Domain.Core.Base.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FormsCreator.App.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IQueryProcessor _queryProcessor;

        public AccountController(IQueryProcessor queryProcessor)
        {
            _queryProcessor = queryProcessor;
        }
        
        /// <summary>
        /// Pobieranie danych uzytkownika
        /// </summary>
        /// <returns>Odpowiedz http o statusie 200 z informacjami o uzytkowniku</returns>
        [HttpGet]
        public async Task<IActionResult> GetAccountData()
        {
            var result = await _queryProcessor.Process<GetAccountDataQuery, AccountDto>(
                new GetAccountDataQuery());

            return Ok(result);
        }
    }
}