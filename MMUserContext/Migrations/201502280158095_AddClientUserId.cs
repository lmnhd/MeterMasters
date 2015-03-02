namespace MMUserContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddClientUserId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MixRequests", "ClientUserId", c => c.String());
            AddColumn("dbo.MixRequests", "ClientId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MixRequests", "ClientId");
            DropColumn("dbo.MixRequests", "ClientUserId");
        }
    }
}
