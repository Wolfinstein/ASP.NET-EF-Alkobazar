namespace Alkobazar.Migrations.Identity
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "Name", c => c.String(nullable: false));
            AddColumn("dbo.Customers", "Phone", c => c.String(nullable: false));
            AddColumn("dbo.Customers", "Logo", c => c.Binary(nullable: false));
            DropColumn("dbo.Customers", "Company_name");
            DropColumn("dbo.Customers", "Customer_phone");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "Customer_phone", c => c.String(nullable: false));
            AddColumn("dbo.Customers", "Company_name", c => c.String(nullable: false));
            DropColumn("dbo.Customers", "Logo");
            DropColumn("dbo.Customers", "Phone");
            DropColumn("dbo.Customers", "Name");
        }
    }
}
