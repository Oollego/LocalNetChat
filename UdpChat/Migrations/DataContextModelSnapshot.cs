﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UdpChat.Data.Entities;

#nullable disable

namespace UdpChat.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.15");

            modelBuilder.Entity("UdpChat.Data.Entities.Contact", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("Avatar")
                        .HasColumnType("BLOB");

                    b.Property<string>("IpAddress")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsAvatarAdded")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("NotReadedMessage")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Surname")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("UdpChat.Data.Entities.UserMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ContactId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<long>("FileLength")
                        .HasColumnType("INTEGER");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("IpAddress")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsFileAdded")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsIncoming")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ContactId");

                    b.ToTable("UserMessages");
                });

            modelBuilder.Entity("UdpChat.Data.Entities.UserSettings", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("Avatar")
                        .HasColumnType("BLOB");

                    b.Property<string>("AvatarFileName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FolderPath")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsAvatarAdded")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Port")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Settings");

                    b.HasData(
                        new
                        {
                            Id = new Guid("4dba2944-b8cf-4460-a618-5ea175d3f924"),
                            AvatarFileName = "",
                            FolderPath = "C:\\Users\\Dasha\\Pictures\\StepTemp\\UdpChat\\UdpChat\\Downloads",
                            IsAvatarAdded = false,
                            Name = "UserName",
                            Port = 10000,
                            Surname = ""
                        });
                });

            modelBuilder.Entity("UdpChat.Data.Entities.UserMessage", b =>
                {
                    b.HasOne("UdpChat.Data.Entities.Contact", "Contact")
                        .WithMany("Messages")
                        .HasForeignKey("ContactId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Contact");
                });

            modelBuilder.Entity("UdpChat.Data.Entities.Contact", b =>
                {
                    b.Navigation("Messages");
                });
#pragma warning restore 612, 618
        }
    }
}
