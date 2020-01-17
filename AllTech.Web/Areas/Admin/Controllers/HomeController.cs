using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AllTech.Services.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace AllTech.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [PermissionChecker(1)]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}