namespace Animais360.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAvatarToUserProfile : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Utilizadores", "Avatar", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Utilizadores", "Avatar");
        }
    }
}
