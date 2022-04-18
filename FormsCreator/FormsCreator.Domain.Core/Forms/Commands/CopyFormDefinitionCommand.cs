using FormsCreator.Domain.Core.Base.Commands;

namespace FormsCreator.Domain.Core.Forms.Commands
{
    /// <summary>
    /// Polecenie skopiowania definicji formularza
    /// </summary>
    public class CopyFormDefinitionCommand : ICommand<long>
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }
    }
}