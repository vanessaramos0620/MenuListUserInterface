using System.Collections.Generic;
using MenuListDataLayer;
using MenuListModel;

namespace MenuListBusinessLogic
{
    public class MenuService
    {
        private readonly MenuDataService _menuDataService;

        public MenuService(MenuDataService menuDataService)
        {
            _menuDataService = menuDataService;
        }

        public List<Menu> GetAllMenus()
        {
            return _menuDataService.GetAllMenus();
        }

        public Menu GetMenu(string item)
        {
            return _menuDataService.GetMenu(item);
        }

        public void AddMenu(Menu menu)
        {
            _menuDataService.AddMenu(menu);
        }

        public bool UpdateMenu(string item, Menu updatedMenu)
        {
            return _menuDataService.UpdateMenu(item, updatedMenu);
        }

        public bool DeleteMenu(string item)
        {
            return _menuDataService.DeleteMenu(item);
        }
    }
}
