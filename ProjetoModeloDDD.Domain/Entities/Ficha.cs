using System.Collections.Generic;

namespace SASF.Domain.Entities
{
    public class Ficha
    {
        public Ficha()
        {
            Dependentes = new List<Dependente>();
            Titulos = new List<Titulo>();
        }

        public int FichaId { get; set; }
        
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

        public int PlanoId { get; set; }
        public virtual Plano Plano { get; set; }
        
        public virtual ICollection<Dependente> Dependentes { get; set; }

        public virtual ICollection<Titulo> Titulos { get; set; }
    }
}
