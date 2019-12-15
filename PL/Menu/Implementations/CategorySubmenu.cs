using System;
using Entities;
using PL.Helpers;
using PL.Menu.Interfaces;
using System.Configuration;
using PL.bin.Debug.Resources;
using BLL.Services.Interfaces;
using System.Collections.Generic;

namespace PL.Menu
{   
    public class CategorySubmenu : Submenu<Category>, IMenu
    {
        public List<string> MenuOptions { get; set; }

        private ICategoryService service;
        private bool continueSelect;

        public CategorySubmenu(ICategoryService service, string confKey)
        {
            this.service = service;
            MenuOptions = ResourceConvert.ToList(ConfigurationManager.AppSettings[confKey]);
        }

        public void SelectOption()
        {
            continueSelect = true;
            do
            {
                ConsoleDisplay.Collection(MenuOptions, Messages.CategorySubmenu);
                int userValue = Int32.Parse(UserValue.Get($"{Messages.SelectItem}: ", 3, x => Int32.TryParse(x, out int y)
                && y > 0 && y <= MenuOptions.Count));

                switch (userValue)
                {
                    case 1:
                        Add(service, CreateCategory(false), Messages.CategoryAdded);
                        break;
                    case 2:
                        int removeId = Int32.Parse(UserValue.Get(Messages.CategoryIdRemove, 2, x => Int32.TryParse(x, out int y) && y > 0));
                        Remove(service, removeId, Messages.CategoryRemoved);
                        break;
                    case 3:
                        Category updated = CreateCategory(true);
                        Update(service, updated, updated.Id, Messages.CategoryUpdated);
                        break;
                    case 4:
                        int showId = Int32.Parse(UserValue.Get(Messages.CategoryIdShow, 2, x => Int32.TryParse(x, out int y) && y > 0));
                        Show(service, showId, Messages.NoCategory);
                        break;
                    case 5:
                        ShowAll();
                        break;
                    case 6:
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

        public void ShowAll()
        {
            var categories = service.GetAll();
            if (categories.Count == 0)
            {
                ConsoleDisplay.Message(Messages.NoCategories, ConsoleColor.DarkYellow);
            }
            else
            {
                ConsoleDisplay.Collection(categories, $"{Messages.CategoriesAll}:");
            }
        }

        private Category CreateCategory(bool addId)
        {
            Category category = new Category();
            if (addId)
            {
                category.Id = Int32.Parse(UserValue.Get($"{Messages.CategoryId}: ", 2, x => Int32.TryParse(x, out int y) && y > 0));
            }
            category.CategoryName = UserValue.Get($"{Messages.CategoryName}: ", 2, x => x.Length != 0);
            return category;
        }
    }
}