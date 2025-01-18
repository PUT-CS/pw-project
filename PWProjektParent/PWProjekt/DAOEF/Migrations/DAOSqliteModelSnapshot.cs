﻿// <auto-generated />
using Barski_Lewandowski_DAOEF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Barski_Lewandowski_DAOEF.Migrations
{
    [DbContext(typeof(DAOSqlite))]
    partial class DAOSqliteModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.11");

            modelBuilder.Entity("DAOEF.BO.Broom", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("FibersMaterial")
                        .HasColumnType("INTEGER");

                    b.Property<int>("HandleMaterial")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<double>("Price")
                        .HasColumnType("REAL");

                    b.Property<int>("ProducerId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ProducerId");

                    b.ToTable("Brooms");
                });

            modelBuilder.Entity("DAOEF.BO.Producer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Producers");
                });

            modelBuilder.Entity("DAOEF.BO.Broom", b =>
                {
                    b.HasOne("DAOEF.BO.Producer", "Producer")
                        .WithMany("Brooms")
                        .HasForeignKey("ProducerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Producer");
                });

            modelBuilder.Entity("DAOEF.BO.Producer", b =>
                {
                    b.Navigation("Brooms");
                });
#pragma warning restore 612, 618
        }
    }
}
