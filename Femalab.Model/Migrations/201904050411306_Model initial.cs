namespace Femalab.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modelinitial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AttentionCategory",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Code = c.String(maxLength: 10),
                        Category = c.String(maxLength: 150),
                        Tag = c.String(maxLength: 30),
                        Action = c.String(maxLength: 100),
                        State = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Attention",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Code = c.String(maxLength: 10),
                        Age = c.Int(nullable: false),
                        Visits = c.Int(nullable: false),
                        Weight = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Size = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Height = c.Decimal(nullable: false, precision: 18, scale: 2),
                        QueryBy = c.String(),
                        PhysicalExam = c.String(),
                        LaboratoryExam = c.String(),
                        FamilyHistory = c.String(),
                        Diagnosis = c.String(),
                        Treatment = c.String(),
                        State = c.Boolean(nullable: false),
                        PatientId = c.Long(nullable: false),
                        DoctorId = c.Long(nullable: false),
                        AttentionTypeId = c.Long(nullable: false),
                        AttentionCategoryId = c.Long(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AttentionCategory", t => t.AttentionCategoryId, cascadeDelete: true)
                .ForeignKey("dbo.AttentionType", t => t.AttentionTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Doctor", t => t.DoctorId, cascadeDelete: true)
                .ForeignKey("dbo.Patient", t => t.PatientId, cascadeDelete: true)
                .Index(t => t.PatientId)
                .Index(t => t.DoctorId)
                .Index(t => t.AttentionTypeId)
                .Index(t => t.AttentionCategoryId);
            
            CreateTable(
                "dbo.AttentionDetails",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        AttentionId = c.Long(nullable: false),
                        ProductId = c.Long(nullable: false),
                        Description = c.String(maxLength: 500),
                        Quantity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Attention", t => t.AttentionId, cascadeDelete: true)
                .ForeignKey("dbo.Product", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.AttentionId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Code = c.String(maxLength: 10),
                        CategoryId = c.Long(nullable: false),
                        Description = c.String(maxLength: 250),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StockInitial = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Observation = c.String(maxLength: 500),
                        State = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Category", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Category",
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
            
            CreateTable(
                "dbo.AttentionType",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Code = c.String(maxLength: 10),
                        Type = c.String(maxLength: 250),
                        Tag = c.String(maxLength: 30),
                        State = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Doctor",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Code = c.String(maxLength: 10),
                        FullName = c.String(maxLength: 250),
                        State = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Patient",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Code = c.String(maxLength: 10),
                        FirstName = c.String(maxLength: 250),
                        LastName = c.String(maxLength: 250),
                        DocumentType = c.String(maxLength: 2),
                        Document = c.String(maxLength: 20),
                        Gender = c.String(maxLength: 1),
                        BirthDate = c.DateTime(nullable: false),
                        Address = c.String(maxLength: 550),
                        Email = c.String(maxLength: 250),
                        Phone = c.String(maxLength: 20),
                        Observation = c.String(maxLength: 550),
                        State = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Attention", "PatientId", "dbo.Patient");
            DropForeignKey("dbo.Attention", "DoctorId", "dbo.Doctor");
            DropForeignKey("dbo.Attention", "AttentionTypeId", "dbo.AttentionType");
            DropForeignKey("dbo.Product", "CategoryId", "dbo.Category");
            DropForeignKey("dbo.AttentionDetails", "ProductId", "dbo.Product");
            DropForeignKey("dbo.AttentionDetails", "AttentionId", "dbo.Attention");
            DropForeignKey("dbo.Attention", "AttentionCategoryId", "dbo.AttentionCategory");
            DropIndex("dbo.Product", new[] { "CategoryId" });
            DropIndex("dbo.AttentionDetails", new[] { "ProductId" });
            DropIndex("dbo.AttentionDetails", new[] { "AttentionId" });
            DropIndex("dbo.Attention", new[] { "AttentionCategoryId" });
            DropIndex("dbo.Attention", new[] { "AttentionTypeId" });
            DropIndex("dbo.Attention", new[] { "DoctorId" });
            DropIndex("dbo.Attention", new[] { "PatientId" });
            DropTable("dbo.Patient");
            DropTable("dbo.Doctor");
            DropTable("dbo.AttentionType");
            DropTable("dbo.Category");
            DropTable("dbo.Product");
            DropTable("dbo.AttentionDetails");
            DropTable("dbo.Attention");
            DropTable("dbo.AttentionCategory");
        }
    }
}
