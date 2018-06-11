using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SASF.MVC.ViewModels
{
    public class FichaViewModel
    {
        [Key]
        public int FichaId { get; set; }

        public int PlanoId { get; set; }
        public string PlanoNome { get; set; }        
        public string NomeCompleto { get; set; }
        public string CPF { get; set; }
        public string Endereco { get; set; }
        public string Cep { get; set; }
        public string Bairro { get; set; }
        public string Complemento { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }
        public string Telefone { get; set; }
        public string TelefoneOpcional { get; set; }
        public string Email { get; set; }
        public string DataNascimento { get; set; }
        public string DataFalecimento { get; set; }
        public string NumeroIdentidade { get; set; }
        public string Ativo { get; set; }
        public string Nacionalidade { get; set; }
        public string Naturalidade { get; set; }

        [AllowHtml]
        [DataType(DataType.MultilineText)]
        public String Description { get; set; }

        public virtual PlanoViewModel Plano { get; set; }

        public List<DependenteViewModel> Dependentes { get; set; }

        public List<TituloViewModel> Titulos { get; set; }
    }
}