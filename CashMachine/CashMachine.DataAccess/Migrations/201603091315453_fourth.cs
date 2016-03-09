namespace CashMachine.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fourth : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OperationHistories", "CardNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.OperationHistories", "CardNumber");
        }
    }
}
