namespace Animais360.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DescricaoToUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Descricao", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Descricao");
        }
    }
}
