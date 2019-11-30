namespace Kursachtry3.BDModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class work_data
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public work_data()
        {
            avto_worker = new HashSet<avto_worker>();
            repairs = new HashSet<repair>();
        }

        [Key]
        public int Work_data_Key { get; set; }

        public int? Department_Key { get; set; }

        public int? Worker_Key { get; set; }

        public int? Profession_Key { get; set; }

        [Column(TypeName = "date")]
        public DateTime? date_start { get; set; }

        [Column(TypeName = "date")]
        public DateTime? date_end { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<avto_worker> avto_worker { get; set; }

        public virtual department department { get; set; }

        public virtual profession profession { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<repair> repairs { get; set; }

        public virtual worker worker { get; set; }
    }
}
