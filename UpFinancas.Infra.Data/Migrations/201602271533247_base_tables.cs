namespace UpFinancas.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class base_tables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.contas",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        nome = c.String(nullable: false, maxLength: 150, unicode: false),
                        data_cadastro = c.DateTime(nullable: false),
                        data_desativacao = c.DateTime(),
                        padrao = c.Int(nullable: false),
                        usuario_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.usuarios", t => t.usuario_id, cascadeDelete: true)
                .Index(t => t.usuario_id);
            
            CreateTable(
                "dbo.usuarios",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        nome = c.String(nullable: false, maxLength: 150, unicode: false),
                        email = c.String(nullable: false, maxLength: 150, unicode: false),
                        senha = c.String(maxLength: 300, unicode: false),
                        data_cadastro = c.DateTime(nullable: false),
                        data_cancelamento = c.DateTime(),
                        data_reativacao = c.DateTime(),
                        data_confirmacao = c.DateTime(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.contas", "usuario_id", "dbo.usuarios");
            DropIndex("dbo.contas", new[] { "usuario_id" });
            DropTable("dbo.usuarios");
            DropTable("dbo.contas");
        }
    }
}
