namespace Animais360.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class JogoToModels1 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Jogoes", newName: "Jogos");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Jogos", newName: "Jogoes");
        }
    }
}
