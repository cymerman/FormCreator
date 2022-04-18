using System.Linq;
using System.Security.Claims;
using FormsCreator.Domain.Core.ContextInfo;
using Microsoft.AspNetCore.Http;

namespace FormsCreator.Domain.ContextInfo
{
    public class CurrentUserProvider : ICurrentUserProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <inheritdoc/>
        public long? GetUserId()
        {
            if (_httpContextAccessor?.HttpContext?.User?.Identity == null)
            {
                return null;
            }

            if (!_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                return null;
            }

            var id = _httpContextAccessor.HttpContext
                .User
                .Claims
                .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }

            return long.Parse(id);
        }
    }
}