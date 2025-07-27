using Models;
using Repository.Interfaces;

namespace Repository
{
    public class InMemoryCategoryRepository : ICategoryRepository
    {
        private readonly List<Category> categories = new();
        private int nextId = 1;

        public List<Category> GetAll() => categories;

        public Category? GetById(int id) => categories.FirstOrDefault(c => c.CategoryId == id);

        public void Add(Category category)
        {
            category.CategoryId = nextId++;
            categories.Add(category);
        }

        public void Update(Category category)
        {
            var existing = GetById(category.CategoryId);
            if (existing != null)
            {
                existing.Name = category.Name;
            }
        }

        public void Delete(int id)
        {
            var category = GetById(id);
            if (category != null)
                categories.Remove(category);
        }
    }
}
