using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using UpFinancas.Domain.Entities;
using UpFinancas.Domain.Interfaces.Repositories;
using UpFinancas.Infra.Data.Context;

namespace UpFinancas.Infra.Data.Repositories
{
    public class ContaRepository : IContaRepository, IDisposable
    {
        private readonly UpFinancasContext _context;

        public ContaRepository(UpFinancasContext context)
        {
            _context = context;
        }

        public void Alterar(Conta conta)
        {
            _context.Entry(conta).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public Conta buscarContaPadrao(int usuario_id, int contaPadraoAtualId)
        {
            return _context.Contas.FirstOrDefault(c => c.UsuarioId == usuario_id && c.Padrao==2 && c.Id!=contaPadraoAtualId);
        }

        public Conta BuscarPorId(int id, int usuario_id)
        {
            return _context.Contas.FirstOrDefault(c=>c.UsuarioId==usuario_id && c.Id==id);
        }
        public void Excluir(Conta conta)
        {
            _context.Contas.Remove(conta);
            _context.SaveChanges();
        }

        public IEnumerable<Conta> Listar(int usuario_id)
        {
            return _context.Contas.Where(c => c.UsuarioId == usuario_id).ToList();
        }

        public void Salvar(Conta conta)
        {
            _context.Contas.Add(conta);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
