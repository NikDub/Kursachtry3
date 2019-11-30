using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
    }
}