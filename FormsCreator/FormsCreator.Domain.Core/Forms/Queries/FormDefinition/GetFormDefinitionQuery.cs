using FormsCreator.Domain.Core.Base.Queries;
using FormsCreator.Domain.Core.Forms.Dto;

namespace FormsCreator.Domain.Core.Forms.Queries.FormDefinition
{
    /// <summary>
    /// Zapytanie do pobierania szczegółów definicji formularza
    /// </summary>
    public class GetFormDefinitionQuery : IQuery<FormDefinitionDto>
    {
        /// <summary>
        /// Id po którym pobieramy
        /// </summary>
        public long Id { get; set; }
    }
}