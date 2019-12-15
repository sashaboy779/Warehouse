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
    public class SupplierSubmenu : Submenu<Supplier>, IMenu
    {
        public List<string> MenuOptions { get; set; }

        private ISupplierService service;
        private bool continueSelect;

        public SupplierSubmenu(ISupplierService service, string confKey)
        {
            this.service = service;
            MenuOptions = ResourceConvert.ToList(ConfigurationManager.AppSettings[confKey]);
        }

        public void SelectOption()
        {
            continueSelect = true;
            do
            {
                ConsoleDisplay.Collection(MenuOptions, Messages.SupplierSubmenu);
                int userValue = Int32.Parse(UserValue.Get($"{Messages.SelectItem}: ", 3, x => Int32.TryParse(x, out int y)
                && y > 0 && y <= MenuOptions.Count));

                switch (userValue)
                {
                    case 1:
                        Add(service, CreateSupplier(false), Messages.SupplierAdded);
                        break;
                    case 2:
                        int removeId = Int32.Parse(UserValue.Get(Messages.SupplierIdRemove, 2, x => Int32.TryParse(x, out int y) && y > 0));
                        Remove(service, removeId, Messages.SupplierRemoved);
                        break;
                    case 3:
                        Supplier updated = CreateSupplier(true);
                        Update(service, updated, updated.Id, Messages.SupplierUpdated);
                        break;
                    case 4:
                        int showId = Int32.Parse(UserValue.Get(Messages.SuppliertIdShow, 2, x => Int32.TryParse(x, out int y) && y > 0));
                        Show(service, showId, Messages.NoSupplier);
                        break;
                    case 5:
                        ConsoleDisplay.Collection(service.GetAllOrderBy(SupplierSort.FirstName), Messages.SupplierByFirstname);
                        break;
                    case 6:
                        ConsoleDisplay.Collection(service.GetAllOrderBy(SupplierSort.LastName), Messages.SupplierByLastname);
                        break;
                    case 7:
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

        private Supplier CreateSupplier(bool addId)
        {
            Supplier supplier = new Supplier();
            if (addId)
            {
                supplier.Id = Int32.Parse(UserValue.Get($"{Messages.SupplierId}: ", 2, x => Int32.TryParse(x, out int y) && y > 0));
            }
            supplier.FirstName = UserValue.Get($"{Messages.SupplierFirstname}: ", 2, x => x.Length != 0);
            supplier.LastName = UserValue.Get($"{Messages.SupplierLastname}: ", 2, x => x.Length != 0);
            return supplier;
        }
    }
}