using AutoMapper;
using Newtonsoft.Json;
using SASF.Application.Interface;
using SASF.Domain.Entities;
using SASF.MVC.Authorize;
using SASF.MVC.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace SASF.MVC.Controllers
{
    [PermissoesFiltro]
    public class FichasController : Controller
    {
        private readonly IFichaAppService _fichaApp;
        private readonly IPlanoAppService _planoApp;
        private readonly IDependenteAppService _dependenteApp;
        private readonly ITituloAppService _tituloApp;

        public FichasController(IFichaAppService fichaApp, IPlanoAppService planoApp, IDependenteAppService dependenteApp, ITituloAppService tituloApp)
        {
            _fichaApp = fichaApp;
            _planoApp = planoApp;
            _dependenteApp = dependenteApp;
            _tituloApp = tituloApp;
        }

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Buscar todas as fichas
        /// </summary>
        /// <returns></returns>
        public string GetFichas()
        {
            try
            {
                var fichas = _fichaApp.GetAllFichas();
                return JsonConvert.SerializeObject(fichas, new JsonSerializerSettings { Formatting = Formatting.Indented, ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            }
            catch (Exception e)
            {
                return JsonConvert.SerializeObject(new { erro = e.Message });
            }
        }

        /// <summary>
        /// Buscar todos os planos ativos
        /// </summary>
        /// <returns></returns>
        public JsonResult GetPlano()
        {
            try
            {
                var plano = _planoApp.GetAll().Where(x => x.Ativo == "S");

                var resultado = plano.Select(c => new PlanoViewModel
                {
                    PlanoId = c.PlanoId,
                    Nome = c.Nome,
                    Valor = c.Valor,
                    IsFamilar = c.IsFamilar
                });
                return Json(resultado, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                return Json(new { erro = e.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Inserir uma ficha
        /// </summary>
        /// <param name="ficha">Ficha</param>
        /// <returns></returns>
        public string InsertFicha(FichaViewModel ficha)
        {
            try
            {
                float valorPlano = 0;
                string planoNome = "";
                if (ficha.Plano != null)
                {
                    ficha.PlanoId = ficha.Plano.PlanoId;
                    valorPlano = ficha.Plano.Valor;
                    planoNome = ficha.Plano.Nome;
                }



                ficha.Plano = null;
                var fichaDomain = Mapper.Map<FichaViewModel, Ficha>(ficha);

                if (ficha.FichaId > 0)
                {
                    _fichaApp.Update(fichaDomain);

                    _tituloApp.AtualizarTodosTituloPendente(valorPlano, fichaDomain.FichaId, planoNome);
                }
                else
                {
                    _fichaApp.Add(fichaDomain);

                    //Adiciona quantidade de meses restantes de Títulos a nova ficha cadastrada.
                    for (int i = DateTime.Now.Month; i <= 12; i++)
                    {

                        _tituloApp.Add(new Titulo
                        {
                            FichaId = fichaDomain.FichaId,
                            DataCadastro = DateTime.Now,
                            Status = "P",
                            DataBaixa = null,
                            Valor = valorPlano,
                            Mes = i,
                            Ano = DateTime.Now.Year,
                            PlanoNome = planoNome
                        });
                    }

                }

                return JsonConvert.SerializeObject(fichaDomain, new JsonSerializerSettings { Formatting = Formatting.Indented, ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            }
            catch (Exception e)
            {
                return JsonConvert.SerializeObject(new { erro = e.Message });
            }
        }

        /// <summary>
        /// Excluir uma Ficha
        /// </summary>
        /// <param name="id">Numero da Ficha</param>
        /// <returns></returns>
        //public JsonResult Excluir(int? id)
        //{
        //    try
        //    {
        //        var dependente = _dependenteApp.BuscarporId(id.Value);

        //        if (dependente != null)
        //            return Json(new { erro = "Ficha não pode ser excluída, pois possui dependentes. " }, JsonRequestBehavior.AllowGet);

        //        var titulo = _tituloApp.BuscarTituloPendente(id.Value);

        //        if (titulo != null)
        //            return Json(new { erro = "Ficha não pode ser excluída, pois possui Títulos Pendentes de Pagamento " }, JsonRequestBehavior.AllowGet);


        //        var ficha = _fichaApp.GetById(id.Value);
        //        _fichaApp.Remove(ficha);

        //        return Json(new { success = "Ficha Excluída" }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { erro = ex.Message }, JsonRequestBehavior.AllowGet);
        //    }
        //}
        
        public FileResult ExportarTitulos(int id)
        {
            FichaViewModel fichaViewmodel = new FichaViewModel();

            try
            {
                var ficha = _fichaApp.GetById(id);

                var titulos = _tituloApp.BuscarTitulosPorFicha(ficha.FichaId);

                fichaViewmodel.NomeCompleto = ficha.NomeCompleto;
                fichaViewmodel.CPF = ficha.CPF;
                fichaViewmodel.Titulos = new List<TituloViewModel>();

                foreach (var item in titulos)
                {
                    fichaViewmodel.Titulos.Add(new TituloViewModel
                    {
                        TituloId = item.TituloId,
                        Status = item.Status == "P" ? "Pendente Pagamento" : "Baixado",
                        DataBaixa = item.DataBaixa,
                        Valor = item.Valor,
                        DataCadastro = item.DataCadastro,
                        Mes = item.Mes,
                        Ano = item.Ano
                    });
                }

                var body = RenderPartialViewToString("_tituloCertificado", fichaViewmodel);

                MemoryStream output = new MemoryStream();
                {
                    StreamWriter writer = new StreamWriter(output, Encoding.UTF8);
                    {
                        writer.Write(body);

                        writer.Flush();
                        output.Position = 0;

                        return base.File(output, "text/plain", "titulo_" + DateTime.Now + ".doc");
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Exportar para excel fichas e dependentes
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public FileResult ExportarCSV(int id)
        {
            FichaViewModel fichaViewmodel = new FichaViewModel();

            try
            {
                var ficha = _fichaApp.GetById(id);

                var dependentes = _dependenteApp.BuscarDependentesPorFicha(ficha.FichaId);

                fichaViewmodel.NomeCompleto = ficha.NomeCompleto;
                fichaViewmodel.CPF = ficha.CPF;
                fichaViewmodel.Dependentes = new List<DependenteViewModel>();

                foreach (var item in dependentes)
                {
                    fichaViewmodel.Dependentes.Add(new DependenteViewModel { NomeCompletoDep = item.NomeCompletoDep, CPFDep = item.CPFDep });
                }

                var body = RenderPartialViewToString("_EmailCertificado", fichaViewmodel);


                MemoryStream output = new MemoryStream();
                {
                    StreamWriter writer = new StreamWriter(output, Encoding.UTF8);
                    {
                        writer.Write(body);

                        writer.Flush();
                        output.Position = 0;

                        return base.File(output, "text/plain", "ficha_" + DateTime.Now + ".doc");
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Renderizar partial view
        /// </summary>
        /// <param name="viewName"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        protected string RenderPartialViewToString(string viewName, object model)
        {
            ViewData.Model = model;

            using (StringWriter sw = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                ViewContext viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }

        /// <summary>
        /// Consultar Endereço usando o CEP
        /// </summary>
        /// <param name="cep">Numero do CEP</param>
        /// <returns></returns>
        public JsonResult ConsultarEnderecoPorCEP(string cep)
        {
            try
            {
                System.Text.RegularExpressions.Regex rgx = new System.Text.RegularExpressions.Regex(@"/D");
                cep = rgx.Replace(cep, "");

                CEPManagerViewModel res = CEPManager.obterAPIcorreios(cep);

                return Json(res, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { erro = e.Message }, JsonRequestBehavior.AllowGet);
            }

        }

    }
}
