namespace CashMachine.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovePropertyCardNumberFromOperation : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Operations", "CardNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Operations", "CardNumber", c => c.String());
        }
    }
}
