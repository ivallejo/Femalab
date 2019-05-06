namespace Femalab.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PaymentState : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Payment", "State", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Payment", "State");
        }
    }
}
