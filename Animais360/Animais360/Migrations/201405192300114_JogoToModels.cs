namespace Animais360.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class JogoToModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Jogoes",
                c => new
                    {
                        JogoId = c.Int(nullable: false, identity: true),
                        RespCertas = c.Int(nullable: false),
                        RespErradas = c.Int(nullable: false),
                        Personagem = c.String(),
                        Nivel = c.Int(nullable: false),
                        Pontos = c.Int(nullable: false),
                        DifQualitativa = c.Int(nullable: false),
                        Estado = c.Int(nullable: false),
                        Sucesso = c.Int(nullable: false),
                        DataInicio = c.DateTime(nullable: false),
                        DataFim = c.DateTime(nullable: false),
                        User_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.JogoId)
                .ForeignKey("dbo.Users", t => t.User_UserId)
                .Index(t => t.User_UserId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Jogoes", new[] { "User_UserId" });
            DropForeignKey("dbo.Jogoes", "User_UserId", "dbo.Users");
            DropTable("dbo.Jogoes");
        }
    }
}
