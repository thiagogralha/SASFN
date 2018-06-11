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
    public class TitulosController : Controller
    {
        private readonly ITituloAppService _tituloApp;

        public TitulosController(ITituloAppService tituloApp)
        {
            _tituloApp = tituloApp;
        }

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Buscar todos os títulos daquela ficha.
        /// </summary>
        /// <param name="fichaId">numero da ficha</param>
        /// <returns></returns>
        public string GetTitulosPorFicha(int? fichaId)
        {

            try
            {
                if (fichaId == null)
                    return JsonConvert.SerializeObject(new { erro = "ficha não foi encontrada" });

                var titulos = _tituloApp.BuscarTitulosPorFicha(fichaId.Value);

                if (titulos == null)
                {
                    return JsonConvert.SerializeObject(new { erro = "Não foi possível encontrar o Título Digitado." });
                }

                return JsonConvert.SerializeObject(titulos, new JsonSerializerSettings { Formatting = Formatting.Indented, ReferenceLoopHandling = ReferenceLoopHandling.Ignore });

            }
            catch (Exception e)
            {
                return JsonConvert.SerializeObject(new { erro = e.Message });
            }

        }

        /// <summary>
        /// Buscar Título
        /// </summary>
        /// <param name="tituloId">Número do título</param>
        /// <returns></returns>
        public string GetTitulos(int? tituloId)
        {
            try
            {
                if (tituloId == null)
                    return JsonConvert.SerializeObject(new { erro = "Necessário digitar o código do Título." });

                var titulo = _tituloApp.BuscarTitulo(tituloId.Value);

                if (titulo == null)
                {
                    return JsonConvert.SerializeObject(new { erro = "Não foi possível encontrar o Título Digitado." });
                }

                var resultado = new TituloViewModel();

                resultado.TituloId = titulo.TituloId;
                resultado.Status = titulo.Status;
                resultado.FichaId = titulo.FichaId;
                resultado.Valor = titulo.Valor;
                resultado.Mes = titulo.Mes;
                resultado.FichaNome = titulo.Ficha.NomeCompleto;
                resultado.DataCadastro = titulo.DataCadastro;
                resultado.DataBaixa = titulo.DataBaixa;
                resultado.PlanoNome = titulo.PlanoNome;

                return JsonConvert.SerializeObject(resultado, new JsonSerializerSettings { Formatting = Formatting.Indented, ReferenceLoopHandling = ReferenceLoopHandling.Ignore });

            }
            catch (Exception e)
            {
                return JsonConvert.SerializeObject(new { erro = e.Message });
            }
        }

        /// <summary>
        /// Inserir um novo título
        /// </summary>
        /// <param name="titulo">Titulo</param>
        /// <returns></returns>
        public ActionResult InsertTitulo(TituloViewModel titulo)
        {
            try
            {
                var tituloDomain = Mapper.Map<TituloViewModel, Titulo>(titulo);

                if (titulo.TituloId > 0)
                {
                    if (titulo.Status == "B") //TITULO BAIXADO
                    {
                        tituloDomain.DataBaixa = DateTime.Now;
                    }
                    else
                    {
                        tituloDomain.DataBaixa = null;
                    }


                    _tituloApp.Update(tituloDomain);

                    if (titulo.Mes == 12) //SIGNIFICA QUE CHEGOU NO ULTIMO MÊS
                    {
                        //Adiciona 1 ano de Títulos a nova ficha cadastrada.
                        for (int i = 1; i <= 12; i++)
                        {

                            _tituloApp.Add(new Titulo
                            {
                                FichaId = titulo.FichaId,
                                DataCadastro = DateTime.Now,
                                Status = "P",
                                DataBaixa = null,
                                Valor = titulo.Valor,
                                Mes = i,
                                Ano = DateTime.Now.Year + 1
                            });
                        }
                    }
                }

                return Json(tituloDomain, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { erro = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Excluir um Título
        /// </summary>
        /// <param name="titulo">Numero do título</param>
        /// <returns></returns>
        //public JsonResult Excluir(int? id)
        //{
        //    try
        //    {
        //        var titulo = _tituloApp.GetById(id.Value);
        //        _tituloApp.Remove(titulo);

        //        return Json(new { success = "Titulo Excluído" }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { erro = ex.Message }, JsonRequestBehavior.AllowGet);
        //    }
        //}

    }
}
