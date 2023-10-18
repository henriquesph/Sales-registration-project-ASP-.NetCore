// Controller - recebe as requisiçoes e coordena as ações tomadas

// Services - implementa a regra de negócio e acessa os dados - restrições
// Ex: Seller (model) - SellerService

using SalesWebMVC.Data;
using SalesWebMVC.Models;

namespace SalesWebMVC.Services
{
    public class SellerService
    {
        // injeção de dependência
        private readonly SalesWebMVCContext _context; // readonly - para que não possa ser alterada

        public SellerService(SalesWebMVCContext context)
        {
            _context = context;
        }

         public List<Seller> FindAll()
        {
            return _context.Seller.ToList(); // acessa os dados da tabela seller e converte para uma lista - operação síncrona, roda operação de acesso ao BD e aplicação fica bloqueada esperando ela terminar, pouca performance
        }

        public void Insert(Seller obj)
        {
            obj.Department = _context.Department.First();
            _context.Add(obj);
            _context.SaveChanges();
        }
    }
}