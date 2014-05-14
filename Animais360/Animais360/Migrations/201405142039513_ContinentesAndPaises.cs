namespace Animais360.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ContinentesAndPaises : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Continentes",
                c => new
                    {
                        ContinenteId = c.Int(nullable: false, identity: true),
                        ContinenteName = c.String(),
                        Descricao = c.String(),
                    })
                .PrimaryKey(t => t.ContinenteId);
            
            CreateTable(
                "dbo.Paises",
                c => new
                    {
                        PaisID = c.Int(nullable: false, identity: true),
                        PaisNome = c.String(),
                        Descricao = c.String(),
                        Continente_ContinenteId = c.Int(),
                    })
                .PrimaryKey(t => t.PaisID)
                .ForeignKey("dbo.Continentes", t => t.Continente_ContinenteId)
                .Index(t => t.Continente_ContinenteId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Paises", new[] { "Continente_ContinenteId" });
            DropForeignKey("dbo.Paises", "Continente_ContinenteId", "dbo.Continentes");
            DropTable("dbo.Paises");
            DropTable("dbo.Continentes");
        }
    }
}
