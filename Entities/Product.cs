using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public int Cost { get; set; }

        public int? CategoryId { get; set; }
        public virtual Category Category { get; set; }


        public int? SupplierId { get; set; }
        public virtual Supplier Supplier { get; set; }

        public override string ToString()
        {
            return $"ID: {Id,-5} Product's name: {Name,-25} Brand: {Brand,-25} Cost: {Cost,-10} " +
                $"Category: {Category?.CategoryName,-25} Supplier: {Supplier?.FirstName + Supplier?.LastName,-25} Supplier ID: {SupplierId}";
        }
    }
}
