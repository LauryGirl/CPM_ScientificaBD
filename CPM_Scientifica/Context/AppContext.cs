using Data;
using Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CPM_Scientifica.Context
{
    public class AppContext : DbContext
    {
        public AppContext() : base("name=AppContext")
        {
           
        }
        public DbSet<Change> Changes { get; set; }

        public DbSet<Inquest> Inquests { get; set; }

        public DbSet<Register> Registers { get; set; }

        public DbSet<Inscription> Inscriptions { get; set; }

        public DbSet<AuthorityTempMarket> AuthorityTemporalMarkets { get; set; }

        public DbSet<Maker> Makers { get; set; }

        public DbSet<ForeignMaker> ForeignMakers { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Sale> Sales { get; set; }

        public DbSet<Wail> Wails { get; set; }

        public DbSet<YearlyRevision> YearlyRevisions { get; set; }
    }
}