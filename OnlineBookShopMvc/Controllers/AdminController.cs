using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineBookShopMvc.Models;
using OnlineBookShopMvc.ViewModel;

namespace OnlineBookShopMvc.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<DefaultUser> _userManager;
        public AdminController(RoleManager<IdentityRole> roleManager, UserManager<DefaultUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        [HttpGet]
        public IActionResult ListAllRoles()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }
        [HttpGet]
        public IActionResult AddRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddRole(AddRoleVM addRoleVM)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new()
                {
                    Name = addRoleVM.RoleName
                };
                var result = await _roleManager.CreateAsync(identityRole);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListAllRoles");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(addRoleVM);
        }

        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                ViewData["ErrorMessage"] = $"No Role with Id - {id} was found";
                return View("Error");
            }
            EditRoleVM editRoleVM = new()
            {
                Id = role.Id,
                RoleName = role.Name,
            };
            foreach (var user in _userManager.Users)
            {
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    editRoleVM.Users.Add(user.UserName);
                }
            }
            return View(editRoleVM);

        }
        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleVM editRoleVM)
        {
			var role = await _roleManager.FindByIdAsync(editRoleVM.Id );

			if (role == null)
			{
				ViewData["ErrorMessage"] = $"No Role with Id - {editRoleVM.Id} was found";
				return View("Error");
            }
            else
            {
                role.Name = editRoleVM.RoleName;
                var result = await _roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListAllRoles");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(editRoleVM);
		}

        [HttpGet]
        public async Task<IActionResult> EditUserInRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            ViewData["roleId"] = id;
            ViewData["roleName"] = role.Name;

            if(role == null)
            {
                ViewData["ErrorMessage"] = $"No role with id {id} was found";
                return View("Error");
            }

            var model = new List<UserRoleVM>();
            foreach (var user in _userManager.Users)
            {
                UserRoleVM userRoleVM = new()
                {
                    Id = user.Id,
                    Name = user.UserName
                };
                if(await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userRoleVM.IsSelected = true;
                }
                else
                {
                    userRoleVM.IsSelected = false;
                }
                model.Add(userRoleVM);
            }
            return View(model); 

        }

        [HttpPost]
        public async Task<IActionResult> EditUserInRole(List<UserRoleVM> model, string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if(role == null)
            {
                ViewData["ErrorMessage"] = $"No role with id {id} was found ";
                return View("Error");
            }

            for(int i = 0; i < model.Count; i++) 
            {
                var user = await _userManager.FindByIdAsync(model[i].Id);

                if (model[i].IsSelected && !(await _userManager.IsInRoleAsync(user, role.Name))) 
                { 
                    await _userManager.AddToRoleAsync(user, role.Name);
                }
                else if(!model[i].IsSelected && (await _userManager.IsInRoleAsync(user, role.Name)))
                {
                    await _userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }
            }
            return RedirectToAction("EditRole", new {Id = id});
        }
    }
}
