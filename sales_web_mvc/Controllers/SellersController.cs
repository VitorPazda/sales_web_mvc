using Microsoft.AspNetCore.Mvc;
using sales_web_mvc.Services; // Importar os servicos
using sales_web_mvc.Models;
using sales_web_mvc.Models.ViewModels;
using System.Data;
using sales_web_mvc.Services.Exceptions;
using System.Diagnostics;

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
            // Previnir que as validações sejam feitas
            if (!ModelState.IsValid)
            {
                var departments = _departmentService.FindAll();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id) // ? == opcional
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id nao fornecido" });
            }

            var objeto = _sellerService.FindById(id.Value);
            if (objeto == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id nao encontrado" });
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
                return RedirectToAction(nameof(Error), new { message = "Id nao fornecido" });
            }

            var objeto = _sellerService.FindById(id.Value);
            if (objeto == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id nao encontrado" });
            }

            return View(objeto);
        }

        // Abrir a tela de edicao
        public IActionResult Edit(int? id)
        {
            // Verificar se o id e nulo
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id nao fornecido" });
            }
            var obj = _sellerService.FindById(id.Value);
            if (obj == null) // Verificar se existe no bd
            {
                return RedirectToAction(nameof(Error), new { message = "Id nao encontrado" });
            }

            List<Department> departments = _departmentService.FindAll();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Seller seller)
        {
            // Previnir que as validações sejam feitas
            if (!ModelState.IsValid)
            {
                var departments = _departmentService.FindAll();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }

            if (id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id nao corresponde" });
            }
            try
            {
                _sellerService.Update(seller);
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }
        // Para retornar algum erro
        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }
    }
}
