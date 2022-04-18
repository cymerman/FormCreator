using System.Threading.Tasks;
using FormsCreator.Domain.Core.Account.Dto;
using FormsCreator.Domain.Core.Account.Queries;
using FormsCreator.Domain.Core.Base.Queries;
using FormsCreator.Domain.Core.ContextInfo;
using FormsCreator.Domain.Core.Exceptions;
using FormsCreator.Domain.Db;
using Microsoft.EntityFrameworkCore;

namespace FormsCreator.Domain.Account.QueryHandlers
{
    public class GetAccountDataQueryHandler : IQueryHandler<GetAccountDataQuery, AccountDto>
    {
        private readonly Context _context;
        private readonly ICurrentUserProvider _currentUserProvider;

        public GetAccountDataQueryHandler(Context context, ICurrentUserProvider currentUserProvider)
        {
            _context = context;
            _currentUserProvider = currentUserProvider;
        }

        public async Task<AccountDto> Process(GetAccountDataQuery query)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == _currentUserProvider.GetUserId().Value);

            if (user == null)
            {
                throw new NotFoundException();
            }

            return new AccountDto()
            {
                Login = user.Login
            };
        }
    }
}