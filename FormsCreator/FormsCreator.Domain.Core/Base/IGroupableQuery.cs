namespace FormsCreator.Domain.Core.Base
{
    /// <summary>
    /// Interfejs, który muszą implementować wszystkie klasy przetwarzane przez QueryProcessor, które mają umożliwiać generyczne filtrowanie
    /// </summary>    
    public interface IGroupableQuery
    {
        /// <summary>
        /// Pole do grupowania
        /// </summary>
        string GroupFieldName { get; set; }
    }
}