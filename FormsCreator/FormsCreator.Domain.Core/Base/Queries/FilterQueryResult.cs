using System.Collections.Generic;

namespace FormsCreator.Domain.Core.Base.Queries
{
    /// <summary>
    /// Wynik kwerendy służącej do filtrowania zbiorów
    /// </summary>
    /// <typeparam name="TResult">Typ zbioru</typeparam>
    public class FilterQueryResult<TResult>
    {
        /// <summary>
        /// Wynik kwerendy
        /// </summary>
        public IEnumerable<TResult> Data { get; set; }
        
        /// <summary>
        /// Całkowita suma rekordów spełniającyh warunki fitru
        /// </summary>
        public int Total { get; set; }
    }
}