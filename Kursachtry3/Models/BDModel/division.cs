namespace Kursachtry3.BDModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("division")]
    public partial class division
    {
        [Key]
        public int division_key { get; set; }

        public int? Avto_Worker_Key { get; set; }

        public int? Ways_Key { get; set; }

        [Column(TypeName = "date")]
        public DateTime? date_start { get; set; }

        [Column(TypeName = "date")]
        public DateTime? date_end { get; set; }

        public virtual avto_worker avto_worker { get; set; }

        public virtual way way { get; set; }
    }
}
