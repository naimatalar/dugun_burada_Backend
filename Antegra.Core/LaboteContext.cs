﻿using Labote.Core.Constants;
using Labote.Core.Entities;
using Labote.Core.Entities.Administrative;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;

namespace Labote.Core
{
    public class LaboteContext : IdentityDbContext<LaboteUser, UserRole, Guid>
    {

        public IConfiguration Configuration { get; }



        public LaboteContext()
        {

        }

        public LaboteContext(DbContextOptions<LaboteContext> options) : base(options)
        {


        }
        public DbSet<UserTopic> UserTopics { get; set; }

        public DbSet<MenuModule> MenuModules { get; set; }
        public DbSet<UserMenuModule> UserMenuModules { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyPropertyKey> CompanyPropertyKeys { get; set; }
        public DbSet<CompanyTypePropertyValue> CompanyPropertyValues { get; set; }
        public DbSet<CompanyUserLaboteUser> FirmUserLaboteUsers { get; set; }
        public DbSet<CompanyType> CompanyTypes { get; set; }
        public DbSet<PropertySelectList> PropertySelectLists { get; set; }
        public DbSet<PropertySelectListValue> PropertySelectListValue { get; set; }
        public DbSet<AboutUs> AboutUses { get; set; }
        public DbSet<Faq> Faqs { get; set; }
        public DbSet<ContactUs> ContactUses { get; set; }
        public DbSet<CompanyImage> CompanyImages { get; set; }
        public DbSet<CompanyGroup> CompanyGroups { get; set; }
        public DbSet<DiscountCompany> DiscountCompanies { get; set; }
        public DbSet<IndexCategory> IndexCategories { get; set; }
        public DbSet<SelectedCompany> SelectedCompanies { get; set; }
        public DbSet<SelectedCompanyTypeCompany> SelectedCompanyTypeCompanies { get; set; }







        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.

                string json = File.ReadAllText("appsettings.json");
                dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
                string connectionString = jsonObj.ConnectionStrings.LaboteConnection.ToString();
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<Company>()
          .HasOne(p => p.CompanyType)
          .WithMany(b => b.Companies).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<CompanyPropertyKey>()
          .HasOne(p => p.CompanyType)
          .WithMany(b => b.CompanyPropertyKeys).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<CompanyTypePropertyValue>()
         .HasOne(p => p.CompanyPropertyKey)
         .WithMany(b => b.CompanyPropertyValues).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<CompanyTypePropertyValue>()
           .HasOne(p => p.Company)
            .WithMany(b => b.CompanyPropertyValues).OnDelete(DeleteBehavior.Restrict);


            base.OnModelCreating(builder);
        }






        public override bool Equals(object obj)
        {
            return obj is LaboteContext context &&
                   base.Equals(obj) &&
                   EqualityComparer<IConfiguration>.Default.Equals(Configuration, context.Configuration);
        }
    }

}

