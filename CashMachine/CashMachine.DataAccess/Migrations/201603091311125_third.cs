namespace CashMachine.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class third : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OperationHistories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateTime = c.DateTime(nullable: false),
                        CodeOfOperation = c.String(),
                        AccountId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.AccountId)
                .Index(t => t.AccountId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OperationHistories", "AccountId", "dbo.Accounts");
            DropIndex("dbo.OperationHistories", new[] { "AccountId" });
            DropTable("dbo.OperationHistories");
        }
    }
}
