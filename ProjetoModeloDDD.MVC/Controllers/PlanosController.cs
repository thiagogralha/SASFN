using AutoMapper;
using SASF.Application.Interface;
using SASF.Domain.Entities;
using SASF.MVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SASF.MVC.Authorize;

namespace SASF.MVC.Controllers
{
    [PermissoesFiltro]
    public class PlanosController : Controller
    {
        private readonly IPlanoAppService _planoApp;
        private readonly ITituloAppService _tituloApp;
        private readonly IFichaAppService _fichaApp;

        public PlanosController(IPlanoAppService planoApp, ITituloAppService tituloApp, IFichaAppService fichaApp)
        {
            _planoApp = planoApp;
            _tituloApp = tituloApp;
            _fichaApp = fichaApp;
        }

        //[PermissoesFiltro(Roles = "Admin")]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Buscar Planos
        /// </summary>
        /// <returns></returns>
        public string GetPlanos()
        {
            try
            {
                var planos = _planoApp.GetAll();

                var resultado = planos.Select(c => new PlanoViewModel
                {
                    PlanoId = c.PlanoId,
                    Nome = c.Nome,
                    Ativo = c.Ativo,
                    Valor = c.Valor,
                    IsFamilar = c.IsFamilar,
                    DataCadastro = c.DataCadastro.ToString()
                });

                return JsonConvert.SerializeObject(resultado, new JsonSerializerSettings { Formatting = Formatting.Indented, ReferenceLoopHandling = ReferenceLoopHandling.Ignore });

            }
            catch (Exception e)
            {
                return JsonConvert.SerializeObject(new { erro = e.Message });
            }
        }

        /// <summary>
        /// Inserir um Plano
        /// </summary>
        /// <param name="plano">Plano</param>
        /// <returns></returns>
        public ActionResult InsertPlano(PlanoViewModel plano)
        {
            try
            {

                var planoDomain = Mapper.Map<PlanoViewModel, Plano>(plano);

                if (plano.PlanoId > 0)
                {
                    _planoApp.Update(planoDomain);

                    var idTodasFichas = _fichaApp.BuscarIdFichas(plano.PlanoId);

                    //ATUALIZA O VALOR DE TODOS OS TITULOS AINDA EM ABERTO
                    foreach (var item in idTodasFichas)
                    {
                        var titulos = _tituloApp.AtualizarTodosTituloPendente(plano.Valor, item, plano.Nome);
                    }
                    
                }
                else
                {
                    _planoApp.Add(planoDomain);
                }

                return Json(planoDomain, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { erro = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Excluir um plano
        /// </summary>
        /// <param name="id">numero do plano</param>
        /// <returns></returns>
        public JsonResult Excluir(int? id)
        {
            try
            {
                var plano = _planoApp.GetById(id.Value);
                _planoApp.Remove(plano);

                return Json(new { success = "Plano Excluído" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { erro = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}
