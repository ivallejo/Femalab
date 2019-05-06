namespace Femalab.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class payment04052019 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ubigeo",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Code = c.String(nullable: false, maxLength: 6),
                        Description = c.String(nullable: false, maxLength: 250),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Ubigeo");
        }
    }
}
