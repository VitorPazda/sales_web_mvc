using Microsoft.AspNetCore.Mvc;
using sales_web_mvc.Services; // Importar os servicos

namespace sales_web_mvc.Controllers
{
    public class SellersController : Controller
    {
        public readonly SellerService _sellerService;
        public SellersController(SellerService sellerService)
        {
            _sellerService = sellerService;
        }
        public IActionResult Index()
        {
            var listaVendedores = _sellerService.FindAll(); // pegar uma lista de todos os vendedores
            return View(listaVendedores);
        }
    }
}
