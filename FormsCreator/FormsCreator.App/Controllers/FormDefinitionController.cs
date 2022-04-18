using System.Threading.Tasks;
using FormsCreator.Domain.Core.Base.Commands;
using FormsCreator.Domain.Core.Base.Queries;
using FormsCreator.Domain.Core.Forms;
using FormsCreator.Domain.Core.Forms.Commands;
using FormsCreator.Domain.Core.Forms.Dto;
using FormsCreator.Domain.Core.Forms.Queries.FormDefinition;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FormsCreator.App.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class FormDefinitionController : ControllerBase
    {
        private readonly IQueryProcessor _queryProcessor;
        private readonly ICommandExecutor _commandExecutor;

        public FormDefinitionController(IQueryProcessor queryProcessor, ICommandExecutor commandExecutor)
        {
            _queryProcessor = queryProcessor;
            _commandExecutor = commandExecutor;
        }
        
        /// <summary>
        /// Pobieranie listy definicji formularzy kontekstowego użytkownika
        /// </summary>
        /// <param name="request">Zapytanie do pobrania listy formularzy kontekstowego użytkownika</param>
        /// <returns>Odpowiedz http o statusie 200 z listą formularzy</returns>
        [HttpGet]
        public async Task<IActionResult> Filter([FromQuery] FilterFormDefinitionQuery request)
        {
            var result = await _queryProcessor.Process<FilterFormDefinitionQuery, FilterQueryResult<FormDefinitionDto>>(request);

            return Ok(result);
        }
        
        /// <summary>
        /// Metoda służy do dodawania definicji 
        /// </summary>
        /// <param name="request">zapytanie do dodawania definicji</param>
        /// <returns>Id dodanego formularza</returns>
        [HttpPost]
        [Route("")]
        public IActionResult Post([FromBody] AddFormDefinitionCommand request)
        {
            var result = _commandExecutor.Execute<AddFormDefinitionCommand, long>(request);

            return Ok(result);
        }
        
        /// <summary>
        /// Metoda służy do aktualizacji definicji formularza
        /// </summary>
        /// <param name="id">Id aktualizowanej definicji formularza</param>
        /// <param name="request">Zapytanie do aktualizacji formularza</param>
        /// <returns>Id zmienionego formularza</returns>
        [HttpPut]
        [Route("{id}")]
        public IActionResult Put(long id, [FromBody] UpdateFormDefinitionCommand request)
        {
            var result = _commandExecutor.Execute<UpdateFormDefinitionCommand, long>(request);

            return Ok(result);
        }
        
        /// <summary>
        /// Metoda do pobrania szczegółów definicji formularza
        /// </summary>
        /// <param name="id">Id formularza</param>
        /// <returns>Odpowiedz http o statusie 200 z szczegółami def. formularza</returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var result = await _queryProcessor.Process<GetFormDefinitionQuery, FormDefinitionDto>(
                new GetFormDefinitionQuery() {Id = id});

            return Ok(result);
        }
        
        [HttpPost]
        [Route("{id}/Copy")]
        public IActionResult CopyDefinition(long id)
        {
            var result = _commandExecutor.Execute<CopyFormDefinitionCommand, long>(new CopyFormDefinitionCommand()
            {
                Id = id,
            });

            return Ok(result);
        }
        
        /// <summary>
        /// Metoda służy do aktualizacji statusu definicji formularza
        /// </summary>
        /// <param name="id">Id aktualizowanej definicji formularza</param>
        /// <param name="status">Status do zmiany</param>
        /// <returns>Id zmienionego wniosku</returns>
        [HttpPut]
        [Route("{id}/Status/{status}")]
        public IActionResult UpdateStatus(long id, FormDefinitionStatus status)
        {
            var result = _commandExecutor.Execute<UpdateStatusFormDefinitionCommand, long>(new UpdateStatusFormDefinitionCommand()
            {
                Id = id,
                Status = status
            });

            return Ok(result);
        }


        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(long id)
        {
            var result = _commandExecutor.Execute<DeleteFormDefinitionCommand, long>(new DeleteFormDefinitionCommand()
            {
                Id = id
            });

            return Ok(result);
        }
    }
}