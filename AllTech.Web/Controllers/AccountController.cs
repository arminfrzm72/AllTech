using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AllTech.DomainClasses.User;
using AllTech.Services.Services.Interfaces;
using AllTech.Utilities.Convertor;
using AllTech.Utilities.Generator;
using AllTech.Utilities.Security;
using AllTech.Utilities.Senders;
using AllTech.ViewModels.UserAccount;
using AllTech.Web.External_Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace AllTech.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IViewRenderService _viewRender;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AccountController(IUserService userService,IViewRenderService viewRender,SignInManager<ApplicationUser> signInManager) 
        {
            _userService = userService;
            _viewRender = viewRender;
            _signInManager = signInManager;
        }

        #region Register
 
        [Route("SignUp")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("SignUp")]
        public IActionResult Register(RegisterViewModel register)
        {
            if (!ModelState.IsValid)
            {
                return View(register);
            }
            if (_userService.IsEmailExist(FixedText.FixedEmail(register.Email)))
            {
                ModelState.AddModelError("Email", "ایمیل وارد شده قبلا ثبت شده است");
                return View(register);
            }
            if (_userService.IsUsernameExist(register.UserName))
            {
                ModelState.AddModelError("UserName", "نام کاربری وارد شده قبلا ثبت شده است");
                return View(register);
            }

            //register user
            AllTech.DomainClasses.User.User user = new User()
            {
                FirstName=register.FirstName,
                LastName=register.LastName,
                UserName=register.UserName,
                Email=FixedText.FixedEmail(register.Email),
                Password=PasswordHelper.EncodePasswordMd5(register.Password),
                ActiveCode=NameGenerator.GenerateUniqueCode(),
                UserAvatar="Default.jpg",
                IsActive=false,              
            };

            string bodyOfActiveEmail = _viewRender.RenderToStringAsync("_ActiveAccount", user);
            SendEmail.Send(user.Email, "ایمیل فعالسازی", bodyOfActiveEmail);

            _userService.AddUser(user);
            return View("SuccessRegister", user);

        }

        #endregion

        #region Login

        [Route("Login")]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("Login")]
        public ActionResult Login(LoginViewModel login)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }
            var user = _userService.LoginUser(login);
            if (user!=null)
            {
                if (user.IsActive)
                {
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier,user.UserID.ToString()),
                        new Claim(ClaimTypes.Name,user.UserName)
                    };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    var properties = new AuthenticationProperties
                    {
                        IsPersistent = login.RememberMe
                    };
                    HttpContext.SignInAsync(principal, properties);
                    ViewBag.IsSuccess = true;
                    return View();
                }
                ModelState.AddModelError("UserName", "حساب کاربری شما فعال نیست");
            }
            ModelState.AddModelError("UserName", "نام کاربری و یا رمز عبور اشتباه است");
            return View(login);
        }

        #endregion

        #region Logout

        [Route("Logout")]
        public ActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }

        #endregion

        #region ActiveAccount

        //[Route(ActiveAccount/{id})]
        public ActionResult ActiveAccount(string id)
        {
            ViewBag.IsActive = _userService.ActiveAccount(id);
            return View();
        }

        #endregion

        #region ForgotPassword

        [Route("ForgotPassword")]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [Route("ForgotPassword")]
        public ActionResult ForgotPassword(ForgotPasswordViewModel forgot)
        {
            if (!ModelState.IsValid)
            {
                return View(forgot);
            }
            string fixedEmail = FixedText.FixedEmail(forgot.Email);
            var user = _userService.GetUserByEmail(fixedEmail);
            if (user==null)
            {
                ModelState.AddModelError("Email", "کاربری با ایمیل وارد شده در سیستم یافت نشد");
                return View(forgot);
            }
            string bodyOfForgotPasswordEmail = _viewRender.RenderToStringAsync("_ForgotPassword", user);
            SendEmail.Send(forgot.Email,"بازیابی کلمه عبور",bodyOfForgotPasswordEmail);
            ViewBag.IsSuccess = true;
            return View();
        }

        #endregion

        #region ResetPassword

        public ActionResult ResetPassword(string id)
        {
            return View(new ResetPasswordViewModel
            {
                ActiveCode = id
            });
        }

        [HttpPost]
        public ActionResult ResetPassword(ResetPasswordViewModel reset)
        {
            if (!ModelState.IsValid)
            {
                return View(reset);
            }
            var user = _userService.GetUserByActiveCode(reset.ActiveCode);
            if (user==null)
            {
                ModelState.AddModelError("RePassword","کد شما برای تغییر کلمه عبور صحیح نیست! لطفا از طریق لینک موجود در ایمیل ارسال شده اقدام کنید!");
                return View(reset);
            }
            string hashPassword = PasswordHelper.EncodePasswordMd5(reset.Password);
            user.Password = hashPassword;
            _userService.UpdateUser(user);
            return Redirect("/Login");
        }

        #endregion

        #region External login

        [Route("Provider/{provider}")]
        public IActionResult GetProvider(string provider)
        {
            var redirectUrl = Url.RouteUrl("ExternalLogin", Request.Scheme);

            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);

            return Challenge(properties,provider);
        }

        [Route("ExternalLogin",Name ="ExternalLogin")]
        public IActionResult ExternalLogin()
        {
            ViewBag.IsSuccess = null;
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var user = _userService.GetUserByEmailForExternalLogin(userEmail);
            if (user!=null)
            {
                if (user.IsActive)
                {
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier,user.UserID.ToString()),
                        new Claim(ClaimTypes.Name,user.UserName)
                    };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    //var properties = new AuthenticationProperties
                    //{
                    //    IsPersistent = true
                    //};
                    HttpContext.SignInAsync(principal);
                    ViewBag.IsSuccess =true;
                    return Redirect("/");
                }
                
            }
            if (user==null)
            {
                ViewBag.IsSuccess = null;
                Logout();
                return View("login");
            }
            return RedirectToRoute("Index");
        }
        #endregion
    }
}