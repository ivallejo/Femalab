namespace Femalab.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PropertiesInvoice : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invoice", "SunatNumber", c => c.String());
            AddColumn("dbo.Invoice", "SunatFilename", c => c.String());
            AddColumn("dbo.Invoice", "SunatExternalId", c => c.String());
            AddColumn("dbo.Invoice", "SunatNumberToLetter", c => c.String());
            AddColumn("dbo.Invoice", "SunatHash", c => c.String());
            AddColumn("dbo.Invoice", "SunatQr", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Invoice", "SunatQr");
            DropColumn("dbo.Invoice", "SunatHash");
            DropColumn("dbo.Invoice", "SunatNumberToLetter");
            DropColumn("dbo.Invoice", "SunatExternalId");
            DropColumn("dbo.Invoice", "SunatFilename");
            DropColumn("dbo.Invoice", "SunatNumber");
        }
    }
}
