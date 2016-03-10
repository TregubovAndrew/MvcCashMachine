namespace CashMachine.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDateOfLastFailedAttempt : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accounts", "AttemptsCount", c => c.Int(nullable: false));
            AddColumn("dbo.Accounts", "DateOfLastFailedAttempt", c => c.DateTime());
            DropColumn("dbo.Accounts", "AttempsCount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Accounts", "AttempsCount", c => c.Int(nullable: false));
            DropColumn("dbo.Accounts", "DateOfLastFailedAttempt");
            DropColumn("dbo.Accounts", "AttemptsCount");
        }
    }
}
