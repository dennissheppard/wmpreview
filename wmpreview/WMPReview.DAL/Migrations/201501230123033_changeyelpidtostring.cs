namespace WMPReview.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeyelpidtostring : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Businesses", "YelpId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Businesses", "YelpId", c => c.Int());
        }
    }
}
