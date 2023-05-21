using System.Threading.Tasks;
using FormsCreator.Domain.Core.Base.Commands;
using FormsCreator.Domain.Core.Base.Queries;
using FormsCreator.Domain.Core.Forms.Commands;
using FormsCreator.Domain.Core.Forms.Dto;
using FormsCreator.Domain.Core.Forms.Queries.Form;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FormsCreator.App.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [AllowAnonymous]
    public class FormController : ControllerBase
    {
        private readonly IQueryProcessor _queryProcessor;
        private readonly ICommandExecutor _commandExecutor;

        public FormController(IQueryProcessor queryProcessor, ICommandExecutor commandExecutor)
        {
            _queryProcessor = queryProcessor;
            _commandExecutor = commandExecutor;
        }
        
        /// <summary>
        /// Pobieranie listy wypelnionych formularzy kontekstowego użytkownika
        /// </summary>
        /// <param name="request">Zapytanie do pobrania listy formularzy kontekstowego użytkownika</param>
        /// <returns>Odpowiedz http o statusie 200 z listą formularzy</returns>
        [HttpGet]
        public async Task<IActionResult> Filter([FromQuery] FilterFormQuery request)
        {
            var result = await _queryProcessor.Process<FilterFormQuery, FilterQueryResult<FormDto>>(request);

            return Ok(result);
        }

 
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetForm([FromRoute] string id)
        {
            var result = await _queryProcessor.Process<GetFormQuery, FormDefinitionDto>(new GetFormQuery
            {
                Id = id
            });

            return Ok(result);
        }
        
        [HttpPost]
        [Route("{id}")]
        public IActionResult SaveForm([FromRoute] string id,[FromBody] FormDetails formData)
        {
            var result = _commandExecutor.Execute<AddFormCommand, long>(new AddFormCommand
            {
                Id = id,
                FormData = formData.FormData
            });

            return Ok(result);
        }
    }
}