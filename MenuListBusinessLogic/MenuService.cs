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
            return _menuDataService.GetMenus();
        }

        public Menu GetMenu(string order)
        {
            return _menuDataService.GetMenu(order);
        }
    }
}