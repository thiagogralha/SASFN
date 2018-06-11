using SASF.Domain.Entities;
using System.Collections.Generic;

namespace SASF.Domain.Interfaces.Repositories
{
    public interface ITituloRepository : IRepositoryBase<Titulo>
    {
        Titulo BuscarTitulo(int id);

        bool AtualizarTodosTituloPendente(float valor, int fichaId, string planoNome);

        List<Titulo> BuscarTitulosPorFicha(int fichaId);
    }
}
