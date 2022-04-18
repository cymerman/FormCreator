namespace FormsCreator.Domain.Core.Base.Commands
{
    /// <summary>
    /// Interfejs dla klasy wykonującej polecenia
    /// </summary>
    public interface ICommandExecutor
    {
        /// <summary>
        /// Metoda obsługująca wykonanie polecenia
        /// </summary>
        /// <param name="command">Polecenie do wykonania</param>
        /// <typeparam name="TCommand">Typ polecenia</typeparam>
        /// <typeparam name="TResult">Typ wyniku wykonanego polecenia</typeparam>
        /// <returns>Wynik operacji</returns>
        TResult Execute<TCommand, TResult>(TCommand command) where TCommand : ICommand<TResult>;
    }
}