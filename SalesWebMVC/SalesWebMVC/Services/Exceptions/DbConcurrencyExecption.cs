namespace SalesWebMVC.Services.Exceptions
{
    public class DbConcurrencyExecption : ApplicationException
    {
        public DbConcurrencyExecption(string message) : base(message)
        {

        }
    }
}
