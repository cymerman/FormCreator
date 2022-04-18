using System.Threading.Tasks;

namespace FormsCreator.Domain.Core.Base.Queries
{
    /// <summary>
    /// Interface dla klas przetwarzająch kwerendy 
    /// </summary>
    public interface IQueryProcessor
    {
        /// <summary>
        /// Metoda przetwarzająca kwerendę
        /// </summary>
        /// <param name="query">Kwerenda do przetworzenia, implementująca interfejs IQuery</param>
        /// <typeparam name="TQuery">Typ kwerendy</typeparam>
        /// <typeparam name="TResult">Typ elementu zwracanego</typeparam>
        /// <returns>Wynik kwerendy typu TResult</returns>
        Task<TResult> Process<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>;
    }
}