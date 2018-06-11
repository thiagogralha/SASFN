using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SASF.MVC.ViewModels
{
    public class TituloViewModel
    {
        [Key]
        public int TituloId { get; set; }

        public string Status { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataBaixa { get; set; }
        public float Valor { get; set; }
        public int FichaId { get; set; }
        public int Mes { get; set; }
        public int Ano { get; set; }
        public string FichaNome { get; set; }
        public string PlanoNome { get; set; }

    }
}