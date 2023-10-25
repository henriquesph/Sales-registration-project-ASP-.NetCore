// Controller - recebe as requisiçoes e coordena as ações tomadas

// Services - implementa a regra de negócio e acessa os dados - restrições
// Ex: Seller (model) - SellerService

using SalesWebMVC.Data;
using SalesWebMVC.Models;
using Microsoft.EntityFrameworkCore;
using SalesWebMVC.Services.Exceptions;

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
            /*obj.Department = _context.Department.First();*/ // paleativo - antes de criar o método para escolher o Department
            _context.Add(obj);
            _context.SaveChanges();
        }

        public Seller FindById(int id)
        {
            return _context.Seller.Include(obj => obj.Department).FirstOrDefault(obj => obj.Id == id);

            // Include - não é do linq nativo, é da biblioteca Microsoft.EntityFrameworkCore, ele dá Join nas tabelas
            // Eager loading  - carregar objetos relacionados
        }

        public void Remove(int id)
        {
            var obj = _context.Seller.Find(id);
            _context.Seller.Remove(obj); // remove do dbSet
            _context.SaveChanges();
        }

        public void Update(Seller obj)
        {
            if (!_context.Seller.Any(x => x.Id == obj.Id))
            {
                throw new NotFoundException("Id not found");
            }
            //quando atauliza o Bd pode retornar uma exceção de conflito de  (DBConcurrencyUpdateException)
            try
            {
                _context.Update(obj);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e) // exceção do entity framework
            {
                throw new DbConcurrencyExecption(e.Message); // exceção a nivel de serviço - o "e" vem do BD // Arquitetura proposta, controller conversa com a camada de serviço
            }
        }
    }
}