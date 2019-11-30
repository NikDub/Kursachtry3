using Kursachtry3.BDModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kursachtry3.Models.ViewModel
{
    public class BrandViewModel
    {
        public int Avto_Key { get; set; }
        public int brand_key { get; set; }

        public int? manufacturer_key { get; set; }

        public int? type_of_avto_key { get; set; }

        public string body_type { get; set; }

        public string name { get; set; }

        public int? expenses { get; set; }

        public virtual manufacturer manufacturer { get; set; }

        public virtual type_of_avto type_of_avto { get; set; }
        public IEnumerable<manufacturer> manufacturermass { get; set; }
        public IEnumerable<type_of_avto> typemass { get; set; }
    }
}
