using System;
using PL.Helpers;
using PL.Menu.Interfaces;
using System.Configuration;
using PL.bin.Debug.Resources;
using BLL.Services.Interfaces;
using System.Collections.Generic;

namespace PL.Menu
{
    public class SearchSubmenu : IMenu
    {
        public List<string> MenuOptions { get; set; }

        private ISearch search;
        private bool continueSelect;

        public SearchSubmenu(ISearch search, string confKey)
        {
            this.search = search;
            MenuOptions = ResourceConvert.ToList(ConfigurationManager.AppSettings[confKey]);
        }

        public void SelectOption()
        {
            continueSelect = true;
            do
            {
                ConsoleDisplay.Collection(MenuOptions, Messages.SearchSubmenu);
                int userValue = Int32.Parse(UserValue.Get($"{Messages.SelectItem}: ", 3, x => Int32.TryParse(x, out int y)
                && y > 0 && y <= MenuOptions.Count));

                switch (userValue)
                {
                    case 1:
                        string productKeyword = UserValue.Get(Messages.ProductKeyword, 2, x => x.Length > 0);
                        ConsoleDisplay.Collection(search.SearchProduct(productKeyword), Messages.SearchResult);
                        break;
                    case 2:
                        string supplierKeyword = UserValue.Get(Messages.SupplierKeyword, 2, x => x.Length > 0);
                        ConsoleDisplay.Collection(search.SearchSupplier(supplierKeyword), Messages.SearchResult);
                        break;
                    case 3:
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
    }
}