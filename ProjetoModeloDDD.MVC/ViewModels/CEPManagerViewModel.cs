using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SASF.MVC.ViewModels
{
    public class CEPManagerViewModel
    {
        public string CEP { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }


        public GPS GPS { get; set; }
    }

    public class GPS
    {
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
    }

}