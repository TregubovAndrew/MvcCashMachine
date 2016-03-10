using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashMachine.DataAccess.Entities;

namespace CashMachine.DataAccess
{
    public class CashMachineDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }

        public DbSet<Operation> Operations { get; set; } 

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().Property(a => a.CardNumber).HasMaxLength(16);

            modelBuilder.Entity<Account>()
                .HasMany(a =>a.Operations)
                .WithOptional(oh =>oh.Account)
                .HasForeignKey(oh =>oh.AccountId)
                .WillCascadeOnDelete(false);
        }
    }
}
