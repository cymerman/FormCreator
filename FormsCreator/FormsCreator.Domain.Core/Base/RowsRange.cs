namespace FormsCreator.Domain.Core.Base
{
    /// <summary>
    /// Obiekt paginacji, odpowiedzialny za zakres wierszy do wyświetlenia
    /// </summary>
    public class RowsRange
    {
        /// <summary>
        /// Liczba wierszy do pominięcia
        /// </summary>
        public int Skip { get; set; }

        /// <summary>
        /// Liczba wierszy do wzięcia
        /// </summary>
        public int Take { get; set; }
    }
}