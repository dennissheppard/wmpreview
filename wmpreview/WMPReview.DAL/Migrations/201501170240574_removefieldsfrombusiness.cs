namespace WMPReview.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removefieldsfrombusiness : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Businesses", "Description");
            DropColumn("dbo.Businesses", "Address1");
            DropColumn("dbo.Businesses", "Address2");
            DropColumn("dbo.Businesses", "City");
            DropColumn("dbo.Businesses", "Locality");
            DropColumn("dbo.Businesses", "PostalCode");
            DropColumn("dbo.Businesses", "Country");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Businesses", "Country", c => c.String());
            AddColumn("dbo.Businesses", "PostalCode", c => c.String());
            AddColumn("dbo.Businesses", "Locality", c => c.String());
            AddColumn("dbo.Businesses", "City", c => c.String());
            AddColumn("dbo.Businesses", "Address2", c => c.String());
            AddColumn("dbo.Businesses", "Address1", c => c.String());
            AddColumn("dbo.Businesses", "Description", c => c.String());
        }
    }
}
