namespace MMUserContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUnderRequest : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MixClients", "UnderReview", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MixClients", "UnderReview");
        }
    }
}
