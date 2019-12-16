using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kursachtry3.BDModel;
using Kursachtry3.Models.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZendeskApi_v2.Requests;

namespace Kursachtry3.Controllers
{
    public class ShowsController : Controller
    {
        private BDModel.AvtoModel _context;

        public ShowsController(BDModel.AvtoModel context)
        {
            _context = context;
        }
        //Avto
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

        [HttpPost]
        public IActionResult ShowsSoldAvto(int Id)
        {
            return RedirectToAction("ShowAvto", "Shows", new { id = Id });
        }

        //Brand
        [HttpGet]
        public IActionResult ShowsBrand()
        {
            ShowBrandEdit showBrandEdit = new ShowBrandEdit()
            {
                brands = _context.brands.Include(e => e.manufacturer).Include(e => e.type_of_avto),
                manufacturers = _context.manufacturers,
                type_Of_Avtos= _context.type_of_avto
            };
            return View(showBrandEdit);
        }

        [HttpPost]
        public IActionResult ShowsBrand(int id, string action, string manfilt, string typeFilt)
        {
            if (manfilt!=null)
            {
                ShowBrandEdit showBrandEdit = new ShowBrandEdit()
                {
                    brands = _context.brands.Include(e => e.manufacturer).Include(e => e.type_of_avto).Where(e=>e.manufacturer.name==manfilt),
                    manufacturers = _context.manufacturers,
                    type_Of_Avtos = _context.type_of_avto
                };
                return View(showBrandEdit);
            }
            if (typeFilt!=null)
            {
                ShowBrandEdit showBrandEdit = new ShowBrandEdit()
                {
                    brands = _context.brands.Include(e => e.manufacturer).Include(e => e.type_of_avto).Where(e => e.type_of_avto.name == typeFilt),
                    manufacturers = _context.manufacturers,
                    type_Of_Avtos = _context.type_of_avto
                };
                return View(showBrandEdit);
            }
            if (action == "Delete")
            {
                var delItem = _context.brands.Where(e => e.brand_key == id).FirstOrDefault();
                _context.brands.Remove(delItem);
                _context.SaveChanges();
                IEnumerable<brand> brands = _context.brands.Include(e => e.manufacturer).Include(e => e.type_of_avto);
                return View(brands);
            }
            else if (action == "Edit")
            {
                return RedirectToAction("EditBrand", "Edit", new { id = id });
            }
            return View();
        }

        //Repair
        [HttpGet]
        public IActionResult ShowsRepair()
        {
            IEnumerable<type_of_repair> repairs = _context.type_of_repair;
            return View(repairs);
        }

        [HttpPost]
        public IActionResult ShowsRepair(int id, string action)
        {
            if (action == "Delete")
            {
                var delItem = _context.type_of_repair.Where(e => e.Type_of_repair_Key == id).FirstOrDefault();
                _context.type_of_repair.Remove(delItem);
                _context.SaveChanges();
                IEnumerable<type_of_repair> repairs = _context.type_of_repair;
                return View(repairs);
            }
            else if (action=="Edit")
            {
                return RedirectToAction("EditRepair","Edit", new { id=id} );
            }
            return View();
        }

        //Way
        [HttpGet]
        public IActionResult ShowsWay()
        {
            IEnumerable<way> ways = _context.ways;
            return View(ways);
        }   

        [HttpPost]
        public IActionResult ShowsWay(int id, string action)
        {
            if (action == "Delete")
            {
                var delItem = _context.ways.Where(e => e.Ways_Key == id).FirstOrDefault();
                _context.ways.Remove(delItem);
                _context.SaveChanges();
                IEnumerable<way> ways = _context.ways;
                return View(ways);
            }
            else if (action == "Edit")
            {
                return RedirectToAction("EditWay", "Edit", new { id = id });
            }
            return View();
        }

        //Worker
        [HttpGet]
        public IActionResult ShowsWorker()
        {
            ShowWorkerEdit showWorkerEdit = new ShowWorkerEdit()
            {
                workers =  _context.workers.Include(e => e.work_data).ThenInclude(e=>e.profession),
                workersmass = _context.workers.Include(e => e.work_data).ThenInclude(e => e.profession)
            };
            return View(showWorkerEdit);
        }

        [HttpPost]
        public IActionResult ShowsWorker(int id, string action,string manfilt)
        {
            if (manfilt != null)
            {
                ShowWorkerEdit showWorkerEdit = new ShowWorkerEdit()
                {
                    workers = _context.workers.Include(e => e.work_data).ThenInclude(e => e.profession).Where(e => e.fullname.Contains(manfilt)),
                    workersmass = _context.workers.Include(e => e.work_data).ThenInclude(e => e.profession)
                };
                return View(showWorkerEdit);
            }

            if (action == "Delete")
            {
                var delItem = _context.workers.Where(e => e.Worker_Key == id).FirstOrDefault();
                var delItm2 = _context.work_data.Where(e => e.worker.Worker_Key == id).FirstOrDefault();
                _context.work_data.Remove(delItm2);
                _context.workers.Remove(delItem);
                _context.SaveChanges();

                ShowWorkerEdit showWorkerEdit = new ShowWorkerEdit()
                {
                    workers = _context.workers.Include(e => e.work_data).ThenInclude(e => e.profession),
                    workersmass = _context.workers.Include(e => e.work_data).ThenInclude(e => e.profession)
                };
                return View(showWorkerEdit);
            }
            else if (action == "Edit")
            {
                return RedirectToAction("EditWorker", "Edit", new { id = id });
            }
            return View();
        }
    }
}