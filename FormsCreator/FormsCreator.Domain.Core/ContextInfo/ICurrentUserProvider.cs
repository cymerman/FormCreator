namespace FormsCreator.Domain.Core.ContextInfo
{
    /// <summary>
    /// Serwis zwracający informacje o aktualnie zalogowanym użytkowniku
    /// </summary>
    public interface ICurrentUserProvider
    {
        /// <summary>
        /// Metoda do pobierania Id aktualnegoużytkownika
        /// </summary>
        /// <returns>Id użytkownika</returns>
        long? GetUserId();
    }
}