namespace GBIplantService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newFieldInBuyerAndModelMessageInfo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MessageInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MessageId = c.String(),
                        FromMailAddress = c.String(),
                        Subject = c.String(),
                        Body = c.String(),
                        DateDelivery = c.DateTime(nullable: false),
                        BuyerId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Buyers", t => t.BuyerId)
                .Index(t => t.BuyerId);
            
            AddColumn("dbo.Buyers", "Mail", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MessageInfoes", "BuyerId", "dbo.Buyers");
            DropIndex("dbo.MessageInfoes", new[] { "BuyerId" });
            DropColumn("dbo.Buyers", "Mail");
            DropTable("dbo.MessageInfoes");
        }
    }
}
