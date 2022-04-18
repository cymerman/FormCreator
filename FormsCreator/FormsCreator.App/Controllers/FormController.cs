using System.Threading.Tasks;
using FormsCreator.Domain.Core.Base.Queries;
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

        public FormController(IQueryProcessor queryProcessor)
        {
            _queryProcessor = queryProcessor;
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
    }
}