using System;
using FormsCreator.Domain.Core.Base.Commands;
using FormsCreator.Domain.Core.Forms;
using FormsCreator.Domain.Core.Forms.Commands;
using FormsCreator.Domain.Db;
using System.Linq;
using FormsCreator.Domain.Core.ContextInfo;

namespace FormsCreator.Domain.Forms.CommandHandlers
{
    /// <summary>
    /// Klasa wywołująca polecenie dodania definicji formularza
    /// </summary>
    public class AddFormDefinitionCommandHandler : ICommandHandler<AddFormDefinitionCommand, long>
    {
        private readonly Context _context;
        private readonly ICurrentUserProvider _currentUserProvider;

        /// <summary>
        /// konstruktor
        /// </summary>
        /// <param name="context">Kontekst bazodanowy</param>
        public AddFormDefinitionCommandHandler(Context context, ICurrentUserProvider currentUserProvider)
        {
            _context = context;
            _currentUserProvider = currentUserProvider;
        }

        /// <summary>
        /// Metoda wywołuje polecenie
        /// </summary>
        /// <param name="command">Poleceni dodania definicji formularza</param>
        /// <returns>Id dodanej definicji formularza</returns>
        public long Execute(AddFormDefinitionCommand command)
        {
            var entity = new FormDefinition()
            {
                Description = command.Description,
                Form = command.Form,
                Name = command.Name,
                Title = command.Title,
                IsFileCountLimited = command.IsFileCountLimited,
                FileCount = command.FileCount,
                FileCountMaximum = command.FileCountMaximum,
                FileCountMinimum = command.FileCountMinimum,
                UserId = _currentUserProvider.GetUserId().Value,
                FormUid = Guid.NewGuid().ToString()
            };
            
            if (command.ChildrenIds.Count > 0)
            {
                var subApplications = _context.FormDefinitions.Where(x => command.ChildrenIds.Contains(x.Id));
                entity.Children.AddRange(subApplications);
            }

            _context.FormDefinitions.Add(entity);
            _context.SaveChanges();

            return entity.Id;
        }
    }
}