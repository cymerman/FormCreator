using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using FormsCreator.Domain.Core.Security.Dto;
using FormsCreator.Domain.Core.Security.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FormsCreator.App.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        
        /// <summary>
        /// Czy jest 
        /// </summary>
        /// <returns>Zwraca true </returns>
        [HttpGet]
        [Route("is-authenticated")]
        [Authorize]
        public IActionResult IsAuthenticated()
        {
            return Ok(HttpContext.User.Identity is {IsAuthenticated: true});
        }

        /// <summary>
        /// Metoda służy do wylogowywania z aplikacji
        /// </summary>
        /// <returns>Odpowiedz http ze statusem 200</returns>
        [HttpDelete]
        [Route("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return Ok();
        }


        /// <summary>
        /// Metoda do logowaniaa
        /// </summary>
        /// <param name="signInDto">Obiekt z info do logowania</param>
        /// <returns>Lista</returns>
        [HttpPost]
        [Route("sign-in")]
        public async Task<IActionResult> SignIn([FromBody] SignInDto signInDto)
        {
            var userId = await _authService.Login(signInDto);
            
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, userId.ToString()),
                new("Login", signInDto.Login)
            };
            
            await CreateAuthTicket(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));

            return Ok();
        }
        
        private async Task CreateAuthTicket(IIdentity claimsIdentity)
        {
            var authProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
                IsPersistent = true,
                IssuedUtc = DateTimeOffset.UtcNow,
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30)
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }
    }
}