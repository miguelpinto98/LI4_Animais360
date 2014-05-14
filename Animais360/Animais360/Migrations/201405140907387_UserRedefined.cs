namespace Animais360.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserRedefined : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Email = c.String(),
                        Avatar = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
            DropTable("dbo.Utilizadores");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Utilizadores",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Email = c.String(),
                        Avatar = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
            DropTable("dbo.Users");
        }
    }
}
