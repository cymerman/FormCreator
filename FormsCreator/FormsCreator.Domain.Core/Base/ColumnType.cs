namespace FormsCreator.Domain.Core.Base
{
    /// <summary>
    /// Typ kolumny rejestru
    /// </summary>
    public enum ColumnType
    {
        /// <summary>
        /// Tekstowa
        /// </summary>
        Text = 1,
        
        /// <summary>
        /// Liczba dziesiętna
        /// </summary>
        Decimal,
        
        /// <summary>
        /// Liczba całkowita
        /// </summary>
        Integer,
        
        /// <summary>
        /// Data
        /// </summary>
        Date,
        
        /// <summary>
        /// Prawda/fałsz
        /// </summary>
        Bool
    }
}