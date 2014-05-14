namespace Animais360.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddValuesToUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Tipo", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Tipo");
        }
    }
}
