namespace FormsCreator.Domain.Core.Base.Commands
{
    /// <summary>
    /// Kontekst wykonywania poleceń asynchornicznych
    /// </summary>
    public class  AsyncCommandExecutionContext
    {
        /// <summary>
        /// Obiekt parametrów przekazanych w kontekście
        /// </summary>
        public object Command { get; set; }
    }
}