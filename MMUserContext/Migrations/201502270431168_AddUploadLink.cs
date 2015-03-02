namespace MMUserContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUploadLink : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MixRequests", "VersType2", c => c.Int(nullable: false));
            AddColumn("dbo.MixRequests", "VersionType3", c => c.Int(nullable: false));
            AddColumn("dbo.MixRequests", "VersionType4", c => c.Int(nullable: false));
            AddColumn("dbo.MixRequests", "VersionType5", c => c.Int(nullable: false));
            AddColumn("dbo.MixRequests", "VersionType6", c => c.Int(nullable: false));
            AddColumn("dbo.MixRequests", "UploadLink", c => c.String());
            AddColumn("dbo.MixRequests", "Genre", c => c.String());
            AddColumn("dbo.MixRequests", "ModifyNotes", c => c.String());
            AddColumn("dbo.MixRequests", "ClientNotes", c => c.String());
            AddColumn("dbo.MixRequests", "EngineerNotes", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MixRequests", "EngineerNotes");
            DropColumn("dbo.MixRequests", "ClientNotes");
            DropColumn("dbo.MixRequests", "ModifyNotes");
            DropColumn("dbo.MixRequests", "Genre");
            DropColumn("dbo.MixRequests", "UploadLink");
            DropColumn("dbo.MixRequests", "VersionType6");
            DropColumn("dbo.MixRequests", "VersionType5");
            DropColumn("dbo.MixRequests", "VersionType4");
            DropColumn("dbo.MixRequests", "VersionType3");
            DropColumn("dbo.MixRequests", "VersType2");
        }
    }
}
