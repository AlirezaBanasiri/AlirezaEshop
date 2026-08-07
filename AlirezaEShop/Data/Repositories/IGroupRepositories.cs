using AlirezaEShop.Models;

namespace AlirezaEShop.Data.Repositories
{
    public interface IGroupRepositories
    {
        IEnumerable<Category> GetAllCategories();
        IEnumerable<ShowGroupViewModel> GetGroupForShow();
        
    }

    public class GroupRepository : IGroupRepositories
    {
        public AlirezaEShopContext _context;

        public GroupRepository(AlirezaEShopContext context)
        {
            _context = context;
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _context.categories;
        }

        public IEnumerable<ShowGroupViewModel> GetGroupForShow()
        {
            return _context.categories
                .Select(c => new ShowGroupViewModel()
                {
                    GroupID = c.ID,
                    Name = c.Name,
                    ProductCount = _context.CategoryToProducts.Count(g => g.CategoryID == c.ID)
                }).ToList();
        }
    }
}
