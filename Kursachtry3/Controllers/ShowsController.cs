using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kursachtry3.Models.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kursachtry3.Controllers
{
    public class ShowsController : Controller
    {
        private BDModel.AvtoModel _context;

        public ShowsController(BDModel.AvtoModel context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult ShowAvto(int id)
        {
            return View(AvtoViewModel.AvtoViewReturn(id, _context)); 
        }

        [HttpGet]
        public IActionResult ShowsSoldAvto()
        {
            var avtos = _context.avtoes.Where(e => e.when_cancellation != null || e.when_sell != null).ToList();
            if (avtos == null)
            {
                return Redirect("/shared/errorpage");
            }
            return View(avtos);
        }


    }
}