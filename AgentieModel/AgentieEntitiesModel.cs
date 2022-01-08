using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace AgentieModel
{
    public partial class AgentieEntitiesModel : DbContext
    {
        public AgentieEntitiesModel()
            : base("name=AgentieEntitiesModel")
        {
        }

        public virtual DbSet<Angajati> Angajati { get; set; }
        public virtual DbSet<Apartamente> Apartamente { get; set; }
        public virtual DbSet<Clienti> Clienti { get; set; }
        public virtual DbSet<Inchirieri> Inchirieri { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Angajati>()
                .HasMany(e => e.Inchirieris)
                .WithOptional(e => e.Angajati)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Apartamente>()
                .HasMany(e => e.Inchirieris)
                .WithOptional(e => e.Apartamente)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Clienti>()
                .HasMany(e => e.Inchirieris)
                .WithOptional(e => e.Clienti)
                .WillCascadeOnDelete();
        }
    }
}
