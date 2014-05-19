namespace Animais360.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ClassificacaoToUser : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Classificacoes",
                c => new
                    {
                        ClassificacaoId = c.Int(nullable: false, identity: true),
                        Dificuldade = c.Int(nullable: false),
                        pontos = c.Int(nullable: false),
                        User_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.ClassificacaoId)
                .ForeignKey("dbo.Users", t => t.User_UserId)
                .Index(t => t.User_UserId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Classificacoes", new[] { "User_UserId" });
            DropForeignKey("dbo.Classificacoes", "User_UserId", "dbo.Users");
            DropTable("dbo.Classificacoes");
        }
    }
}
