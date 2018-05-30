using App.Entity;
using System.Collections.Generic;

namespace WebApplication_Product.Controllers
{
    public interface IRepository
    {
        IEnumerable<ProductFile> GetAll();
        ProductFile Get(int id);
        ProductFile Add(ProductFile item);
        void Remove(int id);
        bool Update(ProductFile item);
        ProductFile PatchUpdate(ProductFile item);
        void Save();
    }
}
