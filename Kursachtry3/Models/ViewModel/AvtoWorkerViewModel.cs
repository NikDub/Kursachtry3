using Kursachtry3.BDModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kursachtry3.Models.ViewModel
{
    public class AvtoWorkerViewModel
    {
        public int? Avto_Key { get; set; }
        public DateTime? date_start { get; set; }
        public DateTime? date_end { get; set; }
        public IEnumerable<worker> work_datas { get; set; }
        public IEnumerable<work_data> workersin { get; set; }
    }
}
