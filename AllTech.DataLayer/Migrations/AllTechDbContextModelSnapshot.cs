﻿// <auto-generated />
using System;
using AllTech.DataLayer.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AllTech.DataLayer.Migrations
{
    [DbContext(typeof(AllTechDbContext))]
    partial class AllTechDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AllTech.DomainClasses.News.News", b =>
                {
                    b.Property<int>("NewsID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate");

                    b.Property<int>("GroupID");

                    b.Property<string>("ImageName");

                    b.Property<string>("MainText")
                        .IsRequired();

                    b.Property<string>("NewsTitle")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("ShortDescription")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<bool>("ShowInSlider");

                    b.Property<string>("Source");

                    b.Property<string>("Tags");

                    b.Property<int>("Visit");

                    b.HasKey("NewsID");

                    b.HasIndex("GroupID");

                    b.ToTable("News");
                });

            modelBuilder.Entity("AllTech.DomainClasses.NewsGroup.NewsGroup", b =>
                {
                    b.Property<int>("GroupID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("GroupTitle")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("GroupID");

                    b.ToTable("NewsGroups");
                });

            modelBuilder.Entity("AllTech.DomainClasses.User.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ActiveCode")
                        .HasMaxLength(50);

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<bool>("IsActive");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<string>("UserAvatar")
                        .HasMaxLength(100);

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("UserID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("AllTech.DomainClasses.User.UserRole", b =>
                {
                    b.Property<int>("UR_Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("RoleId");

                    b.Property<int>("UserId");

                    b.HasKey("UR_Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("AllTech.DomainClasses.Wallet.Wallet", b =>
                {
                    b.Property<int>("WalletId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Amount");

                    b.Property<string>("Description")
                        .HasMaxLength(100);

                    b.Property<bool>("IsPay");

                    b.Property<DateTime>("PayDate");

                    b.Property<int>("TypeId");

                    b.Property<int>("UserID");

                    b.HasKey("WalletId");

                    b.HasIndex("TypeId");

                    b.HasIndex("UserID");

                    b.ToTable("Wallets");
                });

            modelBuilder.Entity("AllTech.DomainClasses.Wallet.WalletType", b =>
                {
                    b.Property<int>("TypeId");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(150);

                    b.HasKey("TypeId");

                    b.ToTable("WalletTypes");
                });

            modelBuilder.Entity("AllTech.DomainClasses.News.News", b =>
                {
                    b.HasOne("AllTech.DomainClasses.NewsGroup.NewsGroup", "NewsGroup")
                        .WithMany("News")
                        .HasForeignKey("GroupID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AllTech.DomainClasses.User.UserRole", b =>
                {
                    b.HasOne("AllTech.DomainClasses.User.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AllTech.DomainClasses.Wallet.Wallet", b =>
                {
                    b.HasOne("AllTech.DomainClasses.Wallet.WalletType", "WalletType")
                        .WithMany("Wallets")
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AllTech.DomainClasses.User.User", "User")
                        .WithMany("Wallet")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
