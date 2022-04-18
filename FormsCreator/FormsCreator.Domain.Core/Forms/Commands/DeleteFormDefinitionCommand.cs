using FormsCreator.Domain.Core.Base.Commands;

namespace FormsCreator.Domain.Core.Forms.Commands
{
    /// <summary>
    /// Polecenie usunięcia definicji formularza
    /// </summary>
    public class DeleteFormDefinitionCommand : ICommand<long>
    {
        /// <summary>
        /// Id definicji formularza
        /// </summary>
        public long Id { get; set; }
    }
}