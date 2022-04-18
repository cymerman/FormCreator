using FormsCreator.Domain.Core.Base.Queries;
using FormsCreator.Domain.Core.Forms.Dto;
using FormsCreator.Domain.Core.Forms.Queries.FormDefinition;
using FormsCreator.Domain.Db;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using FormsCreator.Domain.Core.Exceptions;

namespace FormsCreator.Domain.Forms.QueryHandlers
{
    public class GetFormDefinitionQueryHandler : IQueryHandler<GetFormDefinitionQuery, FormDefinitionDto>
    {
        private readonly Context _context;

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="context">Kontekst bazodanowy</param>
        public GetFormDefinitionQueryHandler(Context context)
        {
            _context = context;
        }

        /// <summary>
        /// Metoda do przetworzenia zapytania
        /// </summary>
        /// <param name="query">Zapytanie</param>
        /// <returns>Szczegóły definicji formularza</returns>
        public async Task<FormDefinitionDto> Process(GetFormDefinitionQuery query)
        {
            var result = await _context.FormDefinitions
                .Include(x => x.Children)
                .FirstOrDefaultAsync(x => x.Id == query.Id);
            if (result == null)
            {
                throw new NotFoundException();
            }
            
            var dto = FormDefinitionDto.MapFrom(result);

            return dto;
        }
    }
}