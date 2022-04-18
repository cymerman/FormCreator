using FormsCreator.Domain.Core.Base.Commands;
using FormsCreator.Domain.Core.Forms.Commands;
using FormsCreator.Domain.Db;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using FormsCreator.Domain.Core.Exceptions;

namespace FormsCreator.Domain.Forms.CommandHandlers
{
    /// <summary>
    /// Klasa do obsługi polecenia aktualizacji definicji formularza
    /// </summary>
    public class UpdateFormDefinitionCommandHandler : ICommandHandler<UpdateFormDefinitionCommand, long>
    {
        private readonly Context _context;

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="context">Kontekst bazodanowy</param>
        public UpdateFormDefinitionCommandHandler(Context context)
        {
            _context = context;
        }

        /// <summary>
        ///  Metoda do wyłowyania polecenia
        /// </summary>
        /// <param name="command">Polecenie</param>
        /// <returns>Id formularza</returns>
        /// <exception cref="NotFoundException">Nie znaleziono definicji formularza</exception>
        public long Execute(UpdateFormDefinitionCommand command)
        {
            var entity = _context.FormDefinitions
                .Include(x => x.Children)
                .FirstOrDefault(x => x.Id == command.Id);
            if (entity == null)
            {
                throw new NotFoundException();
            }
            
            entity.Description = command.Description;
            entity.Title = command.Title;
            entity.Form = command.Form;
            entity.Name = command.Name;
            entity.IsFileCountLimited = command.IsFileCountLimited;
            entity.FileCount = command.FileCount;
            entity.FileCountMaximum = command.FileCountMaximum;
            entity.FileCountMinimum = command.FileCountMinimum;

            entity.Children.Clear();
            var subApplications = _context.FormDefinitions.Where(x => command.ChildrenIds.Contains(x.Id));
            entity.Children.AddRange(subApplications);

            _context.SaveChanges();

            return entity.Id;
        }
    }
}