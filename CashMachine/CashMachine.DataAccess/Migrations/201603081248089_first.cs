namespace CashMachine.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Accounts", "CardNumber", c => c.String(maxLength: 16));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Accounts", "CardNumber", c => c.Int(nullable: false));
        }
    }
}
