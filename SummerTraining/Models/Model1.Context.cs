﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SummerTraining.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SummerTrainingDBEntities : DbContext
    {
        public SummerTrainingDBEntities()
            : base("name=SummerTrainingDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Certificate_Template> Certificate_Template { get; set; }
        public virtual DbSet<Course_Classification> Course_Classification { get; set; }
        public virtual DbSet<Course_Schedule> Course_Schedule { get; set; }
        public virtual DbSet<Regestered_In_The_Course> Regestered_In_The_Course { get; set; }
        public virtual DbSet<Support_Tickets> Support_Tickets { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<User_Role> User_Role { get; set; }
    }
}