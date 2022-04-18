namespace FormsCreator.Domain.Core.Base
{
    /// <summary>
    /// Obiekt sortowania
    /// </summary>
    public class Sorting
    {
        /// <summary>
        /// Nazwa pola
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// Kierunek sortowania
        /// </summary>
        public SortDirection Direction { get; set; }
    }
}