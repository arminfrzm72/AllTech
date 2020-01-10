using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AllTech.Services.Repositories;
using AllTech.Services.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AllTech.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly INewsRepository _newsRepository;
        private readonly IUserService _userService;
        public HomeController(INewsRepository newsRepository,IUserService userService)
        {
            _newsRepository = newsRepository;
            _userService = userService;
        }

        public IActionResult Index()
        {
            ViewBag.Slider = _newsRepository.GetNewsInSlider();
            return View(_newsRepository.GetLatesNews());
        }

        [Route("OnlinePayment/{id}")]
        public IActionResult OnlinePayment(int id)
        {
            if (HttpContext.Request.Query["Status"] != "" &&
                HttpContext.Request.Query["Status"].ToString().ToLower() == "ok" &&
                HttpContext.Request.Query["Authority"] != "")
            {
                string authority = HttpContext.Request.Query["Authority"];
                var wallet = _userService.GetWalletByWalletId(id);
                var payment = new ZarinpalSandbox.Payment(wallet.Amount);
                var res = payment.Verification(authority).Result;
                if (res.Status==100)
                {
                    ViewBag.Code = res.RefId;
                    ViewBag.IsSuccess = true;
                    wallet.IsPay = true;
                    _userService.UpdateWallet(wallet);
                }
            }
                return View();
        }
    }
}