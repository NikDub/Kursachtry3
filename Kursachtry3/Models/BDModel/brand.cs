namespace Kursachtry3.BDModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("brand")]
    public partial class brand
    {
        [Key]
        public int brand_key { get; set; }

        public int? manufacturer_key { get; set; }

        public int? type_of_avto_key { get; set; }

        [StringLength(45)]
        public string body_type { get; set; }

        [StringLength(45)]
        public string name { get; set; }

        public int? expenses { get; set; }

        public virtual manufacturer manufacturer { get; set; }

        public virtual type_of_avto type_of_avto { get; set; }
    }
}
