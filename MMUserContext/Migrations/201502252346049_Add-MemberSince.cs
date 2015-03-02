namespace MMUserContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMemberSince : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MixRequests", "MixClient_Id", "dbo.MixClients");
            DropIndex("dbo.MixRequests", new[] { "MixClient_Id" });
            AddColumn("dbo.MixClients", "MemberSince", c => c.DateTime(nullable: false));
            DropColumn("dbo.MixRequests", "MixClient_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MixRequests", "MixClient_Id", c => c.Int());
            DropColumn("dbo.MixClients", "MemberSince");
            CreateIndex("dbo.MixRequests", "MixClient_Id");
            AddForeignKey("dbo.MixRequests", "MixClient_Id", "dbo.MixClients", "Id");
        }
    }
}
