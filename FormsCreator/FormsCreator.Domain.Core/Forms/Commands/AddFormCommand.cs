using FormsCreator.Domain.Core.Base.Commands;

namespace FormsCreator.Domain.Core.Forms.Commands
{
    public class AddFormCommand : ICommand<long>
    {
        public string Id { get; set; }
        public string FormData { get; set; }
    }
}