using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using UpFinancas.Domain.Entities;

namespace Upfinancas.MVC.ViewModel
{
    public class CategoriaViewModel
    {
        public CategoriaViewModel()
        {
            MontarListaTipos();
        }

        public int? Id { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Digite o nome da categoria.")]
        [StringLength(150, MinimumLength = 5, ErrorMessage = "Categoria deve possuir entre 5 e 150 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe o tipo da categoria.")]
        [Range((int)ETipoCategoria.Despesa, (int)ETipoCategoria.Todos, ErrorMessage = "Tipo de categoria inválida.")]
        public int Tipo { get; set; }

        public DateTime DataCadastro { get; private set; }
        public DateTime? DataDesativacao { get; private set; }

        public IEnumerable<SelectListItem> Tipos { get; private set; }

        public string GetStatus()
        {
            return DataDesativacao == null ? "Ativo" : "Inativo";
        }

        public string GetDataCadastroPtBr()
        {
            return DataCadastro.ToString("dd/MM/yyyy");
        }

        public void MontarListaTipos(int idSelecionado = 0)
        {
            Tipos = new List<SelectListItem>(){
                    new SelectListItem() {Text="Despesa", Value=((int)ETipoCategoria.Despesa).ToString()},
                    new SelectListItem() {Text="Receita", Value=((int)ETipoCategoria.Receita).ToString()},
                    new SelectListItem() {Text="Todos", Value=((int)ETipoCategoria.Todos).ToString()}
            };
        }

        public string GetTipo()
        {
            if (Tipo == (int)ETipoCategoria.Despesa)
                return ETipoCategoria.Despesa.ToString();

            if (Tipo == (int)ETipoCategoria.Receita)
                return ETipoCategoria.Receita.ToString();

            if (Tipo == (int)ETipoCategoria.Todos)
                return ETipoCategoria.Todos.ToString();

            return "";
        }
    }
}