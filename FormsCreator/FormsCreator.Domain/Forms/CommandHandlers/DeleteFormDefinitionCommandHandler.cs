using FormsCreator.Domain.Core.Base.Commands;
using FormsCreator.Domain.Core.Forms.Commands;
using FormsCreator.Domain.Db;
using System.Linq;
using FormsCreator.Domain.Core.Exceptions;

namespace FormsCreator.Domain.Forms.CommandHandlers
{
    /// <summary>
    /// Klasa wywołująca polecenie usunięcia definicji formularza
    /// </summary>
    public class DeleteFormDefinitionCommandHandler : ICommandHandler<DeleteFormDefinitionCommand, long>
    {
        private readonly Context _context;

        /// <summary>
        /// konstruktor
        /// </summary>
        /// <param name="context">Kontekst bazodanowy</param>
        public DeleteFormDefinitionCommandHandler(Context context)
        {
            _context = context;
        }

        /// <summary>
        /// Metoda wywołująca polecenie
        /// </summary>
        /// <param name="command">Polecenie</param>
        /// <returns>Id usuniętego formularza</returns>
        /// <exception cref="NotFoundException">Nie znaleziono definicji do usunięcia</exception>
        public long Execute(DeleteFormDefinitionCommand command)
        {
            var entity = _context.FormDefinitions.FirstOrDefault(x => x.Id == command.Id);
            if (entity == null)
            {
                throw new NotFoundException();
            }
            
            _context.FormDefinitions.Remove(entity);
            _context.SaveChanges();

            return entity.Id;
        }
    }
}