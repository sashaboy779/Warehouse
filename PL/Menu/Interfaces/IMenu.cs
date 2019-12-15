using System.Collections.Generic;

namespace PL.Menu.Interfaces
{
    public interface IMenu
    {
        List<string> MenuOptions { get; set; }
        void SelectOption();
    }
}
