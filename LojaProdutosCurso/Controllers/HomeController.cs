
using Microsoft.AspNetCore.Mvc;

namespace LojaProdutosCurso.Controllers
{
    public class HomeController : Controller
    {   
        public IActionResult Index()
        {
            return View();
        }
       
    }
}
