using AutoMapper;
using Newtonsoft.Json;
using SASF.Application.Interface;
using SASF.Domain.Entities;
using SASF.MVC.Authorize;
using SASF.MVC.ViewModels;
using System;
using System.Web.Mvc;

namespace SASF.MVC.Controllers
{
    [PermissoesFiltro]
    public class DependentesController : Controller
    {
        private readonly IFichaAppService _fichaApp;
        private readonly IDependenteAppService _dependenteApp;

        public DependentesController(IFichaAppService fichaApp, IDependenteAppService dependenteApp)
        {
            _fichaApp = fichaApp;
            _dependenteApp = dependenteApp;
        }

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Buscar Todos Dependentes
        /// </summary>
        /// <returns></returns>
        public string GetDependentes()
        {
            try
            {
                var fichas = _dependenteApp.GetAll();
               
                return JsonConvert.SerializeObject(fichas, new JsonSerializerSettings { Formatting = Formatting.Indented, ReferenceLoopHandling = ReferenceLoopHandling.Ignore });

            }
            catch (Exception e)
            {
                return JsonConvert.SerializeObject(new { erro = e.Message });
            }
        }
        
        /// <summary>
        /// Inserir um Dependente
        /// </summary>
        /// <param name="dependente">Dependente</param>
        /// <returns></returns>
        public ActionResult InsertDependente(DependenteViewModel dependente)
        {
            try
            {
                var dependenteDomain = Mapper.Map<DependenteViewModel, Dependente>(dependente);

                if (dependente.DependenteId > 0)
                {
                    _dependenteApp.Update(dependenteDomain);
                }
                else
                {
                    _dependenteApp.Add(dependenteDomain);
                }

                return Json(dependenteDomain, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { erro = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Excluir um Dependente
        /// </summary>
        /// <param name="id">Número do Dependente</param>
        /// <returns></returns>
        public JsonResult Excluir(int? id)
        {
            try
            {
                var dependente = _dependenteApp.GetById(id.Value);
                _dependenteApp.Remove(dependente);

                return Json(new { success = "Dependente Excluído" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { erro = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}
