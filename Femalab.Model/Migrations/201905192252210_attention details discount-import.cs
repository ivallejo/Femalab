namespace Femalab.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class attentiondetailsdiscountimport : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AttentionDetails", "Discount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.AttentionDetails", "Import", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AttentionDetails", "Import");
            DropColumn("dbo.AttentionDetails", "Discount");
        }
    }
}
