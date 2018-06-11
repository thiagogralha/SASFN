using System.Web.Mvc;

namespace SASF.MVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {          
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "SASF";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contato para Manutenção";

            return View();
        }
    }
}