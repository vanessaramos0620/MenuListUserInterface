using Microsoft.AspNetCore.Mvc;
using MenuListBusinessLogic;
using MenuListModel;
using System.Collections.Generic;

namespace MenuListApi.Controllers
{
    [ApiController]
    [Route("api/menu")]
    public class MenuController : ControllerBase
    {
        private readonly MenuService _menuService;

        public MenuController(MenuService menuService)
        {
            _menuService = menuService;
        }

        [HttpGet]
        public IEnumerable<Menu> GetAllMenus()
        {
            return _menuService.GetAllMenus();
        }

        [HttpGet("{order}")]
        public ActionResult<Menu> GetMenu(string order)
        {
            var menu = _menuService.GetMenu(order);
            if (menu == null)
            {
                return NotFound();
            }
            return menu;
        }

        [HttpPost]
        public IActionResult PostMenu(Menu menu)
        {
         
            return Ok();
        }

        [HttpPatch("{order}")]
        public IActionResult UpdateMenu(string order, Menu updatedMenu)
        {
          
            return Ok();
        }

        [HttpDelete("{order}")]
        public IActionResult DeleteMenu(string order)
        {
         
            return Ok();
        }
    }
}