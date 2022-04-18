using FormsCreator.Domain.Core.Base.Queries;
using FormsCreator.Domain.Core.Forms.Dto;

namespace FormsCreator.Domain.Core.Forms.Queries.Form
{
    public class GetFormQuery : IQuery<FormDefinitionDto>
    {
        public string Id { get; set; }
    }
}