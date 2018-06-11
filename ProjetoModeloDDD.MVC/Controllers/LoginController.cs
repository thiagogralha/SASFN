using SASF.Application.Interface;
using SASF.MVC.ViewModels;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SASF.MVC.Controllers
{
    public class LoginController : Controller
    {
        private readonly IFuncionarioAppService _funcionarioApp;

        public LoginController(IFuncionarioAppService funcionarioApp)
        {
            _funcionarioApp = funcionarioApp;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Acesso ao painel administrativo
        /// </summary>
        /// <param name="funcionario"></param>
        /// <returns></returns>
        public ActionResult BuscarDetalhesFuncionario(FuncionarioViewModel funcionario)
        {
            try
            {
                if (funcionario.Login == null && funcionario.Senha == null)
                    return Json("Não foi possível logar. Favor inserir usuário e senha.", JsonRequestBehavior.AllowGet);

                var func = _funcionarioApp.BuscarDetalhesFuncionario(funcionario.Login, funcionario.Senha);

                var roles = "Admin";

                if (func != null)
                {
                    funcionario.Mensagem = null;
                    FormsAuthentication.SetAuthCookie(funcionario.Login, false);

                    var authTicket = new FormsAuthenticationTicket(1, funcionario.Login, DateTime.Now, DateTime.Now.AddMinutes(20), false, roles);
                    string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                    var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                    HttpContext.Response.Cookies.Add(authCookie);

                    //HttpCookie cookie = Request.Cookies["IsAdmin"];

                    //if (cookie == null)
                    //{
                    //    cookie = new HttpCookie("IsAdmin");
                    //    cookie.Values.Add("isAdmin", func.IsAdmin);
                    //    cookie.Expires = DateTime.Now.AddDays(1);
                    //    cookie.HttpOnly = true;
                    //    this.Response.AppendCookie(cookie);

                    //}


                    return Json("Success", JsonRequestBehavior.AllowGet);
                }

                else
                {
                    return Json("Não foi possível logar. Verifique seus dados de acesso.", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(ex, JsonRequestBehavior.AllowGet);
            }
            
        }
        
        /// <summary>
        /// Deslogar
        /// </summary>
        /// <returns></returns>
        public ActionResult LogOff()
        {
            //var cookie = new HttpCookie(".ASPXAUTH");
            //cookie.Expires = DateTime.Now.AddDays(-1);
            //System.Web.HttpContext.Current.Response.Cookies.Add(cookie);

            //AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            FormsAuthentication.SignOut();           

            return RedirectToAction("Index", "Home");
        }
    }
}
