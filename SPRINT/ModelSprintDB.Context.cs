﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SPRINT
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ModelSprintDBContainer : DbContext
    {
        
        public ModelSprintDBContainer(string connString)
            : base("name=ModelSprintDBContainer")
        {
            this.Database.Connection.ConnectionString = connString;

        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Cottager> CottagerSet { get; set; }
        public virtual DbSet<MonthConsumption> MonthConsumptionSet { get; set; }
        public virtual DbSet<Billing> BillingSet { get; set; }
        public virtual DbSet<CostOfWater> CostOfWaterSet { get; set; }
    }
}