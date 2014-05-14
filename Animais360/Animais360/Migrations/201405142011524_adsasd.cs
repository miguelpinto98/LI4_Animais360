namespace Animais360.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adsasd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "NrVoltas", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "NrJogos", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "Estado", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "Tipo", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "DataRegisto", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "DataRegisto");
            DropColumn("dbo.Users", "Tipo");
            DropColumn("dbo.Users", "Estado");
            DropColumn("dbo.Users", "NrJogos");
            DropColumn("dbo.Users", "NrVoltas");
        }
    }
}
