using SalesWebMVC.Data;
using SalesWebMVC.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore; // ToListAsync

namespace SalesWebMVC.Services
{
    public class DepartmentService
    {
        // injeção de dependência
        private readonly SalesWebMVCContext _context; // readonly - para que não possa ser alterada
        // sempre registrar no sistema de injeção de pependencia (program.cs - AddScoped)
        public DepartmentService(SalesWebMVCContext context)
        {
            _context = context;
        }


        // operações assíncronas: não param a aplicação quando são executadas, rodam em paralelo
        // chamadas ao BD são lentas, dessa forma melhora a perfomance

        //public List<Department> FindAll()  // Sincrona
        //{
        //    return _context.Department.OrderBy(x => x.Name).ToList();
        //}

        public async Task<List<Department>> FindAllAsync()  // Assíncrona
        {
            return await _context.Department.OrderBy(x => x.Name).ToListAsync(); // O linq não executa, apenas prepara, quem faz a chamada é a operação seguinte, no caso ToListAsync, o Await informa ao compilador que é uma chamada assíncrona
        }
    }
}