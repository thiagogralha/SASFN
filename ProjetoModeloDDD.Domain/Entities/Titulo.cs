using System;

namespace SASF.Domain.Entities
{
    public class Titulo
    {
        public int TituloId { get; set; }
        public string Status { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataBaixa { get; set; }
        public float Valor { get; set; }
        public int FichaId { get; set; }
        public int Mes { get; set; }
        public int Ano { get; set; }
        public virtual Ficha Ficha { get; set; }
        public string PlanoNome { get; set; }

    }
}
