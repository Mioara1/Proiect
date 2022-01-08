using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace SalaFitnessModel
{
    public partial class SalaFitnessEntitateModel : DbContext
    {
        public SalaFitnessEntitateModel()
            : base("name=SalaFitnessEntitateModel")
        {
        }

        public virtual DbSet<Abonament> Abonament { get; set; }
        public virtual DbSet<Achizitie> Achizitie { get; set; }
        public virtual DbSet<Client> Client { get; set; }
       // public object Client { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Abonament>()
                .HasMany(e => e.Achizities)
                .WithOptional(e => e.Abonament)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Client>()
                .HasMany(e => e.Achizities)
                .WithOptional(e => e.Client)
                .WillCascadeOnDelete();
        }
    }
}
