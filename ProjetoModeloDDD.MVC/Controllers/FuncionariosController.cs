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
using System.Web.Security;
using SASF.MVC.Authorize;

namespace SASF.MVC.Controllers
{
    [PermissoesFiltro]
    public class FuncionariosController : Controller
    {
        private readonly IFuncionarioAppService _funcionarioApp;

        public FuncionariosController(IFuncionarioAppService funcionarioApp)
        {
            _funcionarioApp = funcionarioApp;
        }

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Buscar todos Funcionarios
        /// </summary>
        /// <returns></returns>
        public string GetFuncionarios()
        {
            try
            {
                var funcionarios = _funcionarioApp.GetAllNoTracking();

                return JsonConvert.SerializeObject(funcionarios, new JsonSerializerSettings { Formatting = Formatting.Indented, ReferenceLoopHandling = ReferenceLoopHandling.Ignore });

            }
            catch (Exception e)
            {
                return JsonConvert.SerializeObject(new { erro = e.Message });
            }
        }

        /// <summary>
        /// Inserir um novo funcionario
        /// </summary>
        /// <param name="funcionario">Funcionario</param>
        /// <returns></returns>
        public ActionResult InsertFuncionario(FuncionarioViewModel funcionario)
        {
            try
            {
                var funcionarioDomain = Mapper.Map<FuncionarioViewModel, Funcionario>(funcionario);

                if (funcionario.FuncionarioId > 0)
                {
                    _funcionarioApp.Update(funcionarioDomain);
                }
                else
                {
                    _funcionarioApp.Add(funcionarioDomain);
                }

                return Json(funcionarioDomain, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { erro = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Excluir um funcionario
        /// </summary>
        /// <param name="id">numero do funcionario</param>
        /// <returns></returns>
        public JsonResult Excluir(int? id)
        {
            try
            {
                var funcionario = _funcionarioApp.GetById(id.Value);
                _funcionarioApp.Remove(funcionario);

                return Json(new { success = "Funcionario Excluído" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { erro = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}
