using AutoMapper;
using Upfinancas.MVC.ViewModel;
using UpFinancas.Domain.Entities;

namespace Upfinancas.MVC.Mappers
{
    public class DomainToViewModelMappingProfile:Profile
    {
        public override string ProfileName
        {
            get { return "DomainToViewModelMappings"; }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<Usuario, UsuarioViewModel>();
            Mapper.CreateMap<Conta, ContaViewModel>();
            Mapper.CreateMap<Evento, EventoViewModel>();
            Mapper.CreateMap<Pessoa, PessoaViewModel>();
            Mapper.CreateMap<Categoria, CategoriaViewModel>();
            Mapper.CreateMap<Forma, FormaViewModel>();
        }
    }
}