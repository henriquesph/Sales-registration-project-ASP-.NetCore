using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Services;
using SalesWebMVC.Models;
using SalesWebMVC.Models.ViewModels;
using SalesWebMVC.Services.Exceptions;
using System.Diagnostics;

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

        public async Task<IActionResult> Index()
        {
            var list = await _sellerService.FindAllAsync();
            return View(list); // na view será chamado assim: @model IEnumerable<SalesWebMVC.Models.Seller>
        }

        // Ação: corresponde ao método Get do http (usada no método abaixo, retorna uma view create
        // Ação que altera alguma coisa no sistema usa o Post: inserção, atualização, deleção

        public async Task<IActionResult> Create()
        {
            var departments = await _departmentService.FindAllAsync(); // busca na BD todos os departamentos
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel); // quando for acionada pela primeira vez já receber os objetos de departamento populados 
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // evita ataques xsrf/csrf - quando aproveitam sua sessão para enviar dados maliciosos
        public async Task<IActionResult> Create(Seller seller)
        {
            if(!ModelState.IsValid) // validação a nível de servidor, caso o javascript esteja desabilitado no navegador, as validações do lado do cliente (Form) não vão funcionar e ocasionar erro, a função devolver objeto e recarrega a página até o objeto estar ok, o entityFramework vai reconhecer as validações corretamente. Na aba network do navegador dá para ver se teve alguma requisição. se não teve foi validação via JS
            {
                var departments = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }

            await _sellerService.InsertAsync(seller);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id) // nullable, opcional
        {
            if (id == null)
            {
                //return NotFound
                return RedirectToAction(nameof(Error), new { message = "Id not provided"});
            }

            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                //return NotFound();
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _sellerService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id) // nullable, opcional
        {
            if (id == null)
            {
                //return NotFound();
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                //return NotFound();
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }
            return View(obj);
        }

        // get - retornar a página Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                //return NotFound();
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                //return NotFound();
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            List<Department> departments = await _departmentService.FindAllAsync();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Seller seller)
        {
            if (!ModelState.IsValid)
            {
                var departments = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }
            if (id != seller.Id)
            {
                //return BadRequest();
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" }); // objeto anônimo
            }
            try
            {
                await _sellerService.UpdateAsync(seller);
                return RedirectToAction(nameof(Index));
            }
            // ApplicationExcpetion (supertipo) - Upcasting - poderia substituir pelas 2 de baixo
            catch (NotFoundException e)
            {
                //return NotFound();
                return RedirectToAction(nameof(Error), new { message = e });
            }
            catch (DbConcurrencyExecption e)
            {
                //return BadRequest();
                return RedirectToAction(nameof(Error), new { message = e });
            }
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier // System.Diagnostics
            };
            // Para pegar o Id Interno da requisição
            // ?? - operador de coalescência nula ?? retornará o valor do operando esquerdo se não for null; caso contrário, ele avaliará o operando direito e retornará seu resultado
            // ? - opcional
            return View(viewModel);
        }
    }
}