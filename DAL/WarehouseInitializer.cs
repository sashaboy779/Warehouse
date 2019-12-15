using Entities;
using System.Data.Entity;
using System.Collections.Generic;

namespace DAL
{
    public class WarehouseInitializer : CreateDatabaseIfNotExists<WarehouseContext>
    {
        protected override void Seed(WarehouseContext context)
        {
            Category alcohol = new Category { CategoryName = "Alcoholic beverages" };
            Category food = new Category { CategoryName = "Food products" };
            Category drinks = new Category { CategoryName = "Drinks and water" };

            Supplier sam = new Supplier { FirstName = "Sam", LastName = "Winchester" };
            Supplier will = new Supplier { FirstName = "Will", LastName = "Graham" };

            var products = new List<Product>
            {
                new Product {Name = "Coffe", Brand = "Jascob", Category = drinks, Supplier = sam, Cost = 40},
                new Product {Name = "Tea", Brand = "Greenfield", Category = drinks, Supplier = sam, Cost = 30},
                new Product {Name = "Honey", Brand = "Helth Food", Category = food, Supplier = will, Cost = 150},
                new Product {Name = "Meat", Brand = "TastyMeal", Category = food, Supplier = will, Cost = 200},
                new Product {Name = "Vine", Brand = "Mikaso", Category = alcohol, Supplier = sam, Cost = 80}
            };

            context.Products.AddRange(products);
            base.Seed(context);
        }
    }
}
