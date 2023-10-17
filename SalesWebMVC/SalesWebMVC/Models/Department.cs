using System.Linq;

namespace SalesWebMVC.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Seller> Sellers { get; set; } = new List<Seller>();
        
        /* - Associação com o Seller
           - ICollection tipo genérico - aceita List, HashSet, etc 
           - Instanciação garantida 
           - 1 Department possui vários Sellers
        */

        public Department()
        {
        }

        // ICollection é implementada no método
        public Department(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public void AddSeller(Seller seller)
        {
            Sellers.Add(seller);
        }

        public double TotalSellers(DateTime initial, DateTime final)
        {
            return Sellers.Sum(seller => seller.TotalSales(initial, final));
        }
    }
}