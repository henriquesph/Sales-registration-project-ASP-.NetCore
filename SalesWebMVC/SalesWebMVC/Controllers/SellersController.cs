using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Services;

namespace SalesWebMVC.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService; // implementando injeção de dependência

        public SellersController(SellerService sellerService)
        {
            _sellerService = sellerService;
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
            return View();
        }
    }
}
