namespace Femalab.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class column_state_invoice : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invoice", "State", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Invoice", "State");
        }
    }
}
