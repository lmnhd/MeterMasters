namespace MMUserContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddActiveRequest : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MixRequests", "Active", c => c.Boolean(nullable: false));
            AddColumn("dbo.MixRequests", "MixCancelled", c => c.Boolean(nullable: false));
            AddColumn("dbo.MixRequests", "MixComplete", c => c.Boolean(nullable: false));
            AddColumn("dbo.Mixes", "RequestId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Mixes", "RequestId");
            DropColumn("dbo.MixRequests", "MixComplete");
            DropColumn("dbo.MixRequests", "MixCancelled");
            DropColumn("dbo.MixRequests", "Active");
        }
    }
}
