using System.Collections.Generic;
using FormsCreator.Domain.Core.Base.Commands;
using FormsCreator.Domain.Core.Forms.Dto;

namespace FormsCreator.Domain.Core.Forms.Commands
{
    /// <summary>
    /// Polecenie aktualizacji definicji formularza
    /// </summary>
    public class UpdateFormDefinitionCommand : FormDefinitionDto, ICommand<long>
    {
        /// <summary>
        /// Lista Id dzieci formularza
        /// </summary>
        public List<long> ChildrenIds { get; set; } = new List<long>();
    }
}