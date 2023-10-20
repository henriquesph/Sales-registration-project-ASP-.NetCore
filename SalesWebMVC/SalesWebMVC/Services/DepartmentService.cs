using SalesWebMVC.Data;
using SalesWebMVC.Models;
using System.Linq;

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

        public List<Department> FindAll()
        {
            return _context.Department.OrderBy(x => x.Name).ToList();
        }
    }
}
