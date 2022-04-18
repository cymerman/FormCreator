using System.Linq;
using System.Threading.Tasks;
using FormsCreator.Domain.Core.Base;
using FormsCreator.Domain.Core.Base.Queries;
using Microsoft.EntityFrameworkCore;

namespace FormsCreator.Domain
{
    /// <inheritdoc />
    public abstract class FilterQueryHandlerBase<TQuery, TResult> : IQueryHandler<TQuery, FilterQueryResult<TResult>>
        where TQuery : IFilterQuery<TResult>
    {
        /// <inheritdoc />
        public virtual async Task<FilterQueryResult<TResult>> Process(TQuery query)
        {
            var result = PrepareQuery(query);

            result = ApplyFilter(result, query);

            if (query is IGroupableQuery groupableQuery)
            {
                result = ApplySorting(result, query.Sorting, groupableQuery.GroupFieldName);
            }
            else
            {
                result = ApplySorting(result, query.Sorting);
            }

            var totalCount = result.Count();

            result = ApplyPaging(result, query.RowsRange);
            
            var resultList = await result.ToListAsync();

            return new FilterQueryResult<TResult>
            {
                Total = totalCount,
                Data = resultList
            };
        }

        /// <summary>
        /// Metoda paginująca wyniki    
        /// </summary>
        /// <param name="result">Queryable z wynikowego obiektu</param>
        /// <param name="rowsRange">Obiekt zawierający szczegóły potrzebne do paginacji</param>
        /// <returns></returns>
        protected virtual IQueryable<TResult> ApplyPaging(IQueryable<TResult> result, RowsRange rowsRange)
        {
            if (rowsRange == null)
            {
                return result;
            }

            if (result.Count() < rowsRange.Skip)
            {
                rowsRange.Skip = 0;
            }
            
            result = result.Skip(rowsRange.Skip);

            if (rowsRange.Take > 0)
            {
                result = result.Take(rowsRange.Take);
            }

            return result;
        }

        /// <summary>
        /// Metoda która powinna przygotowywać queryable które dalej będzie filtrowane i sortowane
        /// </summary>
        /// <param name="query">Obiekt zapytania który zawiera podstawowe parametry filtrowania, sortowania itp potrzebne do zbudowania queryable</param>
        /// <returns>Iqueryable z utworzonym w metodzie</returns>
        protected abstract IQueryable<TResult> PrepareQuery(TQuery query);

        /// <summary>
        /// Nałożene filtrów na obiekt queryable
        /// </summary>
        /// <param name="result">Queryable z wynikiem query</param>
        /// <param name="query">Obiekt zawierający informacje jak przefiltrować queryable</param>
        /// <returns>Queryable z wynikiem query</returns>
        protected abstract IQueryable<TResult> ApplyFilter(IQueryable<TResult> result, TQuery query);

        /// <summary>
        /// Metoda która powinna nałożyć sortowanie na obiekt queryable
        /// </summary>
        /// <param name="query">Queryable z wynikiem query</param>
        /// <param name="sorting">Obiekt zawierający inforamcje o tym jak posortować queryable</param>
        /// <param name="groupFiledName">Kolumna po której mają być grupowane dane</param>
        /// <returns>Queryable z wynikiem query</returns>
        protected abstract IQueryable<TResult> ApplySorting(IQueryable<TResult> query, Sorting sorting, string groupFiledName = null);
    }
}