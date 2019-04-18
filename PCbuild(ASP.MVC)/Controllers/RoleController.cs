using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PCbuild_ASP.MVC_.Models.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;

namespace PCbuild_ASP.MVC_.Controllers
{
    [Authorize(Roles ="Admin")]
    public class RoleController : Controller
    {
        private ApplicationUserManager UserManager
        {
            get { return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
        }

        private ApplicationRoleManager RoleManager
        {
            get { return HttpContext.GetOwinContext().GetUserManager<ApplicationRoleManager>(); }
        }

        // GET: Role
        [Authorize(Roles ="Admin")]
        public ActionResult Index()
        {
            return View(RoleManager.Roles);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateRoleModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await RoleManager.CreateAsync(new ApplicationRole
                {
                    Name = model.Name,
                    Description = model.Description
              
                });
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Something is wrong");
                }

            }
            return View();
        }

        public async Task<ActionResult> Edit(string id)
        {
            ApplicationRole role = await RoleManager.FindByIdAsync(id);
            if (role != null)
            {
                string[] membersIDs = role.Users.Select(x => x.UserId).ToArray();
                IEnumerable<ApplicationUser> members = UserManager.Users.Where(x => membersIDs.Any(y => y == x.Id));

                IEnumerable<ApplicationUser> nonMembers = UserManager.Users.Except(members);

                return View(new EditRoleModel { Id = role.Id, Name = role.Name, Description = role.Description, Members=members, NonMembers = nonMembers });

            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> Edit(ModificationRoleModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationRole role = RoleManager.FindByName(model.RoleName);
                if (role != null)
                {
                    IdentityResult result;
                    foreach(string userId in model.IdsToAdd ?? new string[] { })
                    {
                        result = await UserManager.AddToRoleAsync(userId, model.RoleName);
                        if (!result.Succeeded)
                        {
                            return View("Error", result.Errors);
                        }
                    }
                    foreach (string userId in model.IdsToDelete ?? new string[] { })
                    {
                        result = await UserManager.RemoveFromRoleAsync(userId, model.RoleName);
                        if (!result.Succeeded)
                        {
                            //ModelState.AddModelError("", "Something is wrong");
                            return View("Error", result.Errors);
                        }
                    }
                    
                    role.Description = model.Description;
                    role.Name = model.RoleName;
                    result = await RoleManager.UpdateAsync(role);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Something is wrong");
                    }
                }
            }
            return View(model);
        }

        public async Task<ActionResult> Delete(string id)
        {
            ApplicationRole role = await RoleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await RoleManager.DeleteAsync(role);
            }
            return RedirectToAction("Index");
        }
    }
}