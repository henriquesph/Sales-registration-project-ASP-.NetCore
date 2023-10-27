using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SalesWebMVC.Models
{
    public class Seller
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "{0} required")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "{0} size should  be between {2} and {1}")] // 0: nome do atributo - 1: valor máximo - 2: valor mínimo
        public string Name { get; set; }


        [DataType(DataType.EmailAddress)] // gera link p/ abrir o email
        [Required(ErrorMessage = "{0} required")]
        [EmailAddress(ErrorMessage = "Enter a valid email")]
        public string Email { get; set; }


        [Display (Name = "Birth Date")]
        [Required(ErrorMessage = "{0} required")]
        [DataType (DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")] // 0: valor do atributo?
        public DateTime BirthDate { get; set; }


        [Display(Name = "Base Salary")]
        [Range(100.0, 5000.0, ErrorMessage = "{0} must be from {1} to {2}")]
        [DisplayFormat(DataFormatString = "{0:F2}")] // 0: valor deo atributo, F2: 2 casas decimais
        [Required(ErrorMessage = "{0} required")]
        public double BaseSalary { get; set; }


        // já é obrigatório
        public Department? Department { get; set; } // Associação, 1 Selller possui 1 Department - ? coloquei que aceita null (apesar da lógica não ser essa) porque quando usei operações assíncronas, deu erro na hora de criar um novo Seller, dizendo que nao tinha cadastrado um Department, apesar de eu ter escolhido na caixa de seleção
        public int DepartmentId { get; set; } // Entity framework entende que o nome da propriedade + Id é o Department que deverá ser instaciado
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>(); // Associação e instanciação - 1 para muitos

        public Seller()
        {

        }

        public Seller(int id, string name, string email, DateTime birthDate, double baseSalary, Department department)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            BaseSalary = baseSalary;
            Department = department;
        }

        public void AddSales(SalesRecord sr)
        {
            Sales.Add(sr);
        }

        public void RemoveSales(SalesRecord sr)
        {
            Sales.Remove(sr);
        }

        public double TotalSales(DateTime initial, DateTime final)
        {
            return Sales.Where(sr => sr.Date >= initial && sr.Date <= final).Sum(sr => sr.Amount);
        }
    }
}
