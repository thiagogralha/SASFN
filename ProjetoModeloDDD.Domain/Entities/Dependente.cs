namespace SASF.Domain.Entities
{
    public class Dependente
    {
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
        public string NacionalidadeDep { get; set; }
        public string NaturalidadeDep { get; set; }



        public int FichaId { get; set; }
        public virtual Ficha Ficha { get; set; }
    }
}
