using System.Linq;
using FormsCreator.Domain.Core.ContextInfo;
using FormsCreator.Domain.Core.Forms.Dto;
using FormsCreator.Domain.Core.Forms.Queries.Form;
using FormsCreator.Domain.Db;
using Microsoft.EntityFrameworkCore;

namespace FormsCreator.Domain.Forms.QueryHandlers.Form
{
    public class FilterFormQueryHandler : DynamicFilterQueryHandlerBase< FilterFormQuery, FormDto>
    {
        private readonly Context _context;
        private readonly ICurrentUserProvider _currentUserProvider;

        public FilterFormQueryHandler(Context context, ICurrentUserProvider currentUserProvider)
        {
            _context = context;
            _currentUserProvider = currentUserProvider;
        }
        
        protected override IQueryable<FormDto> PrepareQuery(FilterFormQuery query)
        {
            var currentUserId = _currentUserProvider.GetUserId().Value;
            
            var forms = _context.Forms.Include(x => x.FormDefinition)
                .Where(x => x.FormDefinition.CreatedBy == currentUserId)
                .Select(x => new FormDto()
                {
                    FormUid = x.FormDefinition.FormUid,
                    CreatedOn = x.CreatedOn,
                    FormData = x.Data
                });

            return forms;
        }
    }
}