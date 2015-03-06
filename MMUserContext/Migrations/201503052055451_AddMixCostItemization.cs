namespace MMUserContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMixCostItemization : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Mixes", "ClientUserId", c => c.String());
            AddColumn("dbo.Mixes", "ClientId", c => c.Int(nullable: false));
            AddColumn("dbo.Mixes", "ReviewMixUrl", c => c.String());
            AddColumn("dbo.Mixes", "MasterArchiveUrl", c => c.String());
            AddColumn("dbo.Mixes", "ClientComments", c => c.String());
            AddColumn("dbo.Mixes", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Mixes", "Price");
            DropColumn("dbo.Mixes", "ClientComments");
            DropColumn("dbo.Mixes", "MasterArchiveUrl");
            DropColumn("dbo.Mixes", "ReviewMixUrl");
            DropColumn("dbo.Mixes", "ClientId");
            DropColumn("dbo.Mixes", "ClientUserId");
        }
    }
}
