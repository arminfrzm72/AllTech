using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AllTech.Services.Services.Interfaces;
using AllTech.ViewModels.UserAccount;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AllTech.Web.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IUserService _userService;
        public HomeController(IUserService userService)
        {
            _userService = userService;
        }
        
        public IActionResult Index()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return View(_userService.GetUserInformation(userId));
        }

        [Route("UserPanel/EditProfile")]
        public ActionResult EditProfile()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return View(_userService.GetUserInformationToEdit(userId));
        }

        [Route("UserPanel/EditProfile")]
        [HttpPost]
        public ActionResult EditProfile(EditProfileViewModel editProfile)
        {
            if (!ModelState.IsValid)
            {
                return View(editProfile);
            }
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;           
           _userService.EditProfile(userId,editProfile);
            ViewBag.IsSuccess = true;                      
            return View(editProfile);
        }

        [Route("UserPanel/ChangePassword")]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [Route("UserPanel/ChangePassword")]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewModel changePassword)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (!ModelState.IsValid)
            {
                return View(changePassword);
            }
            
            if (!_userService.CompareOldPassword(userId, changePassword.OldPassword))
            {
                ModelState.AddModelError("OldPassword", "کلمه عبور فعلی صحیح نمی‌باشد");
                return View(changePassword);
            }
            _userService.ChangePassword(userId,changePassword.Password);
            ViewBag.IsSuccess = true;
            return View();
        }
    }
}