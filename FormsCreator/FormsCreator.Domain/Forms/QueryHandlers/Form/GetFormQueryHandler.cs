using System.Threading.Tasks;
using FormsCreator.Domain.Core.Base.Queries;
using FormsCreator.Domain.Core.Exceptions;
using FormsCreator.Domain.Core.Forms.Dto;
using FormsCreator.Domain.Core.Forms.Queries.Form;
using FormsCreator.Domain.Db;
using Microsoft.EntityFrameworkCore;

namespace FormsCreator.Domain.Forms.QueryHandlers.Form
{
    public class GetFormQueryHandler : IQueryHandler<GetFormQuery, FormDefinitionDto>
    {
        private readonly Context _context;

        public GetFormQueryHandler(Context context)
        {
            _context = context;
        }

        public async Task<FormDefinitionDto> Process(GetFormQuery query)
        {
            var formDefinition = await _context.FormDefinitions
                .Include(x => x.Children)
                .FirstOrDefaultAsync(x => x.FormUid == query.Id);

            if (formDefinition == null)
            {
                throw new NotFoundException();
            }

            var dto = FormDefinitionDto.MapFrom(formDefinition);

            return dto;
        }
    }
}