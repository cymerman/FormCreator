using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace FormsCreator.App.Middlewares
{
    /// <summary>
    /// Event handler do weryfikowania ciasteczka
    /// </summary>
    public class CookieAuthenticationBaseEventHandler : CookieAuthenticationEvents
    {
        public override async Task ValidatePrincipal(CookieValidatePrincipalContext context)
        {
            var userId = context.Principal?
                .Claims
                .FirstOrDefault(x=>x.Type == ClaimTypes.NameIdentifier)
                ?.Value;
            
            if (string.IsNullOrEmpty(userId))
            {
                context.RejectPrincipal();

                await context.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            }
        }
    }
}