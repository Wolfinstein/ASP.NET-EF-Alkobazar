namespace Alkobazar.Migrations.Identity
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "Logo", c => c.Binary());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "Logo", c => c.Binary(nullable: false));
        }
    }
}
