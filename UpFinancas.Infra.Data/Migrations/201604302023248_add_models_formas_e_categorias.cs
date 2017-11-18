namespace UpFinancas.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_models_formas_e_categorias : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.categorias",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        nome = c.String(nullable: false, maxLength: 150, unicode: false),
                        tipo = c.Int(nullable: false),
                        data_cadastro = c.DateTime(nullable: false),
                        data_desativacao = c.DateTime(),
                        usuario_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.usuarios", t => t.usuario_id)
                .Index(t => t.usuario_id);
            
            CreateTable(
                "dbo.formas",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        nome = c.String(nullable: false, maxLength: 150, unicode: false),
                        data_cadastro = c.DateTime(nullable: false),
                        data_desativacao = c.DateTime(),
                        usuario_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.usuarios", t => t.usuario_id)
                .Index(t => t.usuario_id);
            
            AddColumn("dbo.eventos", "categoria_id", c => c.Int());
            AddColumn("dbo.eventos", "forma_id", c => c.Int(nullable: false));
            CreateIndex("dbo.eventos", "categoria_id");
            CreateIndex("dbo.eventos", "forma_id");
            AddForeignKey("dbo.eventos", "categoria_id", "dbo.categorias", "id", cascadeDelete: true);
            AddForeignKey("dbo.eventos", "forma_id", "dbo.formas", "id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.categorias", "usuario_id", "dbo.usuarios");
            DropForeignKey("dbo.eventos", "forma_id", "dbo.formas");
            DropForeignKey("dbo.formas", "usuario_id", "dbo.usuarios");
            DropForeignKey("dbo.eventos", "categoria_id", "dbo.categorias");
            DropIndex("dbo.formas", new[] { "usuario_id" });
            DropIndex("dbo.eventos", new[] { "forma_id" });
            DropIndex("dbo.eventos", new[] { "categoria_id" });
            DropIndex("dbo.categorias", new[] { "usuario_id" });
            DropColumn("dbo.eventos", "forma_id");
            DropColumn("dbo.eventos", "categoria_id");
            DropTable("dbo.formas");
            DropTable("dbo.categorias");
        }
    }
}
