namespace Animais360.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddForeignKeysToPaisANDAreaProtegida : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Continente", newName: "Continentes");
            RenameTable(name: "dbo.Ajuda", newName: "Ajudas");
            RenameTable(name: "dbo.AreaProtegida", newName: "AreaProtegidas");
            RenameTable(name: "dbo.Questao", newName: "Questoes");
            AddColumn("dbo.AreaProtegidas", "Pais_PaisID", c => c.Int());
            AddForeignKey("dbo.AreaProtegidas", "Pais_PaisID", "dbo.Paises", "PaisID");
            CreateIndex("dbo.AreaProtegidas", "Pais_PaisID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.AreaProtegidas", new[] { "Pais_PaisID" });
            DropForeignKey("dbo.AreaProtegidas", "Pais_PaisID", "dbo.Paises");
            DropColumn("dbo.AreaProtegidas", "Pais_PaisID");
            RenameTable(name: "dbo.Questoes", newName: "Questao");
            RenameTable(name: "dbo.AreaProtegidas", newName: "AreaProtegida");
            RenameTable(name: "dbo.Ajudas", newName: "Ajuda");
            RenameTable(name: "dbo.Continentes", newName: "Continente");
        }
    }
}
