// <auto-generated />
using System;
using AthletesRestAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AthletesRestAPI.Migrations
{
    [DbContext(typeof(AthleteDBContext))]
    [Migration("20211205122259_enableCascadeDeleting")]
    partial class enableCascadeDeleting
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AthletesRestAPI.Data.Entity.AthleteEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DisciplineId")
                        .HasColumnType("int");

                    b.Property<int?>("Gender")
                        .HasColumnType("int");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nationality")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("NumberOfCompetitions")
                        .HasColumnType("int");

                    b.Property<decimal?>("PersonalBest")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("SeasonBest")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("DisciplineId");

                    b.ToTable("Athletes");
                });

            modelBuilder.Entity("AthletesRestAPI.Data.Entity.DisciplineEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal?>("FemaleWorldRecord")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("MaleWorldRecord")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Rules")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Disciplines");
                });

            modelBuilder.Entity("AthletesRestAPI.Data.Entity.AthleteEntity", b =>
                {
                    b.HasOne("AthletesRestAPI.Data.Entity.DisciplineEntity", "Discipline")
                        .WithMany("Athletes")
                        .HasForeignKey("DisciplineId")
                        .OnDelete(DeleteBehavior.ClientCascade);

                    b.Navigation("Discipline");
                });

            modelBuilder.Entity("AthletesRestAPI.Data.Entity.DisciplineEntity", b =>
                {
                    b.Navigation("Athletes");
                });
#pragma warning restore 612, 618
        }
    }
}
