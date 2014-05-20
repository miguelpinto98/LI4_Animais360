namespace Animais360.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addAQA : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ajuda",
                c => new
                    {
                        AjudaID = c.Int(nullable: false, identity: true),
                        Grau = c.Int(nullable: false),
                        Pista = c.String(),
                        Questao_QuestaoID = c.Int(),
                    })
                .PrimaryKey(t => t.AjudaID)
                .ForeignKey("dbo.Questao", t => t.Questao_QuestaoID)
                .Index(t => t.Questao_QuestaoID);
            
            CreateTable(
                "dbo.AreaProtegida",
                c => new
                    {
                        AreaProtegidaID = c.Int(nullable: false, identity: true),
                        AreaNome = c.String(),
                        Longitude = c.String(),
                        Latitude = c.String(),
                        Descricao = c.String(),
                        Permitida = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AreaProtegidaID);
            
            CreateTable(
                "dbo.Questao",
                c => new
                    {
                        QuestaoID = c.Int(nullable: false, identity: true),
                        DifQuantitativa = c.Int(nullable: false),
                        Pergunta = c.String(),
                        Resposta = c.String(),
                        Hipoteses = c.String(),
                        Tipo = c.Int(nullable: false),
                        Imagem = c.String(),
                        AreaProtegida_AreaProtegidaID = c.Int(),
                    })
                .PrimaryKey(t => t.QuestaoID)
                .ForeignKey("dbo.AreaProtegida", t => t.AreaProtegida_AreaProtegidaID)
                .Index(t => t.AreaProtegida_AreaProtegidaID);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Questao", new[] { "AreaProtegida_AreaProtegidaID" });
            DropIndex("dbo.Ajuda", new[] { "Questao_QuestaoID" });
            DropForeignKey("dbo.Questao", "AreaProtegida_AreaProtegidaID", "dbo.AreaProtegida");
            DropForeignKey("dbo.Ajuda", "Questao_QuestaoID", "dbo.Questao");
            DropTable("dbo.Questao");
            DropTable("dbo.AreaProtegida");
            DropTable("dbo.Ajuda");
        }
    }
}
