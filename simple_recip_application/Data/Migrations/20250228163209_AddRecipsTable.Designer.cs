﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using simple_recip_application.Data;

#nullable disable

namespace simple_recip_application.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250228163209_AddRecipsTable")]
    partial class AddRecipsTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.2");

            modelBuilder.Entity("simple_recip_application.Features.IngredientsManagement.Persistence.Entities.IngredientModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("Image")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.Property<DateTime>("ModificationDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("RemoveDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Ingredients");
                });

            modelBuilder.Entity("simple_recip_application.Features.RecipesManagement.Persistence.Entites.RecipeIngredient", b =>
                {
                    b.Property<Guid>("RecipeId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("IngredientId")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("TEXT");

                    b.HasKey("RecipeId", "IngredientId");

                    b.HasIndex("IngredientId");

                    b.ToTable("RecipeIngredients");
                });

            modelBuilder.Entity("simple_recip_application.Features.RecipesManagement.Persistence.Entites.RecipeModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<TimeOnly>("CookingTime")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("Image")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.Property<string>("Instructions")
                        .IsRequired()
                        .HasMaxLength(3000)
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("ModificationDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<TimeOnly>("PreparationTime")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("RemoveDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Recipes");
                });

            modelBuilder.Entity("simple_recip_application.Features.RecipesManagement.Persistence.Entites.RecipeIngredient", b =>
                {
                    b.HasOne("simple_recip_application.Features.IngredientsManagement.Persistence.Entities.IngredientModel", "Ingredient")
                        .WithMany()
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("simple_recip_application.Features.RecipesManagement.Persistence.Entites.RecipeModel", "Recipe")
                        .WithMany("Ingredients")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ingredient");

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("simple_recip_application.Features.RecipesManagement.Persistence.Entites.RecipeModel", b =>
                {
                    b.Navigation("Ingredients");
                });
#pragma warning restore 612, 618
        }
    }
}
