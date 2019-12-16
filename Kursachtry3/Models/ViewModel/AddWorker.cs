using Kursachtry3.BDModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kursachtry3.Models.ViewModel
{
    public class AddWorker
    {
        public int Avto_Key { get; set; }
        public int? Department_Key { get; set; }
        public int? Worker_Key { get; set; }
        public DateTime? date_start { get; set; }
        public DateTime? date_end { get; set; }
        public int? Profession_Key { get; set; }
        public string fullname { get; set; }
        public int? age { get; set; }
        public string namber { get; set; }
        public int work_data_key { get; set; }

        public IEnumerable<profession> professionsmass { get; set; }
        public IEnumerable<department> departmentsmass { get; set; }
    }
}
