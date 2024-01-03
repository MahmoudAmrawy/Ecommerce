using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext context;

        public ProductRepository(StoreContext context)
        {
            this.context = context;
        }

        public async Task<IReadOnlyList<Product>> GetProduct()
        {
            return await context.Products
                .Include(p => p.ProductBrand)
                .Include(p => p.ProductType)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrands()
        {
            return await context.ProductBrands.ToListAsync();
        }

        public async Task<Product> GetProductById(int id)
        {
            return await context.Products
                 .Include(p => p.ProductBrand)
                .Include(p => p.ProductType)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<IReadOnlyList<ProductType>> GetProductTypes()
        {
            return await context.ProductTypes.ToListAsync();
        }
    }
}
