namespace MMUserContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddHometown : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MixClients", "Hometown", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MixClients", "Hometown");
        }
    }
}
