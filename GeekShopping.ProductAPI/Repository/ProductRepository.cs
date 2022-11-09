using AutoMapper;
using GeekShopping.ProductAPI.Data.ValueObjects;
using GeekShopping.ProductAPI.Models;
using GeekShopping.ProductAPI.Models.Context;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.ProductAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly MySQLContext _context;
        private IMapper _mapper;

        public ProductRepository(MySQLContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ProductVO>> FindAll()
        {
            var product = await _context.Product.ToListAsync();
            return _mapper.Map<List<ProductVO>>(product);
        }
        public async Task<ProductVO> FindById(long id)
        {
            var product = await _context.Product.FindAsync(id);
            return _mapper.Map<ProductVO>(product);
        }
     
        public async Task<ProductVO> Create(ProductVO productVO)
        {
            var product = _mapper.Map<Product>(productVO);
            await _context.AddAsync(product);
            await _context.SaveChangesAsync(); 
            return _mapper.Map<ProductVO>(product);
        }

        public async Task<ProductVO> Update(ProductVO productVO)
        {
            var product = _mapper.Map<Product>(productVO);
            _context.Update(product);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProductVO>(product);
        }
  
        public async Task<bool> Delete(long id)
        {
            try
            {
                var product = await _context.Product.FindAsync(id);
                if (product == null) return false;
                _context.Remove(product);
                await _context.SaveChangesAsync();
                return true;

            } catch (Exception)
            {
                return false;
            }
        }

    }
}
