using System.Collections.Generic;
using CashMachine.DataAccess.Entities;
using CashMachine.Web.Helpers;

namespace CashMachine.DataAccess.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CashMachine.DataAccess.CashMachineDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CashMachine.DataAccess.CashMachineDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            context.Accounts.AddOrUpdate(a => a.CardNumber,
            new Account
            {
                CardNumber = "1234567890123456",
                PinCode = HashManager.HashPassword("1234"),
                AvailableBalance = 1000,
                IsBlocked = false,
                AttemptsCount = 0
            },
            new Account
            {
                CardNumber = "3333333333333333",
                PinCode = HashManager.HashPassword("3333"),
                AvailableBalance = 1000,
                IsBlocked = false,
                AttemptsCount = 0
            },
            new Account
            {
                CardNumber = "1111111111111111",
                PinCode = HashManager.HashPassword("1111"),
                AvailableBalance = 1000,
                IsBlocked = false,
                AttemptsCount = 0
            });
        }
    }
}
