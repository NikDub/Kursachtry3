using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Kursachtry3.BDModel;
using Kursachtry3.Models.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kursachtry3.Controllers
{
    public class EditController : Controller
    {
        private BDModel.AvtoModel _context;
        public EditController(BDModel.AvtoModel context)
        {
            _context = context;
        }
        
        //avto
        [HttpGet]
        public IActionResult EditAvto()
        {
            return View();
        }

        [HttpPost]
        public IActionResult EditAvto(int Id_edit, AvtoViewModel model, string action, string typeofa, string depart, int brandl)
        {
            if (action == "Save Change")
            {
                var a = _context.avtoes.
                Include(e => e.repairs).ThenInclude(e => e.work_data).ThenInclude(e => e.worker).
                Include(e => e.repairs).ThenInclude(e => e.work_data).ThenInclude(e => e.profession).
                Include(e => e.repairs).ThenInclude(e => e.type_of_repair).
                Include(e => e.department).
                Include(e => e.avto_worker).ThenInclude(e => e.work_data).ThenInclude(e => e.worker).
                Include(e => e.avto_worker).ThenInclude(e => e.work_data).ThenInclude(e => e.profession).
                Include(e => e.avto_worker).ThenInclude(e => e.divisions).ThenInclude(e => e.way).
                Include(e => e.brand).ThenInclude(e => e.manufacturer).
                Include(e => e.brand).ThenInclude(e => e.type_of_avto).
                Where(e => e.Avto_Key == model.Avto_Key).FirstOrDefault();

                a.brand = _context.brands.Where(e=>e.brand_key==brandl).FirstOrDefault();
                a.Brand_Key = brandl;
                a.color = model.color;
                a.namber = model.namber;
                a.additional_info = model.additional_info;
                a.year_of_release = model.year_of_release;
                a.department = _context.departments.Where(e => e.Department_Key == Convert.ToInt32(depart)).FirstOrDefault();
                a.Department_Key = a.department.Department_Key;
                a.Avto_Key = model.Avto_Key;

                byte[] imageData = null;
                if (model.imageget != null)
                {
                    using (var binaryReader = new BinaryReader(model.imageget.OpenReadStream()))
                    {
                        imageData = binaryReader.ReadBytes((int)model.imageget.Length);
                    }   
                    a.image_byte = imageData;
                }
               
                _context.avtoes.Update(a);
                _context.SaveChanges();
                return RedirectToAction("ShowAvto", "Shows", new { id = a.Avto_Key });
            }
            return View(AvtoViewModel.AvtoViewReturn(Id_edit, _context));
        }

        //repair
        [HttpGet]
        public IActionResult EditRepair(int id)
        {
            var item = _context.type_of_repair.Where(e => e.Type_of_repair_Key == id).FirstOrDefault();
            return View(item);
        }

        [HttpPost]
        public IActionResult EditRepair(type_of_repair model)
        {
            _context.type_of_repair.Update(model);
            _context.SaveChanges(); 
            return RedirectToAction("ShowsRepair","Shows");
        }

        //brand
        [HttpGet]
        public IActionResult EditBrand(int id)
        {
            var item = _context.brands.Where(e => e.brand_key == id).FirstOrDefault();
            BrandViewModel model = new BrandViewModel()
            {
                Avto_Key=id,
                body_type=item.body_type,
                brand_key=id,
                expenses=item.expenses,
                manufacturer=item.manufacturer,
                manufacturer_key=item.manufacturer_key,
                name=item.name,
                type_of_avto=item.type_of_avto,
                type_of_avto_key=item.type_of_avto_key,
                typemass = _context.type_of_avto.ToList(),
                manufacturermass = _context.manufacturers.ToList()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult EditBrand(BrandViewModel model, string manufact, string type)
        {
            brand brand = new brand()
            {
               brand_key=model.Avto_Key
            };
            brand.expenses = model.expenses;
            brand.name = model.name;
            brand.body_type = model.body_type;
            brand.type_of_avto = _context.type_of_avto.Where(e => e.type_of_avto_key == Convert.ToInt32( type)).FirstOrDefault();
            brand.type_of_avto_key = _context.type_of_avto.Where(e => e.type_of_avto_key == Convert.ToInt32(type)).FirstOrDefault().type_of_avto_key;
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
            brand.manufacturer = _context.manufacturers.Where(e => e.Manufacturer_Key == Convert.ToInt32(manufact)).FirstOrDefault();
            brand.manufacturer_key = _context.manufacturers.Where(e => e.Manufacturer_Key == Convert.ToInt32(manufact)).FirstOrDefault().Manufacturer_Key;

            _context.brands.Update(brand);
            _context.SaveChanges();
            return RedirectToAction("ShowsBrand","Shows");
        }
        
        //way
        [HttpGet]
        public IActionResult EditWay(int id)
        {
            var item = _context.ways.Where(e => e.Ways_Key == id).FirstOrDefault();
            AddWay addWay = new AddWay()
            {
                Avto_Key = id,
                cost = item.cost,
                end_point = item.end_point,
                lenght = item.lenght,
                name = item.name,
                start_point = item.start_point,
                time_in_way = item.time_in_way
            };
            return View(addWay);
        }

        [HttpPost]
        public IActionResult EditWay(AddWay model)
        {
            way way = new way()
            {
                Ways_Key=model.Avto_Key,
                cost = model.cost,
                end_point = model.end_point,
                start_point = model.start_point,
                lenght = model.lenght,
                name = model.name,
                time_in_way = model.time_in_way
            };
            _context.ways.Update(way);
            _context.SaveChanges();
            return RedirectToAction("ShowsWay", "Shows");
        }

        //worker
        [HttpGet]
        public IActionResult EditWorker(int id)
        {
            var item = _context.workers.Include(e=>e.work_data).ThenInclude(e=>e.profession).Where(e => e.Worker_Key == id).FirstOrDefault();
            AddWorker addWorker = new AddWorker()
            {
                work_data_key= item.work_data.First().Work_data_Key,
                Avto_Key = id,
                departmentsmass = _context.departments,
                professionsmass = _context.professions,
                age=item.age,
                date_end=item.work_data.First().date_end,
                date_start=item.work_data.First().date_start,
                Department_Key=item.work_data.First().Department_Key,
                fullname=item.fullname,
                namber=item.namber,
                Profession_Key=item.work_data.First().Profession_Key,
                Worker_Key=item.Worker_Key
            };
            return View(addWorker);
        }

        [HttpPost]
        public IActionResult EditWorker(AddWorker model, string depart, string professionss)
        {
            worker worker = new worker()
            {
                Worker_Key=model.Avto_Key,
                age = model.age,
                fullname = model.fullname,
                namber = model.namber,
            };

            _context.workers.Update(worker);
            _context.SaveChanges();

            work_data work_Data = new work_data()
            {
                Work_data_Key=model.work_data_key,
                department = _context.departments.Where(e => e.Department_Key == Convert.ToInt32(depart)).FirstOrDefault(),
                worker = _context.workers.Where(e => e.fullname == worker.fullname).FirstOrDefault(),
                profession = _context.professions.Where(e => e.Profession_Key ==Convert.ToInt32(professionss)).FirstOrDefault(),
                date_start = model.date_start,
                date_end = model.date_end
            };
            work_Data.Department_Key = work_Data.department.Department_Key;
            work_Data.Worker_Key = work_Data.worker.Worker_Key;
            work_Data.Profession_Key = work_Data.profession.Profession_Key;

            _context.work_data.Update(work_Data);
            _context.SaveChanges();
            return RedirectToAction("ShowsWorker", "Shows");
        }
    }
}