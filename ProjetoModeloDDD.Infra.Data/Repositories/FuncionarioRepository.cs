using SASF.Domain.Entities;
using SASF.Domain.Interfaces.Repositories;
using System.Linq;

namespace SASF.Infra.Data.Repositories
{
    public class FuncionarioRepository : RepositoryBase<Funcionario>, IFuncionarioRepository
    {
        public Funcionario BuscarDetalhesFuncionario(string login, string senha)
        {
            return Db.Funcionarios.Where(d => d.Login == login && d.Senha == senha).FirstOrDefault();
        }
    }
}
