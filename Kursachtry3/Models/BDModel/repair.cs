namespace Kursachtry3.BDModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("repair")]
    public partial class repair
    {
        [Key]
        public int repair_Key { get; set; }

        public int? Avto_Key { get; set; }

        public int? Type_of_repair_Key { get; set; }

        public int? Work_data_Key { get; set; }

        [Column(TypeName = "date")]
        public DateTime? date { get; set; }

        public virtual avto avto { get; set; }

        public virtual type_of_repair type_of_repair { get; set; }

        public virtual work_data work_data { get; set; }
    }
}
