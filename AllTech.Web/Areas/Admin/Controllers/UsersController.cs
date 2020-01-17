using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AllTech.DomainClasses.User;
using AllTech.Services.Attributes;
using AllTech.Services.Services.Interfaces;
using AllTech.ViewModels.UserAccount;
using Microsoft.AspNetCore.Mvc;

namespace AllTech.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [PermissionChecker(1)]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly IPermissionService _permissionService;
        public UsersController(IUserService userService,IPermissionService permissionService)
        {
            _userService = userService;
            _permissionService = permissionService;
        }
        public IActionResult Index(int pageId = 1, string filterUserName = "", string filterEmail = "")
        {

            return View(_userService.GetUsers(pageId,filterUserName,filterEmail));
        }

        [Route("Admin/CreateUser")]
        public IActionResult CreateUser()
        {
            ViewBag.Roles = _permissionService.GetRoles();
  
            return View();
        }

        [Route("Admin/CreateUser")]
        [HttpPost]
        public IActionResult CreateUser(CreateUserViewModel createUser,List<Roles> selectedRoles)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Roles = _permissionService.GetRoles();
                return View(createUser);
            }

            int userId = _userService.AddUserFromAdmin(createUser);
            _permissionService.AddRolesToUser(selectedRoles, userId);
            return Redirect("Users");
        }

        [Route("Admin/EditUser/{id}")]
        public IActionResult EditUser(int id)
        {
            ViewBag.Roles = _permissionService.GetRoles();
            return View(_userService.GetUserForEditByAdmin(id));
        }

        //Edit user
        [Route("Admin/EditUser/{id}")]
        [HttpPost]
        public IActionResult EditUser(EditUserViewModel editUser,List<Roles> SelectedRoles)
        {
            
            if (!ModelState.IsValid)
            {
                ViewBag.Roles = _permissionService.GetRoles();
                return View(editUser);
            }
            _userService.EditUserFromAdmin(editUser);
            _permissionService.EditUserRoles(editUser.UserId, SelectedRoles);
            return Redirect("Users");
        }


    }
}