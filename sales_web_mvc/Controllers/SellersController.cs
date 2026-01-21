using Microsoft.AspNetCore.Mvc;
using sales_web_mvc.Services; // Importar os servicos
using sales_web_mvc.Models;
using sales_web_mvc.Models.ViewModels;

namespace sales_web_mvc.Controllers
{
    public class SellersController : Controller
    {
        public readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;
        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }
        public IActionResult Index()
        {
            var listaVendedores = _sellerService.FindAll(); // pegar uma lista de todos os vendedores
            return View(listaVendedores);
        }

        public IActionResult Create()
        {
            var departments = _departmentService.FindAll(); // Buscar no bd todos os departamentos
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }
        // Inserir no banco o seller
        [HttpPost] // Enviar como post
        [ValidateAntiForgeryToken] // Prevenir ataques CSRF
        public IActionResult Create(Seller seller)
        {
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id) // ? == opcional
        {
            if (id == null)
            {
                return NotFound();
            }

            var objeto = _sellerService.FindById(id.Value);
            if (objeto == null)
            {
                return NotFound();
            }

            return View(objeto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _sellerService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var objeto = _sellerService.FindById(id.Value);
            if (objeto == null)
            {
                return NotFound();
            }

            return View(objeto);
        }
    }
}
