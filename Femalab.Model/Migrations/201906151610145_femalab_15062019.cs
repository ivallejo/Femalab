namespace Femalab.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class femalab_15062019 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invoice", "SunatState", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Invoice", "SunatState");
        }
    }
}
