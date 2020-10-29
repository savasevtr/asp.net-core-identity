using System;
using System.Collections.Generic;
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
    public class RoleController : Controller
    {
        private readonly RoleManager<AppRole> _roleManager;

        public RoleController(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View(_roleManager.Roles.ToList());
        }

        public IActionResult Create()
        {
            return View(new RoleViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppRole role = new AppRole
                {
                    Name = model.Name
                };

                var identityResult = await _roleManager.CreateAsync(role);

                if (identityResult.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                foreach (var item in identityResult.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }

            return View(model);
        }

        public IActionResult Edit(int id)
        {
            var role = _roleManager.Roles.FirstOrDefault(x => x.Id == id);

            RoleEditViewModel model = new RoleEditViewModel
            {
                Id = role.Id,
                Name = role.Name
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RoleEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = _roleManager.Roles.Where(x => x.Id == model.Id).FirstOrDefault();
                role.Name = model.Name;

                var identityResult = await _roleManager.UpdateAsync(role);

                if (identityResult.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                foreach (var item in identityResult.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }

            return View(model);
        }


    }
}