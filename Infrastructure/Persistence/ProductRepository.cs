using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _dbcontext;
        public ProductRepository(AppDbContext context)
        {
            _dbcontext = context;
        }

        public async Task<Product?> GetByIdAsync(Guid id)
        {
            var product = await _dbcontext.Products.FindAsync(id);
            return product;
        }


        public async Task<IEnumerable<Product>> ListAllAsync()
        {
            return await _dbcontext.Products.ToListAsync();
        }

        public async Task AddAsync(Product product)
        {
            await _dbcontext.Products.AddAsync(product);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            _dbcontext.Products.Update(product);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var product = await _dbcontext.Products.FindAsync(id);
            _dbcontext.Products.Remove(product!);
            await _dbcontext.SaveChangesAsync();
        }

    }
}
