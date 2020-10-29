using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UdemyIdentity.Context;
using UdemyIdentity.Models;

namespace UdemyIdentity.Controllers
{
    [Authorize]
    public class PanelController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        public PanelController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            return View(user);
        }

        public async Task<IActionResult> UpdateUser()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            UserUpdateViewModel model = new UserUpdateViewModel
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                PictureUrl = user.PictureUrl
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser(UserUpdateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);

                if (model.PictureFile != null)
                {
                    var currentDirectory = Directory.GetCurrentDirectory();
                    var fileExtension = Path.GetExtension(model.PictureFile.FileName);
                    var pictureFileName = Guid.NewGuid() + fileExtension;
                    var filePath = currentDirectory + "/wwwroot/img/" + pictureFileName; 

                    using var fileStream = new FileStream(filePath, FileMode.Create);

                    await model.PictureFile.CopyToAsync(fileStream);
                    user.PictureUrl = pictureFileName;
                }

                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                user.PhoneNumber = model.PhoneNumber;

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }

            return View(model);
        }

        [AllowAnonymous] // herkese açık
        public IActionResult Index2()
        {
            return View();
        }
    }
}