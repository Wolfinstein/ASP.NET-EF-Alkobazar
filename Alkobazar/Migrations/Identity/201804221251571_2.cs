namespace Alkobazar.Migrations.Identity
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "CreateTimeStamp");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "CreateTimeStamp", c => c.DateTime(nullable: false));
        }
    }
}
