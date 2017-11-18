namespace UpFinancas.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class model_evento : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.eventos",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        descricao = c.String(nullable: false, maxLength: 150, unicode: false),
                        tipo = c.Int(nullable: false),
                        valor = c.Double(nullable: false),
                        valor_juros = c.Double(nullable: false),
                        valor_desconto = c.Double(nullable: false),
                        observacao = c.String(maxLength: 300, unicode: false),
                        data_lancamento = c.DateTime(nullable: false),
                        data_vencimento = c.DateTime(),
                        data_pagamento = c.DateTime(),
                        conta_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.contas", t => t.conta_id, cascadeDelete: true)
                .Index(t => t.conta_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.eventos", "conta_id", "dbo.contas");
            DropIndex("dbo.eventos", new[] { "conta_id" });
            DropTable("dbo.eventos");
        }
    }
}
