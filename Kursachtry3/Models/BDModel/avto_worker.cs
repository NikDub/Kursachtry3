namespace Kursachtry3.BDModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class avto_worker
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public avto_worker()
        {
            divisions = new HashSet<division>();
        }

        [Key]
        public int Avto_Worker_Key { get; set; }

        public int? Work_data_Key { get; set; }

        public int? Avto_Key { get; set; }

        [Column(TypeName = "date")]
        public DateTime? date_start { get; set; }

        [Column(TypeName = "date")]
        public DateTime? date_end { get; set; }

        public virtual avto avto { get; set; }

        public virtual work_data work_data { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<division> divisions { get; set; }
    }
}
