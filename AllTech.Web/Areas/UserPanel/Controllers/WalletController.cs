using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AllTech.Services.Services.Interfaces;
using AllTech.ViewModels.Wallet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AllTech.Web.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Authorize]
    public class WalletController : Controller
    {
        private readonly IUserService _userService;
        public WalletController(IUserService userService)
        {
            _userService = userService;
        }

        [Route("UserPanel/Wallet")]
        public IActionResult Index()
        {

            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            ViewBag.WalletList = _userService.GetUserWallet(userId);
            return View();
        }

        [Route("UserPanel/Wallet")]
        [HttpPost]
        public IActionResult Index(ChargeWalletViewModel chargeWallet)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (!ModelState.IsValid)
            {
                ViewBag.WalletList = _userService.GetUserWallet(userId);
                return View(chargeWallet);
            }

            int walletId = _userService.ChargeWallet(userId,chargeWallet.Amount,"شارژ حساب");

            #region Online Payment

            var payment = new ZarinpalSandbox.Payment(chargeWallet.Amount);
            var res = payment.PaymentRequest("افزایش موجودی حساب", "https://localhost:44384/OnlinePayment/"+walletId);
            if (res.Result.Status==100)
            {
                return Redirect("https://sandbox.zarinpal.com/pg/StartPay/" + res.Result.Authority);
            }
            #endregion

            return View();
        }
    }
}