namespace UpFinancas.Infra.Data.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class model_pessoa : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.pessoas",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        nome = c.String(nullable: false, maxLength: 150, unicode: false),
                        telefone = c.String(maxLength: 30, unicode: false),
                        celular = c.String(maxLength: 30, unicode: false),
                        email = c.String(maxLength: 150, unicode: false),
                        rua = c.String(maxLength: 150, unicode: false),
                        numero = c.String(maxLength: 20, unicode: false),
                        bairro = c.String(maxLength: 150, unicode: false),
                        cidade = c.String(maxLength: 150, unicode: false),
                        complemento = c.String(maxLength: 200, unicode: false),
                        data_cadastro = c.DateTime(nullable: false),
                        data_desativacao = c.DateTime(),
                        usuario_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.usuarios", t => t.usuario_id, cascadeDelete: true)
                .Index(t => t.usuario_id);
            
            AddColumn("dbo.eventos", "pessoa_id", c => c.Int());
            CreateIndex("dbo.eventos", "pessoa_id");
            AddForeignKey("dbo.eventos", "pessoa_id", "dbo.pessoas", "id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.pessoas", "usuario_id", "dbo.usuarios");
            DropForeignKey("dbo.eventos", "pessoa_id", "dbo.pessoas");
            DropIndex("dbo.pessoas", new[] { "usuario_id" });
            DropIndex("dbo.eventos", new[] { "pessoa_id" });
            DropColumn("dbo.eventos", "pessoa_id");
            DropTable("dbo.pessoas");
        }
    }
}
