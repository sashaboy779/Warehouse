using System;
using PL.Helpers;
using PL.Menu.Interfaces;
using System.Configuration;
using PL.bin.Debug.Resources;
using System.Collections.Generic;
using BLL.Services.Implementations;

namespace PL.Menu
{
    public class MainMenu : IMenu
    {
        public List<string> MenuOptions { get; set; }

        private CategorySubmenu category;
        private ProductSubmenu product;
        private SupplierSubmenu supplier;
        private SearchMenu search;
        private bool continueSelect;

        public MainMenu()
        {
            continueSelect = true;
            MenuOptions = ResourceConvert.ToList(ConfigurationManager.AppSettings["MainMenu"]);
            category = new CategorySubmenu(new CategoryService(), "CategorySubmenu");
            product = new ProductSubmenu(new ProductService(), "ProductSubmenu");
            supplier = new SupplierSubmenu(new SupplierService(), "SupplierSubmenu");
            search = new SearchMenu(new Search(), "SearchSubmenu");
        }

        public void SelectOption()
        {
            try
            {
                do
                {
                    Console.Clear();
                    ConsoleDisplay.Collection(MenuOptions, Messages.MainMenu);
                    int userValue = Int32.Parse(UserValue.Get($"{Messages.SelectItem}: ", 3, x => Int32.TryParse(x, out int y)
                    && y > 0 && y <= MenuOptions.Count));

                    IMenu menu = new MainMenu();
                    Console.Clear();

                    switch (userValue)
                    {
                        case 1:
                            menu = category;
                            break;
                        case 2:
                            menu = product;
                            break;
                        case 3:
                            menu = supplier;
                            break;
                        case 4:
                            menu = search;
                            break;
                        case 5:
                            continueSelect = false;
                            return;
                    }
                    menu.SelectOption();
                } while (continueSelect);
            }
            catch (Exception e)
            {
                ConsoleDisplay.Message(e.Message, ConsoleColor.DarkRed);
                Console.ReadKey();
            }
            finally
            {
                if (continueSelect)
                {
                    SelectOption();
                }
            }
        }
    }
}
