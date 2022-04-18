using System.Threading.Tasks;

namespace FormsCreator.Domain.Core.Base.Queries
{
    /// <summary>
    /// Interface dla handlerów obsługujących kwerendy
    /// </summary>
    /// <typeparam name="TQuery">Typ kwerendy obsługiwanej przez handler</typeparam>
    /// <typeparam name="TResult">Typ wyniku zwracanego przez handler</typeparam>
    public interface IQueryHandler<in TQuery, TResult> : IHandler where TQuery : IQuery<TResult>
    {
        /// <summary>
        /// Metoda przetwarzająca kwerendę
        /// </summary>
        /// <param name="query">Obiekt kwerendy</param>
        /// <returns>Wynik przetworzenia kwerendy</returns>
        Task<TResult> Process(TQuery query);
    }
}