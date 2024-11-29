using Microsoft.AspNetCore.Mvc;

namespace Desapego.Controllers
{
    public class ProdutosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult PageProdutos()
        {
            return View();
        }
    }
}
