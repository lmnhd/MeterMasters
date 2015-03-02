namespace MMUserContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDAW : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MixRequests", "ArchiveType", c => c.Int(nullable: false));
            AddColumn("dbo.MixRequests", "DAW", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MixRequests", "DAW");
            DropColumn("dbo.MixRequests", "ArchiveType");
        }
    }
}
