using AthletesRestAPI.Data.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthletesRestAPI.Data
{
    public class AthleteDBContext:IdentityDbContext
    {
        public DbSet<DisciplineEntity> Disciplines => Set<DisciplineEntity>();
        public DbSet<AthleteEntity> Athletes => Set<AthleteEntity>();
        //public DbSet<Customer> Customers => Set<Customer>();
       // public DbSet<Order> Orders => Set<Order>();
        public AthleteDBContext(DbContextOptions<AthleteDBContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //DISCIPLINES---------------------------------------
            //mappear entity a tabla con ese nombre
            modelBuilder.Entity<DisciplineEntity>().ToTable("Disciplines");
            //indicar cual es el primary key y hacer que se agregue el valor incrementando el indice automaticamente
            modelBuilder.Entity<DisciplineEntity>().Property(d=>d.Id).ValueGeneratedOnAdd();
            //indicar tipo de relacion entre entities
            modelBuilder.Entity<DisciplineEntity>().HasMany(d => d.Athletes).WithOne(a=>a.Discipline);

            //ATHLETES---------------------------------------

            //mappear entity a tabla con ese nombre
            modelBuilder.Entity<AthleteEntity>().ToTable("Athletes");
            //indicar cual es el primary key y hacer que se agregue el valor incrementando el indice automaticamente
            modelBuilder.Entity<AthleteEntity>().Property(a => a.Id).ValueGeneratedOnAdd();
            //indicar tipo de relacion entre entities
            modelBuilder.Entity<AthleteEntity>().HasOne(a => a.Discipline).WithMany(d => d.Athletes);

            //CASCADE ------------------------------------------------
            // modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            /*modelBuilder.Entity<AthleteEntity>()
                .HasOne(a => a.Discipline)
                .WithMany(d => d.Athletes)
                .HasForeignKey(a => a.DisciplineId)
                .WillCascadeOnDelete(true);*/
            /*modelBuilder
                .Entity<AthleteEntity>()
                .HasOne(a => a.Discipline)
                .WithMany(d => d.Athletes)
                .HasForeignKey(a => a.Discipline.Id)
                .OnDelete(DeleteBehavior.Cascade);*/


        }


    }
}
