using Kursachtry3.BDModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kursachtry3.Models.ViewModel
{
    public class ShowWorkerEdit
    {
        public IEnumerable<worker> workers { get; set; }
        public IEnumerable<worker> workersmass { get; set; }

    }
}
