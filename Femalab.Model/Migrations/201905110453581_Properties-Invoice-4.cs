namespace Femalab.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PropertiesInvoice4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invoice", "SunatPdf", c => c.String());
            AddColumn("dbo.Invoice", "SunatXml", c => c.String());
            AddColumn("dbo.Invoice", "SunatCdr", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Invoice", "SunatCdr");
            DropColumn("dbo.Invoice", "SunatXml");
            DropColumn("dbo.Invoice", "SunatPdf");
        }
    }
}
