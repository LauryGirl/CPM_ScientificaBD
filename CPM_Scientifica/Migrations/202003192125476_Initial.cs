namespace CPM_Scientifica.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Registers",
                c => new
                    {
                        RegisterId = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        NewRegister = c.DateTime(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Documentation = c.String(),
                        Validity = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Product_ProductId = c.Int(),
                        Product_ProductId1 = c.Int(),
                    })
                .PrimaryKey(t => t.RegisterId)
                .ForeignKey("dbo.Products", t => t.Product_ProductId)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.Product_ProductId1)
                .Index(t => t.ProductId)
                .Index(t => t.Product_ProductId)
                .Index(t => t.Product_ProductId1);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        RegisterPresent = c.Boolean(nullable: false),
                        System = c.String(),
                        Family = c.String(),
                        Application = c.String(),
                        Presentation = c.String(),
                        Ref = c.String(),
                        MakerId = c.Int(nullable: false),

                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.Makers", t => t.MakerId, cascadeDelete: true)
                .Index(t => t.MakerId);
            
            CreateTable(
                "dbo.Changes",
                c => new
                    {
                        ChangeId = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Reason = c.String(),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ChangeId)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Inquests",
                c => new
                    {
                        InquestId = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Receiver = c.String(),
                        Center = c.String(),
                        Recommendation = c.String(),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.InquestId)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Makers",
                c => new
                    {
                        MakerId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        AuthorityOfCommerceCameraDate = c.DateTime(nullable: false),
                        LifeTimeYearsACC = c.Int(nullable: false),
                        AuthorityOfCommerceCamera = c.String(),
                        GoodPracticesDocumentationDate = c.DateTime(nullable: false),
                        LifeTimeYearsGPD = c.Int(nullable: false),
                        GoodPracticesDocumentation = c.String(),
                        Country = c.String(),
                        CertificateFreeSaleDate = c.DateTime(),
                        CertificateFreeSale = c.String(),
                        LifeTimeYearsCFS = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.MakerId);
            
            CreateTable(
                "dbo.Sales",
                c => new
                    {
                        SaleId = c.Int(nullable: false, identity: true),
                        Center = c.String(),
                        Date = c.DateTime(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SaleId)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Wails",
                c => new
                    {
                        WailId = c.Int(nullable: false, identity: true),
                        CecmedInfo = c.String(),
                        MakerInfo = c.String(),
                        Closure = c.String(),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.WailId)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.YearlyRevisions",
                c => new
                    {
                        YearlyRevisionId = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        LimitRev = c.DateTime(nullable: false),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.YearlyRevisionId)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Registers", "Product_ProductId1", "dbo.Products");
            DropForeignKey("dbo.YearlyRevisions", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Wails", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Sales", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Registers", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Registers", "Product_ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "MakerId", "dbo.Makers");
            DropForeignKey("dbo.Inquests", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Changes", "ProductId", "dbo.Products");
            DropIndex("dbo.YearlyRevisions", new[] { "ProductId" });
            DropIndex("dbo.Wails", new[] { "ProductId" });
            DropIndex("dbo.Sales", new[] { "ProductId" });
            DropIndex("dbo.Inquests", new[] { "ProductId" });
            DropIndex("dbo.Changes", new[] { "ProductId" });
            DropIndex("dbo.Products", new[] { "MakerId" });
            DropIndex("dbo.Registers", new[] { "Product_ProductId1" });
            DropIndex("dbo.Registers", new[] { "Product_ProductId" });
            DropIndex("dbo.Registers", new[] { "ProductId" });
            DropTable("dbo.YearlyRevisions");
            DropTable("dbo.Wails");
            DropTable("dbo.Sales");
            DropTable("dbo.Makers");
            DropTable("dbo.Inquests");
            DropTable("dbo.Changes");
            DropTable("dbo.Products");
            DropTable("dbo.Registers");
        }
    }
}
