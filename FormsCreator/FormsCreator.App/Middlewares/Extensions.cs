using FormsCreator.Domain.Security;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace FormsCreator.App.Middlewares
{
    public static class Extensions
    {
        internal static IServiceCollection AddAuth(this IServiceCollection services)
        {
            services
                .AddAuthentication(o =>
                {
                    o.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    o.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                })
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, cookieAuthenticationOptions =>
                {
                    cookieAuthenticationOptions.SlidingExpiration = false;
                    cookieAuthenticationOptions.Cookie.SameSite = SameSiteMode.Unspecified;
                    cookieAuthenticationOptions.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                    cookieAuthenticationOptions.Cookie.Name = ".FormCreator.Auth";
                    cookieAuthenticationOptions.Cookie.Path = "/";
                    cookieAuthenticationOptions.Cookie.HttpOnly = true;
                    cookieAuthenticationOptions.EventsType = typeof(CookieAuthenticationBaseEventHandler);
                    cookieAuthenticationOptions.CookieManager = new ChunkingCookieManager();
                });

            return services;
        }
    }
}