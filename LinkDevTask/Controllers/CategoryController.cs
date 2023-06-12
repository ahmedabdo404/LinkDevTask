using LinkDevTask.Application.Servcices.Implementations;
using LinkDevTask.Application.Servcices.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LinkDevTask.WebApp.Controllers
{
    public class CategoryController : BaseController
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IActionResult GetAll()
        {
            var categories = _categoryService.GetAll();
            return View(categories);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(string Name)
        {
            if (ModelState.IsValid)
            {
                var result = await _categoryService.AddCategory(Name);
                if (!result.Equals(default))
                {
                    return RedirectToAction(nameof(GetAll));
                }
            }
            var AllRoles = _categoryService.GetAll();
            return View(nameof(GetAll), AllRoles);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _categoryService.DeleteCategory(id);
            if (!result.Equals(default))
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
