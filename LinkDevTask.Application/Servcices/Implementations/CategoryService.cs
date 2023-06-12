using LinkDevTask.Application.Helpers;
using LinkDevTask.Domain.Models;
using LinkDevTask.Application.ViewModels.Category;
using LinkDevTask.Domain.DataAccess;
using LinkDevTask.Application.Servcices.Interfaces;
using LinkDevTask.Application.ViewModels.Job;

namespace LinkDevTask.Application.Servcices.Implementations
{
    public class CategoryService : ICategoryService
    {
        #region ctor

        private readonly IUnitOfWork _unitOfWork;
        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion

        public IEnumerable<CategoryVM> GetAll()
        {
            var categories = _unitOfWork._categoryRepo.GetAll(cat => cat.Jobs);
            foreach (var cat in categories)
            {
                var categoryVM = Mapper.MapTo<CategoryVM>(cat);
                categoryVM.JobsInCategoryCount = cat.Jobs.Count;
                yield return categoryVM;
            }
        }

        public CategoryVM? GetCategory(string id)
        {
            var Category = _unitOfWork._categoryRepo.Find(id);
            if (Category is not null)
            {
                return Mapper.MapTo<CategoryVM>(Category);
            }

            return default;
        }

        public async Task<int> AddCategory(string newCategory)
        {
            var Category = _unitOfWork._categoryRepo.GetOne(cat => cat.Name.Equals(newCategory));
            if (Category is null)
            {
                Category = new Category(newCategory);
                _unitOfWork._categoryRepo.Add(Category);
                return await _unitOfWork.SaveAsync();
            }
            return default;
        }

        public async Task<int> EditCategory(CategoryVM newCategory)
        {
            var Category = _unitOfWork._categoryRepo.Find(newCategory.Id!);
            if (Category is not null)
            {
                await Task.Run(() => _unitOfWork._categoryRepo.Update(Mapper.MapTo<Category>(newCategory)));
                return await _unitOfWork.SaveAsync();
            }
            return default;
        }

        public async Task<int> DeleteCategory(string id)
        {

            var Category = _unitOfWork._categoryRepo.Find(id);
            if (Category is not null)
            {
                _unitOfWork._categoryRepo.Delete(Category);
                return await _unitOfWork.SaveAsync();
            }
            return default;
        }
    }
}
