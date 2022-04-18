using FormsCreator.Domain.Core.Base.Commands;
using FormsCreator.Domain.Core.Exceptions;
using FormsCreator.Domain.Core.Forms.Commands;
using FormsCreator.Domain.Db;
using System.Linq;

namespace FormsCreator.Domain.Forms.CommandHandlers
{
    public class UpdateStatusFormDefinitionCommandHandler : ICommandHandler<UpdateStatusFormDefinitionCommand, long>
    {
        private readonly Context _context;

        public UpdateStatusFormDefinitionCommandHandler(Context context)
        {
            _context = context;
        }

        public long Execute(UpdateStatusFormDefinitionCommand command)
        {
            var entity = _context.FormDefinitions.FirstOrDefault(x => x.Id == command.Id);
            if (entity == null)
            {
                throw new NotFoundException();
            }

            entity.SetStatus(command.Status);

            _context.SaveChanges();

            return entity.Id;
        }
    }
}