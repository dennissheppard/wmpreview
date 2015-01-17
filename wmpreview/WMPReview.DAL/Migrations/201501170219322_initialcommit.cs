namespace WMPReview.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialcommit : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Businesses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Lat = c.Double(),
                        Long = c.Double(),
                        Address1 = c.String(),
                        Address2 = c.String(),
                        City = c.String(),
                        Locality = c.String(),
                        PostalCode = c.String(),
                        Country = c.String(),
                        YelpId = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Rating = c.Int(),
                        BusinessId = c.Int(nullable: false),
                        Text = c.String(),
                        UserId = c.String(),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Businesses", t => t.BusinessId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.BusinessId)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TagBusinesses",
                c => new
                    {
                        Tag_Id = c.Int(nullable: false),
                        Business_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_Id, t.Business_Id })
                .ForeignKey("dbo.Tags", t => t.Tag_Id, cascadeDelete: true)
                .ForeignKey("dbo.Businesses", t => t.Business_Id, cascadeDelete: true)
                .Index(t => t.Tag_Id)
                .Index(t => t.Business_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TagBusinesses", "Business_Id", "dbo.Businesses");
            DropForeignKey("dbo.TagBusinesses", "Tag_Id", "dbo.Tags");
            DropForeignKey("dbo.Reviews", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Reviews", "BusinessId", "dbo.Businesses");
            DropIndex("dbo.TagBusinesses", new[] { "Business_Id" });
            DropIndex("dbo.TagBusinesses", new[] { "Tag_Id" });
            DropIndex("dbo.Reviews", new[] { "User_Id" });
            DropIndex("dbo.Reviews", new[] { "BusinessId" });
            DropTable("dbo.TagBusinesses");
            DropTable("dbo.Tags");
            DropTable("dbo.Users");
            DropTable("dbo.Reviews");
            DropTable("dbo.Businesses");
        }
    }
}
