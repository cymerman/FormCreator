using FormsCreator.Domain.Core.Base;
using FormsCreator.Domain.Core.Base.Queries;
using FormsCreator.Domain.Core.Forms.Dto;

namespace FormsCreator.Domain.Core.Forms.Queries.Form
{
    public class FilterFormQuery  : IFilterQuery<FormDto>
    {
        /// <summary>
        /// Id definicji
        /// </summary>
        [ColumnType(ColumnType.Text)]
        public QueryFilterItem FormUid { get; set; }
        
        /// <summary>
        /// Data utworzenia
        /// </summary>
        [ColumnType(ColumnType.Date)]
        public QueryFilterItem CreatedOn { get; set; }
        
        public RowsRange RowsRange { get; set; }
        public Sorting Sorting { get; set; }
    }
}