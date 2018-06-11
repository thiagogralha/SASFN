using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SASF.MVC.ViewModels
{
    public class DependenteViewModel
    {
        [Key]
        public int DependenteId { get; set; }

        public string NomeCompletoDep { get; set; }
        public string CPFDep { get; set; }
        public string EnderecoDep { get; set; }
        public string CepDep { get; set; }
        public string BairroDep { get; set; }
        public string ComplementoDep { get; set; }
        public string EstadoDep { get; set; }
        public string CidadeDep { get; set; }
        public string TelefoneDep { get; set; }
        public string TelefoneOpcionalDep { get; set; }
        public string EmailDep { get; set; }
        public string DataNascimentoDep { get; set; }
        public int FichaId { get; set; }
        public string NacionalidadeDep { get; set; }
        public string NaturalidadeDep { get; set; }


    }
}