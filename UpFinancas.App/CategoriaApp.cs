
using System;
using System.Collections.Generic;
using System.Linq;
using UpFinancas.App.Interfaces;
using UpFinancas.Domain.Entities;
using UpFinancas.Domain.Interfaces.Repositories;

namespace UpFinancas.App
{
    public class CategoriaApp : ICategoriaApp
    {
        private readonly ICategoriaRepository _repo;

        public CategoriaApp(ICategoriaRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<Categoria> Listar(int usuario_id)
        {
            var categorias = _repo.Listar(usuario_id);
            return categorias;
        }

        public void Salvar(Categoria categoria)
        {
            if (categoria.Id>0)
                _repo.Alterar(categoria);
            else
                _repo.Salvar(categoria);
        }

        public void AlterarStatus(int id, int usuario_id)
        {
            var categoria = _repo.BuscarPorId(id, usuario_id);
            categoria.AlterarStatus();
            _repo.Alterar(categoria);
        }

        public void Excluir(int id, int usuario_id)
        {
            var categoria = _repo.BuscarPorId(id, usuario_id);
            _repo.Excluir(categoria);
        }


        public Categoria Buscar(int usuarioId,int pessoaId)
        {
            var categoria = _repo.BuscarPorId(pessoaId, usuarioId);
            if(categoria == null)
                throw new Exception("Categoria não encontrada!");
            return categoria;
        }

        public IEnumerable<Categoria> ListarSomenteAtivasPorTipo(int usuario_id,int tipo)
        {
            var categoria = Listar(usuario_id).Where(p => p.DataDesativacao == null&&(p.Tipo==3 ||p.Tipo==tipo)).OrderBy(p => p.Nome);

            return categoria;

        }

        public void SalvarCategoriasPadrao(int usuarioId)
        {
            var categoria = new Categoria("Alimentação", (int)ETipoCategoria.Despesa, usuarioId);
            Salvar(categoria);

            categoria = new Categoria("Transporte", (int)ETipoCategoria.Despesa, usuarioId);
            Salvar(categoria);

            categoria = new Categoria("Lazer", (int)ETipoCategoria.Despesa, usuarioId);
            Salvar(categoria);

            categoria = new Categoria("Vestuário", (int)ETipoCategoria.Despesa, usuarioId);
            Salvar(categoria);

            categoria = new Categoria("Saúde", (int)ETipoCategoria.Despesa, usuarioId);
            Salvar(categoria);

            categoria = new Categoria("Beleza", (int)ETipoCategoria.Despesa, usuarioId);
            Salvar(categoria);

            categoria = new Categoria("Educação", (int)ETipoCategoria.Despesa, usuarioId);
            Salvar(categoria);

            categoria = new Categoria("Moradia", (int)ETipoCategoria.Despesa, usuarioId);
            Salvar(categoria);

            categoria = new Categoria("Segurança", (int)ETipoCategoria.Despesa, usuarioId);
            Salvar(categoria);

            categoria = new Categoria("Salário", (int)ETipoCategoria.Receita, usuarioId);
            Salvar(categoria);

            categoria = new Categoria("Investimentos", (int)ETipoCategoria.Receita, usuarioId);
            Salvar(categoria);

            categoria = new Categoria("Premiações", (int)ETipoCategoria.Receita, usuarioId);
            Salvar(categoria);

            categoria = new Categoria("Comissões", (int)ETipoCategoria.Receita, usuarioId);
            Salvar(categoria);

        }

        public void Dispose()
        {
            _repo.Dispose();
        }
    }
}
