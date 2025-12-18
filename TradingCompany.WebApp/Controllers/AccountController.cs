using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TradingCompany.BL.Interfaces;
using TradingCompany.DTO;

namespace TradingCompany.WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthManager _authManager;

        public AccountController(IAuthManager authManager)
        {
            _authManager = authManager;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
       
        public async Task<IActionResult> Login(string username, string password, string returnUrl = null)
        {
            if (!_authManager.Login(username, password))
            {
                ModelState.AddModelError("", "Невірний логін або пароль");
                return View();
            }

            var user = _authManager.GetUserByLogin(username);

            // Створюємо claims
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, user.Login)
    };

            // Додаємо ролі
            if (user.Privileges != null)
            {
                foreach (var privilege in user.Privileges)
                {
                    claims.Add(new Claim(ClaimTypes.Role, privilege.Name));
                }
            }

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            //  Якщо ReturnUrl був переданий — повертаємо на нього
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            // Інакше — на головну
            return RedirectToAction("Index", "Home");
        }


        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
