
using Autofac;
using FormsCreator.Domain.Core.Base.Commands;
using FormsCreator.Domain.Core.ContextInfo;

namespace FormsCreator.Domain
{
    /// <summary>
    /// Klasa wywołująca polecenie
    /// </summary>
    public class CommandExecutor : ICommandExecutor
    {
        private readonly IComponentContext _context;
        private readonly ICurrentUserProvider _currentUserProvider;

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="context">kontekst bazy danych</param>
        /// <param name="currentUserProvider">Serwis trzymający informacje o aktualnym użytkowniku</param>
        public CommandExecutor(IComponentContext context,
            ICurrentUserProvider currentUserProvider)
        {
            _context = context;
            _currentUserProvider = currentUserProvider;
        }

        /// <summary>
        /// Metoda wykonuje polecenie
        /// </summary>
        /// <param name="command">Polecenie</param>
        /// <typeparam name="TCommand">Typ polecenia impolementujący ICommand</typeparam>
        /// <typeparam name="TResult">Typ zwracany przez polecenie</typeparam>
        /// <returns>Zwraca wynik wykonania polecenia o typie TResult</returns>
        public TResult Execute<TCommand, TResult>(TCommand command) where TCommand : ICommand<TResult>
        {
            var handler = _context.Resolve<ICommandHandler<TCommand, TResult>>();

            return handler.Execute(command);
        }
    }
}