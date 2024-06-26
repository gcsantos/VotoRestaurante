﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VotoRestaurante.Context;

#nullable disable

namespace VotoRestaurante.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240531032224_tabelaVotos")]
    partial class tabelaVotos
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.6");

            modelBuilder.Entity("VotoRestaurante.Models.Restaurante", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<bool>("Participa")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Restaurantes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Nome = "Brabus",
                            Participa = false
                        },
                        new
                        {
                            Id = 2,
                            Nome = "Velho Chico",
                            Participa = false
                        },
                        new
                        {
                            Id = 3,
                            Nome = "Bar 29",
                            Participa = false
                        },
                        new
                        {
                            Id = 4,
                            Nome = "Hakuma Batata",
                            Participa = false
                        },
                        new
                        {
                            Id = 5,
                            Nome = "Bar do Peixe",
                            Participa = false
                        });
                });

            modelBuilder.Entity("VotoRestaurante.Models.Voto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DataRegistro")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<int>("RestauranteId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("RestauranteId");

                    b.ToTable("Votos");
                });

            modelBuilder.Entity("VotoRestaurante.Models.Voto", b =>
                {
                    b.HasOne("VotoRestaurante.Models.Restaurante", "Restaurante")
                        .WithMany()
                        .HasForeignKey("RestauranteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Restaurante");
                });
#pragma warning restore 612, 618
        }
    }
}
