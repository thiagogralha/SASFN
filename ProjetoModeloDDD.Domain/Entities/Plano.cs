using System;

namespace SASF.Domain.Entities
{
    public class Plano
    {
        public int PlanoId { get; set; }
        public string Nome { get; set; }
        public string Ativo { get; set; }
        public float Valor { get; set; }        
        public DateTime DataCadastro { get; set; }
        public string IsFamilar { get; set; }
    }
}
