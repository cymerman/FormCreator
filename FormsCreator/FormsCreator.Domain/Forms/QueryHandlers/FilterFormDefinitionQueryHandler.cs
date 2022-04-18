using System.Linq;
using FormsCreator.Domain.Core.ContextInfo;
using FormsCreator.Domain.Core.Forms.Dto;
using FormsCreator.Domain.Core.Forms.Queries.FormDefinition;
using FormsCreator.Domain.Db;

namespace FormsCreator.Domain.Forms.QueryHandlers
{
    public class FilterFormDefinitionQueryHandler : DynamicFilterQueryHandlerBase< FilterFormDefinitionQuery, FormDefinitionDto>
    {
        private readonly Context _context;
        private readonly ICurrentUserProvider _currentUserProvider;

        public FilterFormDefinitionQueryHandler(Context context, ICurrentUserProvider currentUserProvider)
        {
            _context = context;
            _currentUserProvider = currentUserProvider;
        }

        protected override IQueryable<FormDefinitionDto> PrepareQuery(FilterFormDefinitionQuery query)
        {
            var currentUserId = _currentUserProvider.GetUserId().Value;
            
            return _context.FormDefinitions
                .Where(x => x.UserId == currentUserId)
                .Select(x => new FormDefinitionDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Status = x.Status,
                    Title = x.Title,
                    Form = x.Form,
                    FileCount = x.FileCount,
                    FileCountMaximum = x.FileCountMaximum,
                    FileCountMinimum = x.FileCountMinimum,
                    IsFileCountLimited = x.IsFileCountLimited,
                    Description = x.Description,
                    CreatedOn = x.CreatedOn,
                    CreatedBy = x.CreatedBy.ToString(),
                    ModifiedOn = x.ModifiedOn,
                    ModifiedBy = x.ModifiedBy.ToString(),
                });
        }
    }
}