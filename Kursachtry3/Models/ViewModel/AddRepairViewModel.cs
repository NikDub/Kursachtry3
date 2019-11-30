using Kursachtry3.BDModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kursachtry3.Models.ViewModel
{
    public class AddRepairViewModel
    {
        public int Avto_key { get; set; }
        public DateTime? date { get; set; }
        public IEnumerable<type_of_repair> type_Of_Repairs { get; set; }
        public IEnumerable<work_data> work_Datas { get; set; }

    }
}
