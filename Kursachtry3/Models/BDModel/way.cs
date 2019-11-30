namespace Kursachtry3.BDModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class way
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public way()
        {
            divisions = new HashSet<division>();
        }

        [Key]
        public int Ways_Key { get; set; }
        [StringLength(20)]
        public string name { get; set; }
        public double? lenght { get; set; }
        public TimeSpan? time_in_way { get; set; }
        [StringLength(45)]
        public string start_point { get; set; }
        public double? cost { get; set;  }
        [StringLength(45)]
        public string end_point { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<division> divisions { get; set; }
    }
}
