using System.Linq;
using FormsCreator.Domain.Core.Base.Commands;
using FormsCreator.Domain.Core.Exceptions;
using FormsCreator.Domain.Core.Forms.Commands;
using FormsCreator.Domain.Db;
using Microsoft.EntityFrameworkCore;

namespace FormsCreator.Domain.Forms.CommandHandlers
{
    /// <summary>
    /// Klasa do wywoływania polecenie kopiowania definicji formularza
    /// </summary>
    public class CopyFormDefinitionCommandHandler : ICommandHandler<CopyFormDefinitionCommand, long>
    {
        private readonly Context _context;

        /// <summary>
        /// konstruktor
        /// </summary>
        /// <param name="context">Kontekst bazodanowy</param>
        public CopyFormDefinitionCommandHandler(Context context)
        {
            _context = context;
        }

        /// <summary>
        /// Metoda do wywoływania polecenia
        /// </summary>
        /// <param name="command">Polecenie</param>
        /// <returns>Id skopiowanego formularza</returns>
        /// <exception cref="NotFoundException">nie znaleziono definicji formularza</exception>
        public long Execute(CopyFormDefinitionCommand command)
        {
            var formDefinition = _context.FormDefinitions
                .AsNoTracking()
                .FirstOrDefault(x => x.Id == command.Id);
            if (formDefinition == null)
            {
                throw new NotFoundException();
            }

            var clone = formDefinition.Clone();
            _context.FormDefinitions.Add(clone);
            _context.SaveChanges();

            _context.SaveChanges();

            return clone.Id;
        }
    }
}