using System;
using PL.Helpers;
using PL.Menu.Interfaces;
using BLL.Services.Interfaces;

namespace PL.Menu
{
    public abstract class Submenu<T> : ISubmenu<T> where T : class
    {
        public void Add(IService<T> service, T entity, string successMessage)
        {
            try
            {
                service.Add(entity);
                ConsoleDisplay.Message(successMessage, ConsoleColor.Green);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Remove(IService<T> service, int id, string successMessage)
        {
            try
            {
                service.Remove(id);
                ConsoleDisplay.Message(successMessage, ConsoleColor.Green);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Show(IService<T> service, int id, string failMessage)
        {
            T entity = service.Get(id);
            if (entity == null)
            {
                ConsoleDisplay.Message(failMessage, ConsoleColor.DarkYellow);
            }
            else
            {
                Console.WriteLine($"{entity}{Environment.NewLine}");
            }
        }

        public void Update(IService<T> service, T updatedEntity, int id, string successMessage)
        {
            try
            {
                service.Update(updatedEntity, id);
                ConsoleDisplay.Message(successMessage, ConsoleColor.Green);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
