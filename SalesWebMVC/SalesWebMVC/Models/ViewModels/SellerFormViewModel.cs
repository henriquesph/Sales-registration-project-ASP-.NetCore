// ViewModel - Model composto, algumas telas precisam de mais dados do que apenas 1 entidade, cria-se todos os dados que precisam navegar na tela


namespace SalesWebMVC.Models.ViewModels
{
    public class SellerFormViewModel
    {
        public Seller Seller { get; set; }
        public ICollection<Department> Departments { get; set; } // para criar uma caixa com select
    }
}