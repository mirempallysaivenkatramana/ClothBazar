namespace ClothBazar.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addednewpropertiestouser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Name", c => c.String(nullable: false));
            AddColumn("dbo.AspNetUsers", "Address", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Address");
            DropColumn("dbo.AspNetUsers", "Name");
        }
    }
}
