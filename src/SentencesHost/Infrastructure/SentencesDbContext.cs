﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using Sentences;

namespace SentencesHost.Infrastructure
{
    public class SentencesDbContext : DbContext
    {
        public SentencesDbContext() : base("sentence.sqlite")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;

            Database.SetInitializer<SentencesDbContext>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sentence>()
                        .ToTable("sentence")
                        .HasKey(x => x.Id)
                        .Property(x => x.Id)
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            base.OnModelCreating(modelBuilder);
        }
    }
}
