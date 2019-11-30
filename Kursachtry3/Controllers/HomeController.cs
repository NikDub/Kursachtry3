using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Kursachtry3.Models;
using Microsoft.EntityFrameworkCore;

namespace Kursachtry3.Controllers
{
    public class HomeController : Controller
    {
        private BDModel.AvtoModel _context;

        public HomeController(BDModel.AvtoModel context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var avtos = _context.avtoes.Where(e => e.when_cancellation == null && e.when_sell==null).ToList();
            if (avtos == null)
            {
                return Redirect("/shared/errorpage");
            }
            return View(avtos);
        }

        [HttpPost]
        public IActionResult Index(int Id)
        {
            return RedirectToAction("ShowAvto", "Shows", new { id = Id });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
