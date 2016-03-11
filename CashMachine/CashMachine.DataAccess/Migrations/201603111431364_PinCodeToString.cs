namespace CashMachine.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PinCodeToString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Accounts", "PinCode", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Accounts", "PinCode", c => c.Int(nullable: false));
        }
    }
}
