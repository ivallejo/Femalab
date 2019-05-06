namespace Femalab.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class payment040520192 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customer",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        DocumentType = c.String(maxLength: 2),
                        Document = c.String(maxLength: 20),
                        FirstName = c.String(maxLength: 250),
                        TradeName = c.String(maxLength: 250),
                        Country = c.String(maxLength: 2),
                        Department = c.String(maxLength: 2),
                        Province = c.String(maxLength: 2),
                        District = c.String(maxLength: 2),
                        Address = c.String(maxLength: 250),
                        Phone = c.String(maxLength: 20),
                        Email = c.String(maxLength: 250),
                        DomicileFiscalCode = c.String(maxLength: 4),
                        IdUbigeo = c.Long(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Ubigeo", t => t.IdUbigeo, cascadeDelete: true)
                .Index(t => t.IdUbigeo);
            
            CreateTable(
                "dbo.Invoice",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        VoucherType = c.String(maxLength: 2),
                        Series = c.String(maxLength: 2),
                        DocumentNumber = c.Long(nullable: false),
                        Currency = c.String(maxLength: 3),
                        IGV = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalSale = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalTax = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IssueDate = c.DateTime(nullable: false),
                        ExpirationDate = c.DateTime(nullable: false),
                        Observations = c.String(maxLength: 250),
                        CustomerId = c.Long(nullable: false),
                        AttentionId = c.Long(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Attention", t => t.AttentionId, cascadeDelete: true)
                .ForeignKey("dbo.Customer", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.AttentionId);
            
            CreateTable(
                "dbo.InvoiceDetails",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        InvoiceId = c.Long(nullable: false),
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
                .ForeignKey("dbo.Invoice", t => t.InvoiceId, cascadeDelete: true)
                .ForeignKey("dbo.Product", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.InvoiceId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Payment",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        InvoiceId = c.Long(nullable: false),
                        PaymentType = c.String(maxLength: 1),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Invoice", t => t.InvoiceId, cascadeDelete: true)
                .Index(t => t.InvoiceId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Payment", "InvoiceId", "dbo.Invoice");
            DropForeignKey("dbo.InvoiceDetails", "ProductId", "dbo.Product");
            DropForeignKey("dbo.InvoiceDetails", "InvoiceId", "dbo.Invoice");
            DropForeignKey("dbo.Invoice", "CustomerId", "dbo.Customer");
            DropForeignKey("dbo.Invoice", "AttentionId", "dbo.Attention");
            DropForeignKey("dbo.Customer", "IdUbigeo", "dbo.Ubigeo");
            DropIndex("dbo.Payment", new[] { "InvoiceId" });
            DropIndex("dbo.InvoiceDetails", new[] { "ProductId" });
            DropIndex("dbo.InvoiceDetails", new[] { "InvoiceId" });
            DropIndex("dbo.Invoice", new[] { "AttentionId" });
            DropIndex("dbo.Invoice", new[] { "CustomerId" });
            DropIndex("dbo.Customer", new[] { "IdUbigeo" });
            DropTable("dbo.Payment");
            DropTable("dbo.InvoiceDetails");
            DropTable("dbo.Invoice");
            DropTable("dbo.Customer");
        }
    }
}
