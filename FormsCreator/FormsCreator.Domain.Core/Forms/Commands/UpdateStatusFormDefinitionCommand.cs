using FormsCreator.Domain.Core.Base.Commands;

namespace FormsCreator.Domain.Core.Forms.Commands
{
    public class UpdateStatusFormDefinitionCommand : ICommand<long>
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Status 
        /// </summary>
        public FormDefinitionStatus Status { get; set; }
    }
}