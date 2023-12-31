﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SubsidiosClientes.Data;

#nullable disable

namespace SubsidiosClientes.Migrations
{
    [DbContext(typeof(SubsidiosContext))]
    [Migration("20231107071420_FechaCuotaDateTime")]
    partial class FechaCuotaDateTime
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.13");

            modelBuilder.Entity("SubsidiosClientes.Data.Entities.Cuota", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal?>("AmortizacionCapital")
                        .HasColumnType("TEXT");

                    b.Property<int?>("CantidadDias")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("FechaVencimiento")
                        .HasColumnType("TEXT");

                    b.Property<int>("IdPrestamo")
                        .HasColumnType("INTEGER");

                    b.Property<decimal?>("Interes")
                        .HasColumnType("TEXT");

                    b.Property<decimal?>("InteresControlSubsidio")
                        .HasColumnType("TEXT");

                    b.Property<decimal?>("InteresControlTNA")
                        .HasColumnType("TEXT");

                    b.Property<int?>("NroCuota")
                        .HasColumnType("INTEGER");

                    b.Property<decimal?>("SaldoFinalCapital")
                        .HasColumnType("TEXT");

                    b.Property<decimal?>("SaldoInicialCapital")
                        .HasColumnType("TEXT");

                    b.Property<decimal?>("ValorCuota")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("IdPrestamo");

                    b.ToTable("Cuotas");
                });

            modelBuilder.Entity("SubsidiosClientes.Data.Entities.Prestamo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CantidadCuotas")
                        .HasColumnType("INTEGER");

                    b.Property<string>("FechaComunicacionBNA")
                        .HasColumnType("TEXT");

                    b.Property<decimal?>("MontoCredito")
                        .HasColumnType("TEXT");

                    b.Property<decimal?>("MontoCuota")
                        .HasColumnType("TEXT");

                    b.Property<string>("NombreCliente")
                        .HasColumnType("TEXT");

                    b.Property<string>("NroPrestamo")
                        .HasColumnType("TEXT");

                    b.Property<decimal?>("TEM")
                        .HasColumnType("TEXT");

                    b.Property<decimal?>("TNA")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Prestamos");
                });

            modelBuilder.Entity("SubsidiosClientes.Data.Entities.Cuota", b =>
                {
                    b.HasOne("SubsidiosClientes.Data.Entities.Prestamo", "Prestamo")
                        .WithMany("Cuotas")
                        .HasForeignKey("IdPrestamo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Prestamo");
                });

            modelBuilder.Entity("SubsidiosClientes.Data.Entities.Prestamo", b =>
                {
                    b.Navigation("Cuotas");
                });
#pragma warning restore 612, 618
        }
    }
}
