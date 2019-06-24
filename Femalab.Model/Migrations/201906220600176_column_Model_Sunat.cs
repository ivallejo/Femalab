namespace Femalab.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class column_Model_Sunat : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invoice", "SunatSerie", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Invoice", "SunatSerie");
        }
    }
}
