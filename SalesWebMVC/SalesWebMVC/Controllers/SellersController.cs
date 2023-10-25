using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Services;
using SalesWebMVC.Models;
using SalesWebMVC.Models.ViewModels;

namespace SalesWebMVC.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService; // implementando injeção de dependência
        private readonly DepartmentService _departmentService;

        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }

        public IActionResult Index()
        {
            var list = _sellerService.FindAll();
            return View(list); // na view será chamado assim: @model IEnumerable<SalesWebMVC.Models.Seller>
        }

        // Ação: corresponde ao método Get do http (usada no método abaixo, retorna uma view create
        // Ação que altera alguma coisa no sistema usa o Post: inserção, atualização, deleção

        public IActionResult Create()
        {
            var departments = _departmentService.FindAll(); // busca na BD todos os departamentos
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel); // quando for acionada pela primeira vez já receber os objetos de departamento populados 
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // evita ataques xsrf/csrf - quando aproveitam sua sessão para enviar dados maliciosos
        public IActionResult Create(Seller seller)
        {
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id) // nullable, opcional
        {
            if (id == null)
            {
                return NotFound();
            }

            var obj = _sellerService.FindById(id.Value);
            if(obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _sellerService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int? id) // nullable, opcional
        {
            if (id == null)
            {
                return NotFound();
            }

            var obj = _sellerService.FindById(id.Value);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
    }
}