using AutoMapper;
using Upfinancas.MVC.ViewModel;
using UpFinancas.Domain.Entities;

namespace Upfinancas.MVC.Mappers
{
    public class ViewModelToDomainMappingProfile:Profile
    {
        public override string ProfileName
        {
            get { return "ViewModelToDomainMappings"; }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<UsuarioViewModel, Usuario>();
            Mapper.CreateMap<ContaViewModel, Conta>();
            Mapper.CreateMap<EventoViewModel, Evento>();
            Mapper.CreateMap<PessoaViewModel, Pessoa>();
            Mapper.CreateMap<CategoriaViewModel, Categoria>();
            Mapper.CreateMap<FormaViewModel, Forma>();
        }
    }
}