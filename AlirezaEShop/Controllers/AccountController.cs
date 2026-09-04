using AlirezaEShop.Data.Repositories;
using AlirezaEShop.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Security.Claims;

namespace AlirezaEShop.Controllers
{
    public class AccountController : Controller
    {
        private IUserRepository _userRepository;
        public AccountController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        #region Register
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel Register)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            User user = new User()
            {
                Email = Register.Email,
                Password = Register.Password,
                IsAdmin = false,
                RegisterDate = DateTime.Now
            };
            _userRepository.AddUser(user);
            return View("Success", Register);
        }

        public IActionResult VerfyEmail(string email)
        {
            if (_userRepository.IsExistUserByEmail(email))
            {
                return Json($"ایمیل {email} تکراری میباشد");
            }
            return Json(true);
        }
        #endregion

        #region Login

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }
            var user = _userRepository.GetUserForLogin(loginViewModel.Email.ToLower(), loginViewModel.Password);
            if (user == null)
            {
                ModelState.AddModelError("Email", "اطلاعات وارد شده صحیح نمیباشد");
                return View(loginViewModel);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.userId.ToString()),
                new Claim(ClaimTypes.Name, user.Email),
                new Claim("IsAdmin", user.IsAdmin.ToString())
            };


            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            var properties = new AuthenticationProperties
            {
                IsPersistent = loginViewModel.RememberMe
            };

            HttpContext.SignInAsync(principal, properties);
            return Redirect("/");
        }
        #endregion

        #region Logout
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
        #endregion
    }
}

