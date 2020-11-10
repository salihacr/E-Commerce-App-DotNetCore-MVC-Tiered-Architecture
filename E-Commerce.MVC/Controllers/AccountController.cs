using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using E_Commerce.MVC.Models;
using Microsoft.AspNetCore.Identity;
using E_Commerce.MVC.Identity;
using Newtonsoft.Json;
using E_Commerce.MVC.EmailServices;
using E_Commerce.MVC.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace E_Commerce.MVC.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class AccountController : Controller
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signManager;
        private IEmailSender _emailSender;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _signManager = signInManager;
            _emailSender = emailSender;
        }
        [AllowAnonymous]
        public IActionResult Login(string ReturnUrl = null)
        {
            return View(new LoginModel()
            {
                ReturnUrl = ReturnUrl
            });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            //var user = await _userManager.FindByNameAsync(model.UserName);
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "Bu kullanıcı adına ait hesap bulunamadı.");
                return View(model);
            }
            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                ModelState.AddModelError("", "Hesabınızı onaylayın maile bak.");
                return View(model);
            }
            var result = await _signManager.PasswordSignInAsync(user, model.Password, true, false);

            if (result.Succeeded)
            {
                return Redirect(model.ReturnUrl ?? "~/");
            }
            ModelState.AddModelError("", "Kullanıcı adı veya parola yanlış.");
            return View(model);
        }
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = new User()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
                Email = model.Email
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                // generate token
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var url = Url.Action("ConfirmEmail", "Account", new
                {
                    userId = user.Id,
                    token = code
                });
                Console.Write(url);
                // email
                var siteUrl = "https://localhost:5001";
                var html = $"lütfen email hesabınızı onaylamak için <a href='{siteUrl + url}'>linke</a> tıklayınız.";
                await _emailSender.SendEmailAsync(model.Email, "hesabınızı onaylayınız.", html);

                return RedirectToAction("Login", "Account");
            }
            ModelState.AddModelError("", "Bilinmeyen bir hata oldu lütfen daha sonra tekrar deneyiniz.");
            return View(model);
        }
        public async Task<IActionResult> Logout()
        {
            await _signManager.SignOutAsync();
            return Redirect("~/");
        }
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                CreateMessage("geçersiz token", "geçersiz token", "danger");
                return View();
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    CreateMessage("hesabınız onaylandı", "hesabınız onaylandı.", "success");
                    return View();
                }
            }
            CreateMessage("böyle biri yok", "böyle biri yok", "warning");
            return View();
        }
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string Email)
        {
            if (string.IsNullOrEmpty(Email))
            {
                return View();
            }
            var user = await _userManager.FindByEmailAsync(Email);
            if (user == null)
            {
                return View();
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            // generate token
            var url = Url.Action("ResetPassword", "Account", new
            {
                userId = user.Id,
                token = token
            });
            Console.Write(url);
            // email
            var siteUrl = "https://localhost:5001";
            var html = $"lütfen hesap şifrenizi değiştirmek için <a href='{siteUrl + url}'>linke</a> tıklayınız.";
            await _emailSender.SendEmailAsync(Email, "Şifre Değiştirme.", html);

            return View();
        }
        [AllowAnonymous]
        public IActionResult ResetPassword(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return RedirectToAction("Home", "Index");
            }
            var model = new ResetPasswordModel { Token = token };
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return RedirectToAction("Home", "Index");
            }
            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("Login", "Account");
            }
            return View(model);
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
        public void CreateMessage(string title, string message, string alerttype)
        {
            TempData.Put("message", new AlertMessage()
            {
                Title = title,
                Message = message,
                AlertType = alerttype
            });
        }
    }
}