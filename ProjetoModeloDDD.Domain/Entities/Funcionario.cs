namespace SASF.Domain.Entities
{
    public class Funcionario
    {
        public int FuncionarioId { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string IsAdmin { get; set; }


    }
}
