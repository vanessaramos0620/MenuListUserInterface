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

        [HttpGet("{item}")]
        public ActionResult<Menu> GetMenu(string item)
        {
            var menu = _menuService.GetMenu(item);
            if (menu == null)
            {
                return NotFound();
            }
            return menu;
        }

        [HttpPost]
        public IActionResult PostMenu(Menu menu)
        {
            _menuService.AddMenu(menu);
            return CreatedAtAction(nameof(GetMenu), new { item = menu.Item }, menu);
        }

        [HttpPatch("{item}")]
        public IActionResult UpdateMenu(string item, Menu updatedMenu)
        {
            if (_menuService.UpdateMenu(item, updatedMenu))
            {
                return NoContent();
            }
            return NotFound();
        }

        [HttpDelete("{item}")]
        public IActionResult DeleteMenu(string item)
        {
            if (_menuService.DeleteMenu(item))
            {
                return NoContent();
            }
            return NotFound();
        }
    }
}
