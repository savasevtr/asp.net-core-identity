using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UdemyIdentity.Context;
using UdemyIdentity.Models;

namespace UdemyIdentity.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public HomeController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(UserSignInViewModel model)
        {
            if (ModelState.IsValid)
            {
                var identityResult = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, true);
                // identityResult.IsNotAllowed // iki aşamalı doğrulamada kullanılıyor

                if (identityResult.IsLockedOut)
                {
                    var lockoutEndDate = await _userManager.GetLockoutEndDateAsync(await _userManager.FindByNameAsync(model.UserName));

                    var remainingTime = lockoutEndDate.Value.Minute - DateTime.Now.Minute;

                    ModelState.AddModelError("", $"3 kez yanlış girdiğiniz için hesabınız kitlenmiştir! Hesabınız { remainingTime } dakika kilitlenmiştir!");

                    return View("Index", model);
                }

                if (identityResult.Succeeded)
                {
                    return RedirectToAction("Index", "Panel");
                }

                var accessFailedCount = await _userManager.GetAccessFailedCountAsync(await _userManager.FindByNameAsync(model.UserName));

                ModelState.AddModelError("", $"Kullanıcı adı veya şifre hatalı! { 3 - accessFailedCount } hakkınız kaldı");
            }

            return View("Index", model);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserSignUpViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = new AppUser()
                {
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    UserName = model.UserName
                };

                var result = await _userManager.CreateAsync(appUser, model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);

                    return View();
                }
            }

            return View();
        }
    }
}