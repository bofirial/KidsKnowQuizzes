using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace KidsKnowQuizzes.Controllers
{
    [Route("/")]
    [Route("/[controller]")]
    public class ChromeCastController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
