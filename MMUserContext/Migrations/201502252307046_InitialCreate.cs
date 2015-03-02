namespace MMUserContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MixClients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        Name = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MixRequests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VersType = c.Int(nullable: false),
                        Title = c.String(),
                        CanModifySounds = c.Boolean(nullable: false),
                        EntryTime = c.DateTime(nullable: false),
                        AcceptanceTime = c.DateTime(nullable: false),
                        MixClient_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MixClients", t => t.MixClient_Id)
                .Index(t => t.MixClient_Id);
            
            CreateTable(
                "dbo.Mixes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MixRequests", "MixClient_Id", "dbo.MixClients");
            DropIndex("dbo.MixRequests", new[] { "MixClient_Id" });
            DropTable("dbo.Mixes");
            DropTable("dbo.MixRequests");
            DropTable("dbo.MixClients");
        }
    }
}
