using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SASF.MVC.ViewModels
{
    public class PlanoViewModel
    {
        [Key]
        public int PlanoId { get; set; }
        public string Nome { get; set; }
        public string Ativo { get; set; }
        public string DataCadastro { get; set; }
        public float Valor { get; set; }
        public string IsFamilar { get; set; }
    }
}