namespace MMUserContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMixType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MixRequests", "MyMixType", c => c.Int(nullable: false));
            DropColumn("dbo.MixRequests", "VersType");
            DropColumn("dbo.MixRequests", "VersType2");
            DropColumn("dbo.MixRequests", "VersionType3");
            DropColumn("dbo.MixRequests", "VersionType4");
            DropColumn("dbo.MixRequests", "VersionType5");
            DropColumn("dbo.MixRequests", "VersionType6");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MixRequests", "VersionType6", c => c.Int(nullable: false));
            AddColumn("dbo.MixRequests", "VersionType5", c => c.Int(nullable: false));
            AddColumn("dbo.MixRequests", "VersionType4", c => c.Int(nullable: false));
            AddColumn("dbo.MixRequests", "VersionType3", c => c.Int(nullable: false));
            AddColumn("dbo.MixRequests", "VersType2", c => c.Int(nullable: false));
            AddColumn("dbo.MixRequests", "VersType", c => c.Int(nullable: false));
            DropColumn("dbo.MixRequests", "MyMixType");
        }
    }
}
