using Kursachtry3.BDModel;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kursachtry3.Models.ViewModel
{
    public class AvtoViewModel
    {
        public AvtoViewModel() { }


        public int Avto_Key { get; set; }
        public int? Department_Key { get; set; }
        public string namber { get; set; }
        public int? Brand_Key { get; set; }
        public DateTime? year_of_release { get; set; }
        public string color { get; set; }
        public int? additional_info { get; set; }
        public DateTime? cancelletion_date { get; set; }
        public DateTime? receipt_date { get; set; }
        public DateTime? when_cancellation { get; set; }
        public DateTime? when_sell { get; set; }
        public double? sell_cost { get; set; }
        public byte[] image_byte { get; set; }
        public IFormFile imageget { get; set; }
        public virtual department department { get; set; }
        public virtual brand brand { get; set; }
        public virtual ICollection<avto_worker> avto_worker { get; set; }
        public virtual ICollection<repair> repairs { get; set; }
        public ICollection<department> departmass { get; set; }
        public ICollection<type_of_avto> typemass { get; set; }
        public ICollection<brand> brandlist { get; set; }

        public static AvtoViewModel AvtoViewReturn(int id, BDModel.AvtoModel context)
        {
            var temp = context.avtoes.
                Include(e => e.repairs).ThenInclude(e => e.work_data).ThenInclude(e => e.worker).
                Include(e => e.repairs).ThenInclude(e => e.work_data).ThenInclude(e => e.profession).
                Include(e => e.repairs).ThenInclude(e => e.type_of_repair).
                Include(e => e.department).
                Include(e=>e.avto_worker).ThenInclude(e=>e.work_data).ThenInclude(e=>e.worker).
                Include(e=>e.avto_worker).ThenInclude(e=>e.work_data).ThenInclude(e=>e.profession).
                Include(e=>e.avto_worker).ThenInclude(e=>e.divisions).ThenInclude(e=>e.way).
                Include(e=>e.brand).ThenInclude(e=>e.manufacturer).
                Include(e=> e.brand).ThenInclude(e=> e.type_of_avto).
                Where(e => e.Avto_Key == id).FirstOrDefault();

            AvtoViewModel avtoViewModel=new AvtoViewModel()
            {
                Avto_Key=temp.Avto_Key,
                additional_info=temp.additional_info,
                avto_worker=temp.avto_worker,
                cancelletion_date=temp.cancelletion_date,
                color=temp.color,
                department=temp.department,
                Department_Key=temp.Department_Key,
                image_byte=temp.image_byte,
                brand=temp.brand,
                Brand_Key=temp.Brand_Key,
                namber=temp.namber,
                receipt_date=temp.receipt_date,
                repairs=temp.repairs,
                sell_cost=temp.sell_cost,
                when_cancellation=temp.when_cancellation,
                when_sell=temp.when_sell,
                year_of_release=temp.year_of_release,
                typemass=context.type_of_avto.ToList(),
                departmass=context.departments.ToList(),
                brandlist=context.brands.Include(e=>e.manufacturer).ToList(),
            };
            return avtoViewModel;
        }
    }
}
