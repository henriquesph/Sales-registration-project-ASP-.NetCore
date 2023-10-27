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


        //public List<Seller> FindAll() // acessa os dados da tabela seller e converte para uma lista - operação síncrona, roda operação de acesso ao BD e aplicação fica bloqueada esperando ela terminar, pouca performance
        //{
        //    return _context.Seller.ToList();
        //}


        public async Task<List<Seller>> FindAllAsync()  // chamada assíncrona
        {
            return await _context.Seller.ToListAsync(); 
        }

        public async Task InsertAsync(Seller obj) // na chamada síncrona tinha o void
        {
            /*obj.Department = _context.Department.First();*/ // paleativo - antes de criar o método para escolher o Department
            _context.Add(obj); // feita em memória, não precisa do async
            await _context.SaveChangesAsync(); // acessa o BD, precisa do async
        }

        public async Task<Seller> FindByIdAsync(int id)
        {
            return await _context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(obj => obj.Id == id);

            // Include - não é do linq nativo, é da biblioteca Microsoft.EntityFrameworkCore, ele dá Join nas tabelas
            // Eager loading  - carregar objetos relacionados
            // na versão síncrona era FirstOrDefault
        }

        public async Task RemoveAsync(int id) // na função síncrona tinha o void
        {
            var obj = await _context.Seller.FindAsync(id);
            _context.Seller.Remove(obj); // remove do dbSet
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Seller obj)
        {
            //if (!_context.Seller.Any(x => x.Id == obj.Id)) // abaixo reescrevi para assíncrono

            bool hasAny = await _context.Seller.AnyAsync(x => x.Id == obj.Id);
            if(!hasAny)
            {
                throw new NotFoundException("Id not found");
            }
            //quando atauliza o Bd pode retornar uma exceção de conflito de  (DBConcurrencyUpdateException)
            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e) // exceção do entity framework
            {
                throw new DbConcurrencyExecption(e.Message); // exceção a nivel de serviço - o "e" vem do BD // Arquitetura proposta, controller conversa com a camada de serviço
            }
        }
    }
}