using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using AutoMapper;
using GraphQlLibary.Domain.Interfaces.Services;
using GraphQlLibary.Domain.Models;
using GraphQlLibary.Models.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GraphQlLibary.Controllers
{
    public class AuthorizeController : Controller
    {
        private readonly IUserService _userService;

        private readonly IRoleService _roleService;

        private readonly IMapper _mapper;


        public AuthorizeController(IUserService userService, IMapper mapper, IRoleService roleService)
        {
            _userService = userService;
            _mapper = mapper;
            _roleService = roleService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel credentials)
        {
            var user = _userService.GetByMailAndPassword(credentials.Email, credentials.Password);

            if (user == null)
            {
                ModelState.AddModelError(nameof(credentials.Password), "Invalid login or password");

                return View(credentials);
            }

            var claims = GenerateClaims(user);
            var principal = CreatePrincipal(claims);

            HttpContext.SignInAsync(principal);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel userView)
        {
            if (userView == null)
            {
                return View();
            }

            if (userView.Password != userView.PasswordRepied)
            {
                ModelState.AddModelError("Key", "Passwords do not match");

                return View(userView);
            }

            var user = _mapper.Map<User>(userView);
            user.UserRole = new List<UserRole> { new UserRole { RoleId = 1 } };

            try
            {
                _userService.Insert(user);
            }
            catch (ArgumentException exception)
            {
                ModelState.AddModelError("Key", exception.Message);

                return View(userView);
            }

            return Login(new LoginViewModel { Email = userView.Email, Password = userView.Password });
        }

        private ClaimsPrincipal CreatePrincipal(IEnumerable<Claim> claims) =>
            new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));

        private IEnumerable<Claim> GenerateClaims(User user)
        {
            //var a = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var claims = new List<Claim>
                             {
                                 new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                                 new Claim(ClaimTypes.Name, user.Name),
                                 new Claim(ClaimTypes.Email, user.Email)
                             };

            var roles = user.UserRole.Select(x => _roleService.Get(x.RoleId)).ToList();

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role.Name)));

            return claims;
        }
    }
}
