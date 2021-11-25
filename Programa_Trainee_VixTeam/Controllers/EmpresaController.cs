using Microsoft.AspNetCore.Mvc;
using Programa_Trainee_VixTeam.Models;



namespace Programa_Trainee_VixTeam.Controllers
{
    public class EmpresaController : Controller 
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create([Bind("Nome, Codigo, NomeFantasia, cpnj")] EmpresaModel empresa)
        {

            return View("../Home/Index");
            
            
            
        }
    }
}
