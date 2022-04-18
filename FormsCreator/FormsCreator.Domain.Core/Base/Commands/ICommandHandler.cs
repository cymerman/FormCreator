using FormsCreator.Domain.Core.Base.Queries;

namespace FormsCreator.Domain.Core.Base.Commands
{
    /// <summary>
    /// Interfejs dla handlera obsługującego polceania
    /// </summary>
    /// <typeparam name="TCommand">Typ polecenia obsługiwanego przez handler</typeparam>
    /// <typeparam name="TResult">Typ wyniku zwracany przez handler</typeparam>
    public interface ICommandHandler<in TCommand, out TResult> : IHandler where TCommand : ICommand<TResult>
    {
        /// <summary>
        /// Metoda obsługująca polecenie
        /// </summary>
        /// <param name="command">Polecenie do obsłużenia</param>
        /// <returns>Wynik operacji</returns>
        TResult Execute(TCommand command);
    }
}