using LinkDevTask.Application.ViewModels.Category;

namespace LinkDevTask.Application.Servcices.Interfaces
{
    public interface ICategoryService
    {
        Task<int> AddCategory(string newCategory);
        Task<int> DeleteCategory(string id);
        Task<int> EditCategory(CategoryVM newCategory);
        IEnumerable<CategoryVM> GetAll();
        CategoryVM? GetCategory(string id);
    }
}