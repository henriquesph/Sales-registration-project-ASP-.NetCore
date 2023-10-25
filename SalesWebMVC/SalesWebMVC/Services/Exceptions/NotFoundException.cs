// Exceções personalizadas, melhor controle

namespace SalesWebMVC.Services.Exceptions
{
    public class NotFoundException : ApplicationException // herança
    {
        // construtor básico
        public NotFoundException(string message) : base(message) // passa a chamada para a classe base
        
        {

        } 
    }
}
