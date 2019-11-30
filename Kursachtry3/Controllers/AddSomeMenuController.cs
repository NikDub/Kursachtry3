using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kursachtry3.BDModel;
using Kursachtry3.Models.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kursachtry3.Controllers
{
    public class AddSomeMenuController : Controller
    {
        private BDModel.AvtoModel _context;
        public AddSomeMenuController(BDModel.AvtoModel context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult AddBrand()
        {
            BrandViewModel model = new BrandViewModel()
            {
                typemass = _context.type_of_avto.ToList(),
                manufacturermass = _context.manufacturers.ToList()
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult AddBrand(BrandViewModel model, string type, string manufact)
        {
            brand brand = new brand()
            {
                expenses = model.expenses,
                name = model.name,
                body_type = model.body_type,
                type_of_avto = _context.type_of_avto.Where(e => e.name == type).FirstOrDefault(),
                type_of_avto_key = _context.type_of_avto.Where(e => e.name == type).FirstOrDefault().type_of_avto_key,

            };

            manufacturer manufacturer = _context.manufacturers.Where(e => e.name == manufact).FirstOrDefault();
            if (manufacturer == null)
            {
                manufacturer manufactur = new manufacturer()
                {
                    name = manufact
                };
                _context.manufacturers.Add(manufactur);
                _context.SaveChanges();
            }
            brand.manufacturer = _context.manufacturers.Where(e => e.name == manufact).FirstOrDefault();
            brand.manufacturer_key = _context.manufacturers.Where(e => e.name == manufact).FirstOrDefault().Manufacturer_Key;

            _context.brands.Add(brand);
            _context.SaveChanges();
            return RedirectToAction("AddBrand", "AddSomeMenu");
        }

        [HttpGet]
        public IActionResult AddWorker()
        {
            AddWorker addWorker = new AddWorker()
            {
                departmentsmass = _context.departments,
                professionsmass = _context.professions
            };
            return View(addWorker);
        }
        [HttpPost]
        public IActionResult AddWorker(AddWorker model, string depart, string profession) 
        {
            worker worker = new worker()
            {
                age = model.age,
                fullname = model.fullname,
                namber = model.namber,
            };

            _context.workers.Add(worker);
            _context.SaveChanges();

            work_data work_Data = new work_data()
            {
                department = _context.departments.Where(e => e.Department_Key == Convert.ToInt32(depart)).FirstOrDefault(),
                worker = _context.workers.Where(e => e.fullname == worker.fullname).FirstOrDefault(),
                profession = _context.professions.Where(e => e.Profession_Key == Convert.ToInt32(profession)).FirstOrDefault(),
                date_start = model.date_start,
                date_end = model.date_end
            };
            work_Data.Department_Key = work_Data.department.Department_Key;
            work_Data.Worker_Key = work_Data.worker.Worker_Key;
            work_Data.Profession_Key = work_Data.profession.Profession_Key;

            _context.work_data.Add(work_Data);
            _context.SaveChanges();
            return RedirectToAction("AddWorker", "AddSomeMenu");
        }
        [HttpGet]
        public IActionResult AddWay()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddWay(AddWay model)
        {
            way way = new way()
            {
                cost = model.cost,
                end_point = model.end_point,
                start_point = model.start_point,
                lenght = model.lenght,
                name = model.name,
                time_in_way = model.time_in_way
            };
            _context.ways.Add(way);
            _context.SaveChanges();
            return RedirectToAction("AddWay", "AddSomeMenu");
        }

        [HttpGet]
        public IActionResult AddRepair()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddRepair(AddRepair model)
        {
            type_of_repair type_of_repair = new type_of_repair()
            {
                cost = model.cost,
                name = model.name
            };
            _context.type_of_repair.Add(type_of_repair);
            _context.SaveChanges();
            return RedirectToAction("AddRepair", "AddSomeMenu");
        }
    }
}