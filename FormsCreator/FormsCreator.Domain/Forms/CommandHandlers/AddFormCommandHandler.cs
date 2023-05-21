using System;
using System.Linq;
using FormsCreator.Domain.Core.Base.Commands;
using FormsCreator.Domain.Core.Exceptions;
using FormsCreator.Domain.Core.Forms;
using FormsCreator.Domain.Core.Forms.Commands;
using FormsCreator.Domain.Db;
using Microsoft.EntityFrameworkCore;

namespace FormsCreator.Domain.Forms.CommandHandlers
{
    public class AddFormCommandHandler : ICommandHandler<AddFormCommand, long>
    {
        private readonly Context _context;

        public AddFormCommandHandler(Context context)
        {
            _context = context;
        }

        public long Execute(AddFormCommand command)
        {
            var formDefinition = _context.FormDefinitions.FirstOrDefault(x => x.FormUid == command.Id);

            if (formDefinition == null)
            {
                throw new NotFoundException();
            }

            var entity = new Form()
            {
                Data = command.FormData,
                FormDefinitionId = formDefinition.Id,
                CreatedOn = DateTime.Now
            };

            _context.Form.Add(entity);
            _context.SaveChanges();

            return entity.Id;
        }
    }
}