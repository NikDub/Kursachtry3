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
    public class AddSomeController : Controller
    {
        private BDModel.AvtoModel _context;
        public AddSomeController(BDModel.AvtoModel context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> AddAvto()
        {
            AvtoViewModel avtoViewModel = new AvtoViewModel()
            {
                brandlist = await _context.brands.Include(e => e.manufacturer).ToListAsync(),
                departmass = await _context.departments.ToListAsync()
            };

            return View(avtoViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddAvto(string action, AvtoViewModel model, int brandl, int depart)
        {
            if (action == "AddBrand")
            {
                return RedirectToAction("AddBrand", "AddSome");
            }
            else if (action == "Save Change")
            {
                avto a = new avto()
                {
                    additional_info = model.additional_info,
                    brand = _context.brands.Where(e => e.brand_key == brandl).FirstOrDefault(),
                    Brand_Key = brandl,
                    cancelletion_date = model.cancelletion_date,
                    color = model.color,
                    department = _context.departments.Where(e => e.Department_Key == depart).FirstOrDefault(),
                    Department_Key = depart,
                    namber = model.namber,
                    receipt_date = DateTime.Now,
                    year_of_release = model.year_of_release
                };

                byte[] imageData = null;
                if (model.imageget != null)
                {
                    using (var binaryReader = new BinaryReader(model.imageget.OpenReadStream()))
                    {
                        imageData = binaryReader.ReadBytes((int)model.imageget.Length);
                    }
                    a.image_byte = imageData;
                }

                _context.avtoes.Add(a);
                _context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            AvtoViewModel avtoViewModel = new AvtoViewModel()
            {
                brandlist = await _context.brands.Include(e => e.manufacturer).ToListAsync(),
                departmass = await _context.departments.ToListAsync()
            };

            return View(avtoViewModel);
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
            return RedirectToAction("AddBrand", "AddSome");
        }



        [HttpGet]
        public IActionResult AddDriverToAvto(int id)
        {
            AvtoWorkerViewModel avtomodel = new AvtoWorkerViewModel()
            {
                Avto_Key = id,
                work_datas = _context.workers.ToList(),
                workersin = _context.work_data.Include(e => e.worker).Include(e => e.profession).Where(e => e.profession.Profession_Key == 1).
                Where(e => e.department.Department_Key == _context.avtoes.Where(a => a.Avto_Key == id).FirstOrDefault().Department_Key)
            };

            return View(avtomodel);
        }

        [HttpPost]
        public IActionResult AddDriverToAvto(AvtoWorkerViewModel model, string workers)
        {
            avto_worker avto_Worker = new avto_worker()
            {
                Avto_Key = model.Avto_Key,
                avto = _context.avtoes.Where(e => e.Avto_Key == model.Avto_Key).FirstOrDefault(),
                date_end = model.date_end,
                date_start = model.date_start,
                work_data = _context.work_data.Where(e => e.worker.fullname == workers).FirstOrDefault(),
                Work_data_Key = _context.work_data.Where(e => e.worker.fullname == workers).FirstOrDefault().Work_data_Key
            };

            _context.avto_worker.Add(avto_Worker);
            _context.SaveChanges();
            return RedirectToAction("ShowAvto", "Shows", new { id = model.Avto_Key });
        }

        [HttpGet]
        public IActionResult AddDriver(int id)
        {
            AddWorker addWorker = new AddWorker()
            {
                Avto_Key = id,
                departmentsmass = _context.departments,
                professionsmass = _context.professions
            };
            return View(addWorker);
        }

        [HttpPost]
        public IActionResult AddDriver(AddWorker model, string depart)
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
                profession = _context.professions.Where(e => e.Profession_Key == 1).FirstOrDefault(),
                date_start = model.date_start,
                date_end = model.date_end
            };
            work_Data.Department_Key = work_Data.department.Department_Key;
            work_Data.Worker_Key = work_Data.worker.Worker_Key;
            work_Data.Profession_Key = work_Data.profession.Profession_Key;

            _context.work_data.Add(work_Data);
            _context.SaveChanges();
            return RedirectToAction("AddDriverToAvto", "AddSome", new { id = model.Avto_Key });
        }



        [HttpGet]
        public IActionResult AddWayToAvto(int id)
        {
            AddWayViewModel addWayViewModel = new AddWayViewModel()
            {
                Avto_Key = id,
                ways = _context.ways,
                work_Datas = _context.work_data.Include(e => e.worker).Where(e => e.profession.Profession_Key == 1).Where(e => e.Department_Key == _context.avtoes.Where(r => r.Avto_Key == id).FirstOrDefault().Department_Key)
            };

            return View(addWayViewModel);
        }

        [HttpPost]
        public IActionResult AddWayToAvto(AddWayViewModel model, int ways, string worker)
        {
            division division = new division()
            {
                date_end = model.date_end,
                date_start = model.date_start,
                way = _context.ways.Where(e => e.Ways_Key == ways).FirstOrDefault(),
                avto_worker = _context.avto_worker.Where(e => e.work_data.worker.fullname == worker).FirstOrDefault()
            };
            division.Avto_Worker_Key = division.avto_worker.Avto_Worker_Key;
            division.Ways_Key = division.way.Ways_Key;
            _context.divisions.Add(division);
            _context.SaveChanges();
            return RedirectToAction("ShowAvto", "Shows", new { id = model.Avto_Key });
        }

        [HttpGet]
        public IActionResult AddWay(int id)
        {
            return View(new AddWay() { Avto_Key=id });
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
            return RedirectToAction("AddWayToAvto", "AddSome", new { id = model.Avto_Key });
        }



        [HttpGet]
        public IActionResult AddRepairToAvto(int id)
        {
            AddRepairViewModel model = new AddRepairViewModel()
            {
                Avto_key = id,
                type_Of_Repairs = _context.type_of_repair,
                work_Datas = _context.work_data.Include(e => e.worker).Where(e => e.profession.Profession_Key != 1).Where(e => e.Department_Key == _context.avtoes.Where(r => r.Avto_Key == id).FirstOrDefault().Department_Key)
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult AddRepairToAvto(AddRepairViewModel model, string type, string worker)
        {
            repair repair = new repair()
            {
                date = model.date,
                avto = _context.avtoes.Where(e => e.Avto_Key == model.Avto_key).FirstOrDefault(),
                type_of_repair = _context.type_of_repair.Where(e => e.name == type).FirstOrDefault(),
                work_data = _context.work_data.Where(e => e.worker.fullname == worker).FirstOrDefault()
            };
            repair.Avto_Key = model.Avto_key;
            repair.Type_of_repair_Key = repair.type_of_repair.Type_of_repair_Key;
            repair.Work_data_Key = repair.work_data.Work_data_Key;

            _context.repairs.Add(repair);
            _context.SaveChanges();

            return RedirectToAction("ShowAvto", "Shows", new { id = model.Avto_key });
        }

        [HttpGet]
        public IActionResult AddRepair(int id)
        {
            return View(new AddRepair() {Avto_Key=id });
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
            return RedirectToAction("AddRepairToAvto", "AddSome", new { id = model.Avto_Key });
        }
    }
}    