namespace UpFinancas.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_email_unico : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.usuarios", "email", unique: true, name: "IX_EMAIL_USUARIO");
        }
        
        public override void Down()
        {
            DropIndex("dbo.usuarios", "IX_EMAIL_USUARIO");
        }
    }
}
