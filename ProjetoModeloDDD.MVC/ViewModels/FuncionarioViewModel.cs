using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SASF.MVC.ViewModels
{
    public class FuncionarioViewModel
    {
        public int FuncionarioId { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }

        [DataType(DataType.Password)]
        public string Senha { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string IsAdmin { get; set; }
        public string Roles { get; set; }
        
        public string Mensagem { get; set; }

    }
}