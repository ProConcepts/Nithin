using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProConcepts.Controllers
{
   public class HomeController : Controller
    {
        public IActionResult Index()
        {
            //return View("~/Areas/Blogs/Views/Home/Index.cshtml");
            return View();
        }
    }
}
