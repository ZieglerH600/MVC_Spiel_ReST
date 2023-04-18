using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;


namespace MVC_Spiel_ReST.Controllers
{
    public class HomeController : Controller
    {


        public HomeController()
        {
            //balaadasdasd

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }


    }
}