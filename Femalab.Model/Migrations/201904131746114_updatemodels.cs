namespace Femalab.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatemodels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Specialty",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Code = c.String(maxLength: 10),
                        Description = c.String(maxLength: 250),
                        Observation = c.String(maxLength: 500),
                        State = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Product", "SpecialtyId", c => c.Long());
            CreateIndex("dbo.Product", "SpecialtyId");
            AddForeignKey("dbo.Product", "SpecialtyId", "dbo.Specialty", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Product", "SpecialtyId", "dbo.Specialty");
            DropIndex("dbo.Product", new[] { "SpecialtyId" });
            DropColumn("dbo.Product", "SpecialtyId");
            DropTable("dbo.Specialty");
        }
    }
}
