namespace CashMachine.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameAndAddNewProperties : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.OperationHistories", newName: "Operations");
            AddColumn("dbo.Accounts", "AvailableBalance", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Accounts", "AttempsCount", c => c.Int(nullable: false));
            DropColumn("dbo.Accounts", "Money");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Accounts", "Money", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Accounts", "AttempsCount");
            DropColumn("dbo.Accounts", "AvailableBalance");
            RenameTable(name: "dbo.Operations", newName: "OperationHistories");
        }
    }
}
