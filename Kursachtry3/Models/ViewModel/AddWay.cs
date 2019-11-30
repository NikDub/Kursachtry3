using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kursachtry3.Models.ViewModel
{
    public class AddWay
    {
        public int Avto_Key { get; set; }
        public string name { get; set; }
        public double? lenght { get; set; }
        public TimeSpan? time_in_way { get; set; }
        public string start_point { get; set; }
        public double? cost { get; set; }
        public string end_point { get; set; }
    }
}
