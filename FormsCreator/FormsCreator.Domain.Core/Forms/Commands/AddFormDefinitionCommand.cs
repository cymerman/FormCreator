using System.Collections.Generic;
using FormsCreator.Domain.Core.Base.Commands;
using FormsCreator.Domain.Core.Forms.Dto;

namespace FormsCreator.Domain.Core.Forms.Commands
{
    /// <summary>
    /// Polecenie dodania definicji formularza
    /// </summary>
    public class AddFormDefinitionCommand : FormDefinitionDto, ICommand<long>
    {
        /// <summary>
        /// Lista Id dzieci formularza
        /// </summary>
        public List<long> ChildrenIds { get; set; } = new List<long>();
       
    }
}