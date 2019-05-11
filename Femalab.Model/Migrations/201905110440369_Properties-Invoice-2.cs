namespace Femalab.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PropertiesInvoice2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invoice", "ApiSuccess", c => c.String());
            AddColumn("dbo.Invoice", "ApiMessage", c => c.Boolean(nullable: false));
            AddColumn("dbo.Invoice", "ApiFile", c => c.String());
            AddColumn("dbo.Invoice", "ApiLine", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Invoice", "ApiLine");
            DropColumn("dbo.Invoice", "ApiFile");
            DropColumn("dbo.Invoice", "ApiMessage");
            DropColumn("dbo.Invoice", "ApiSuccess");
        }
    }
}
