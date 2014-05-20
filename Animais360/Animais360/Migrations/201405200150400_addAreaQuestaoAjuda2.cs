namespace Animais360.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addAreaQuestaoAjuda2 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Continentes", newName: "Continente");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Continente", newName: "Continentes");
        }
    }
}
