using System;
using Entities;
using PL.Helpers;
using BLL.SortEnums;
using PL.Menu.Interfaces;
using System.Configuration;
using PL.bin.Debug.Resources;
using BLL.Services.Interfaces;
using System.Collections.Generic;

namespace PL.Menu
{
    public class ProductSubmenu : Submenu<Product>, IMenu
    {
        public List<string> MenuOptions { get; set; }

        private IProductService service;
        private bool continueSelect;

        public ProductSubmenu(IProductService service, string confKey)
        {
            this.service = service;
            MenuOptions = ResourceConvert.ToList(ConfigurationManager.AppSettings[confKey]);
        }

        public void SelectOption()
        {
            continueSelect = true;
            do
            {
                ConsoleDisplay.Collection(MenuOptions, Messages.ProductSubmenu);
                int userValue = Int32.Parse(UserValue.Get($"{Messages.SelectItem}: ", 3, x => Int32.TryParse(x, out int y)
                && y > 0 && y <= MenuOptions.Count));

                switch (userValue)
                {
                    case 1:
                        Add(service, CreateProduct(false), Messages.ProductAdded);
                        break;
                    case 2:
                        int removeId = Int32.Parse(UserValue.Get(Messages.ProductIdRemove, 2, x => Int32.TryParse(x, out int y) && y > 0));
                        Remove(service, removeId, Messages.ProductRemoved);
                        break;
                    case 3:
                        AddCategory();
                        break;
                    case 4:
                        RemoveCategory();
                        break;
                    case 5:
                        Product updated = CreateProduct(true);
                        Update(service, updated, updated.Id, Messages.ProductUpdated);
                        break;
                    case 6:
                        int showId = Int32.Parse(UserValue.Get(Messages.ProductIdShow, 2, x => Int32.TryParse(x, out int y) && y > 0));
                        Show(service, showId, Messages.NoProduct);
                        break;
                    case 7:
                        ConsoleDisplay.Collection(service.GetAllOrderBy(ProductSort.Name), Messages.ProductsByName);
                        break;
                    case 8:
                        ConsoleDisplay.Collection(service.GetAllOrderBy(ProductSort.Brand), Messages.ProductsByBrand);
                        break;
                    case 9:
                        ConsoleDisplay.Collection(service.GetAllOrderBy(ProductSort.Cost), Messages.ProductsByCost);
                        break;
                    case 10:
                        continueSelect = false;
                        break;
                }
                if (continueSelect)
                {
                    ConsoleDisplay.Message(Environment.NewLine + Messages.PressToContinue, ConsoleColor.DarkMagenta);
                    Console.ReadKey();
                    Console.Clear();
                }
            } while (continueSelect);
        }

        public void AddCategory()
        {
            try
            {
                int productId = Int32.Parse(UserValue.Get($"{Messages.ProductId}: ", 2, x => Int32.TryParse(x, out int y) && y > 0));
                int productCategoryId = Int32.Parse(UserValue.Get($"{Messages.ProductCategoryId}: ", 2, x => Int32.TryParse(x, out int a)
                    && a > 0));
                service.AddToCategory(productId, productCategoryId);
                ConsoleDisplay.Message(Messages.ProductCategoryAdded, ConsoleColor.Green);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void RemoveCategory()
        {
            try
            {
                int productId = Int32.Parse(UserValue.Get($"{Messages.ProductId}: ", 2, x => Int32.TryParse(x, out int y) && y > 0));
                service.RemoveFromCategory(productId);
                ConsoleDisplay.Message(Messages.ProductCategoryRemoved, ConsoleColor.Green);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private Product CreateProduct(bool addId)
        {
            Product product = new Product();

            if (addId)
            {
                product.Id = Int32.Parse(UserValue.Get($"{Messages.ProductId}: ", 2, x => Int32.TryParse(x, out int y) && y > 0));
            }

            product.Name = UserValue.Get($"{Messages.ProductName}: ", 2, x => x.Length != 0);
            product.Brand = UserValue.Get($"{Messages.ProductBrand}: ", 2, x => x.Length != 0);
            product.Cost = Int32.Parse(UserValue.Get($"{Messages.ProductCost}: ", 2, x => Int32.TryParse(x, out int a)
            && a > 0));
            product.CategoryId = Int32.Parse(UserValue.Get($"{Messages.ProductCategoryId}: ", 2, x => Int32.TryParse(x, out int a)
            && a > 0));
            product.SupplierId = Int32.Parse(UserValue.Get($"{Messages.ProductSupplierId}: ", 2, x => Int32.TryParse(x, out int a)
            && a > 0));
            return product;
        }
    }
}