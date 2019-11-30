namespace Kursachtry3.BDModel
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("worker")]
    public partial class worker
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public worker()
        {
            work_data = new HashSet<work_data>();
        }

        [Key]
        public int Worker_Key { get; set; }

        [StringLength(45)]
        public string fullname { get; set; }

        public int? age { get; set; }

        [StringLength(45)]
        public string namber { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<work_data> work_data { get; set; }
    }
}
