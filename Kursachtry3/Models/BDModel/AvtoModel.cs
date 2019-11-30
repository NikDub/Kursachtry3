namespace Kursachtry3.BDModel
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;

    public partial class AvtoModel : DbContext
    {
        public AvtoModel(DbContextOptions<AvtoModel> options)
         : base(options)
        {
        }

        public virtual DbSet<avto> avtoes { get; set; }
        public virtual DbSet<avto_worker> avto_worker { get; set; }
        public virtual DbSet<brand> brands { get; set; }
        public virtual DbSet<department> departments { get; set; }
        public virtual DbSet<division> divisions { get; set; }
        public virtual DbSet<manufacturer> manufacturers { get; set; }
        public virtual DbSet<profession> professions { get; set; }
        public virtual DbSet<repair> repairs { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<type_of_avto> type_of_avto { get; set; }
        public virtual DbSet<type_of_repair> type_of_repair { get; set; }
        public virtual DbSet<way> ways { get; set; }
        public virtual DbSet<work_data> work_data { get; set; }
        public virtual DbSet<worker> workers { get; set; }

    }
}
