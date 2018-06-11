using SASF.Application.Interface;
using SASF.Domain.Entities;
using SASF.Domain.Interfaces.Services;
using System.Collections.Generic;

namespace SASF.Application
{
    public class TituloAppService : AppServiceBase<Titulo>, ITituloAppService
    {
        private readonly ITituloService _tituloService;

        public TituloAppService(ITituloService tituloService)
            : base(tituloService)
        {
            _tituloService = tituloService;
        }

        public bool AtualizarTodosTituloPendente(float valor, int fichaId, string planoNome)
        {
            return _tituloService.AtualizarTodosTituloPendente(valor, fichaId, planoNome);
        }

        public Titulo BuscarTitulo(int id)
        {
            return _tituloService.BuscarTitulo(id);
        }

        public List<Titulo> BuscarTitulosPorFicha(int fichaId)
        {
            return _tituloService.BuscarTitulosPorFicha(fichaId);
        }
    }
}
