using System.Threading.Tasks;
using Autofac;
using FormsCreator.Domain.Core.Base.Queries;

namespace FormsCreator.Domain
{
    /// <inheritdoc />
    public class QueryProcessor : IQueryProcessor
    {
        private readonly IComponentContext _context;

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="context">Kontekst w jakim będą rozwiązywane zależności</param>
        public QueryProcessor(IComponentContext context)
        {
            _context = context;
        }

        /// <inheritdoc />
        public async Task<TResult> Process<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>
        {
            var handler = _context.Resolve<IQueryHandler<TQuery, TResult>>();

            return await handler.Process(query);
        }
    }
}