namespace FormsCreator.Domain.Core.Base.Queries
{
    /// <summary>
    /// Interfejs bazowy dla kwerend typu Filter
    /// </summary>
    /// <typeparam name="TResult">Typ obiektu jakie będzie posiadało IEnumerable w zwracamym wyniku</typeparam>
    public interface IFilterQuery<TResult> : IQuery<FilterQueryResult<TResult>>
    {
        /// <summary>
        /// Zakres wierszy
        /// </summary>
        RowsRange RowsRange { get; set; }
        
        /// <summary>
        /// Informacje na temat sortowania
        /// </summary>
        Sorting Sorting { get; set; }
    }
}