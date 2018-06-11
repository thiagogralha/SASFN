using SASF.Domain.Entities;
using SASF.Domain.Interfaces.Repositories;
using SASF.Domain.Interfaces.Services;
using System.Collections.Generic;

namespace SASF.Domain.Services
{
    public class TituloService : ServiceBase<Titulo>, ITituloService
    {
        private readonly ITituloRepository _tituloRepository;

        public TituloService(ITituloRepository tituloRepository)
            : base(tituloRepository)
        {
            _tituloRepository = tituloRepository;
        }

        public bool AtualizarTodosTituloPendente(float valor, int fichaId,string planoNome)
        {
            return _tituloRepository.AtualizarTodosTituloPendente(valor, fichaId, planoNome);
        }

        public Titulo BuscarTitulo(int id)
        {
            return _tituloRepository.BuscarTitulo(id);
        }

        public List<Titulo> BuscarTitulosPorFicha(int fichaId)
        {
            return _tituloRepository.BuscarTitulosPorFicha(fichaId);
        }
    }
}
