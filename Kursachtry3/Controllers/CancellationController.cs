using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kursachtry3.Models.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kursachtry3.Controllers
{
    public class CancellationController : Controller
    {
        private BDModel.AvtoModel _context;
        public CancellationController(BDModel.AvtoModel context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CancelAvto(int Id_sold, string action,AvtoViewModel model)
        {
            if (action == "Accept Cancellation")
            {
                var a = await _context.avtoes.
                   Include(e => e.repairs).ThenInclude(e => e.work_data).ThenInclude(e => e.worker).
                   Include(e => e.repairs).ThenInclude(e => e.work_data).ThenInclude(e => e.profession).
                   Include(e => e.repairs).ThenInclude(e => e.type_of_repair).
                   Include(e => e.department).
                   Include(e => e.avto_worker).ThenInclude(e => e.work_data).ThenInclude(e => e.worker).
                   Include(e => e.avto_worker).ThenInclude(e => e.work_data).ThenInclude(e => e.profession).
                   Include(e => e.avto_worker).ThenInclude(e => e.divisions).ThenInclude(e => e.way).
                   Include(e => e.brand).ThenInclude(e => e.manufacturer).
                   Include(e => e.brand).ThenInclude(e => e.type_of_avto).
                   Where(e => e.Avto_Key == model.Avto_Key).FirstOrDefaultAsync();

                a.when_cancellation = model.when_cancellation;
                _context.Update(a);
                await _context.SaveChangesAsync();

                return RedirectToAction("ShowAvto", "Shows", new { id = model.Avto_Key });
            }
            else if (action == "Accept Sold")
            {
                var a = await _context.avtoes.
                  Include(e => e.repairs).ThenInclude(e => e.work_data).ThenInclude(e => e.worker).
                  Include(e => e.repairs).ThenInclude(e => e.work_data).ThenInclude(e => e.profession).
                  Include(e => e.repairs).ThenInclude(e => e.type_of_repair).
                  Include(e => e.department).
                  Include(e => e.avto_worker).ThenInclude(e => e.work_data).ThenInclude(e => e.worker).
                  Include(e => e.avto_worker).ThenInclude(e => e.work_data).ThenInclude(e => e.profession).
                  Include(e => e.avto_worker).ThenInclude(e => e.divisions).ThenInclude(e => e.way).
                  Include(e => e.brand).ThenInclude(e => e.manufacturer).
                  Include(e => e.brand).ThenInclude(e => e.type_of_avto).
                  Where(e => e.Avto_Key == model.Avto_Key).FirstOrDefaultAsync();

                a.when_sell = model.when_sell;
                a.sell_cost = model.sell_cost;
                _context.Update(a);
                await _context.SaveChangesAsync();

                return RedirectToAction("ShowAvto", "Shows", new { id = model.Avto_Key });
            }
            return View(AvtoViewModel.AvtoViewReturn(Id_sold, _context));
        }
    }
}