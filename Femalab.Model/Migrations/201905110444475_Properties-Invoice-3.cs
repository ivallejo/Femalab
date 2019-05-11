namespace Femalab.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PropertiesInvoice3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Invoice", "ApiSuccess", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Invoice", "ApiMessage", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Invoice", "ApiMessage", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Invoice", "ApiSuccess", c => c.String());
        }
    }
}
