namespace Femalab.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductCodeSunat : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "SunatCode", c => c.String(maxLength: 8));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Product", "SunatCode");
        }
    }
}
