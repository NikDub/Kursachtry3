namespace Kursachtry3.BDModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("avto")]
    public partial class avto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public avto()
        {
            avto_worker = new HashSet<avto_worker>();
            repairs = new HashSet<repair>();
        }

        [Key]
        public int Avto_Key { get; set; }
        public int? Department_Key { get; set; }
        public int? Brand_Key { get; set; }
        [StringLength(45)]
        public string namber { get; set; }
        [Column(TypeName = "date")]
        public DateTime? year_of_release { get; set; }
        [StringLength(45)]
        public string color { get; set; }
        public int? additional_info { get; set; }
        [Column(TypeName = "date")]
        public DateTime? cancelletion_date { get; set; }
        [Column(TypeName = "date")]
        public DateTime? receipt_date { get; set; }
        [Column(TypeName = "date")]
        public DateTime? when_cancellation { get; set; }
        [Column(TypeName = "date")]
        public DateTime? when_sell { get; set; }
        public double? sell_cost { get; set; }
        [Column(TypeName = "image")]
        public byte[] image_byte { get; set; }
        public virtual department department { get; set; }
        public virtual brand brand { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<avto_worker> avto_worker { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<repair> repairs { get; set; }
    }
}
