namespace FormsCreator.Domain.Core.Base.Commands
{
    /// <summary>
    /// Handler poleceń asynchronicznych, przychodzących na kolejkę komunikatów
    /// </summary>
    public interface IAsyncCommandHandler
    {
        /// <summary>
        /// Metoda wykonująca polecenia
        /// </summary>
        /// <param name="executionContext">Kontekst wykonania zawierający między innymi obiekt polecenia</param>
        void Execute(AsyncCommandExecutionContext executionContext);
    }
}