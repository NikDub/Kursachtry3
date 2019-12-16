using Kursachtry3.BDModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kursachtry3.Models.ViewModel
{
    public class ShowBrandEdit
    {
        public  IEnumerable<brand> brands { get; set; }
        public IEnumerable<manufacturer> manufacturers { get; set; }
        public IEnumerable<type_of_avto> type_Of_Avtos { get; set; }
    }
}
