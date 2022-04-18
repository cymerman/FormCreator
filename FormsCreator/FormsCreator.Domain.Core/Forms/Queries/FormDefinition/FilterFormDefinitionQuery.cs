using FormsCreator.Domain.Core.Base;
using FormsCreator.Domain.Core.Base.Queries;
using FormsCreator.Domain.Core.Forms.Dto;

namespace FormsCreator.Domain.Core.Forms.Queries.FormDefinition
{
    public class FilterFormDefinitionQuery : IFilterQuery<FormDefinitionDto>
    {
        /// <summary>
        /// Identyfikator
        /// </summary>
        [ColumnType(ColumnType.Integer)]
        public QueryFilterItem Id { get; set; }

        /// <summary>
        /// Tytuł
        /// </summary>
        [ColumnType(ColumnType.Text)]
        public QueryFilterItem Title { get; set; }

        /// <summary>
        /// Opis
        /// </summary>
        [ColumnType(ColumnType.Text)]
        public QueryFilterItem Description { get; set; }
        
        /// <summary>
        /// Nazwa
        /// </summary>
        [ColumnType(ColumnType.Text)]
        public QueryFilterItem Name { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        [ColumnType(ColumnType.Integer)]
        public QueryFilterItem Status { get; set; }
        
        public RowsRange RowsRange { get; set; }
        public Sorting Sorting { get; set; }
    }
}