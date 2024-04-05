using AutoMapper;
using Ecom.core.Entities;
using Ecom.core.Interfaces;
using Ecom.core.Sharing;
using Ecom.Core.Dtos;
using Ecom.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileProvider _fileProvider;
        private readonly IMapper _mapper;

        public ProductRepository(ApplicationDbContext context, IFileProvider fileProvider, IMapper mapper) : base(context)
        {
            _context = context;
            _fileProvider = fileProvider;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ProductDto>> GetAllAsync(ProductParams productParams)
        {
            var query = await _context.Products
                .Include(x => x.Category)
                .AsNoTracking()
                .ToListAsync();

            if (!string.IsNullOrEmpty(productParams.Search))
                query = query.Where(x => x.Name.ToLower().Contains(productParams.Search)).ToList();

            if (productParams.CategoryId.HasValue)
            {
                query = query.Where(x => x.CategoryId == productParams.CategoryId.Value).ToList();
            }

            if (!string.IsNullOrEmpty(productParams.Sort))
            {
                query = productParams.Sort switch
                {
                    "PirceAsync" => query.OrderBy(x => x.Price).ToList(),
                    "PriceDesc" => query.OrderByDescending(x => x.Price).ToList(),
                    _ => query.OrderBy(x => x.Name).ToList(),
                };
            }
            query = query.Skip((productParams.PageNumber - 1) * productParams.PageSize).Take(productParams.PageSize).ToList();
            var res = _mapper.Map<IEnumerable<ProductDto>>(query);
            return res;
        }
        public async Task<bool> AddAsync(CreateProductDto dto)
        {
            if (dto.Image is not null)
            {
                var root = "/images/products";
                var productName = $"{Guid.NewGuid()}"+ dto.Image.FileName;
                if (!Directory.Exists("wwwroot" + root))
                {
                    Directory.CreateDirectory("wwwroot" + root);
                }
                var src = root + productName;
                var picInfo = _fileProvider.GetFileInfo(src);
                var rootPath = picInfo.PhysicalPath;
                using (var fileStream = new FileStream(rootPath,FileMode.Create))
                {
                    await dto.Image.CopyToAsync(fileStream);
                }

                var product = _mapper.Map<Product>(dto);
                product.ProductPicture = src;
                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateAsync(int id, UpdateProductDto dto)
        {
            var CurretProduct = await _context.Products.FindAsync(id);
            if (CurretProduct is not null)
            {
                var src = "";
                if (dto.Image is not null)
                {
                    var root = "/images/products";
                    var productName = $"{Guid.NewGuid()}" + dto.Image.FileName;
                    if (!Directory.Exists("wwwroot" + root))
                    {
                        Directory.CreateDirectory("wwwroot" + root);
                    }
                    src = root + productName;
                    var picInfo = _fileProvider.GetFileInfo(src);
                    var rootPath = picInfo.PhysicalPath;
                    using (var fileStream = new FileStream(rootPath, FileMode.Create))
                    {
                        await dto.Image.CopyToAsync(fileStream);
                    }
                }
                if (!string.IsNullOrEmpty(CurretProduct.ProductPicture))
                {
                    var picInfo = _fileProvider.GetFileInfo(CurretProduct.ProductPicture);
                    var rootPath = picInfo.PhysicalPath;
                    File.Delete(rootPath);
                }

                var res = _mapper.Map<Product>(dto);
                res.ProductPicture = src;
                _context.Products.Update(res);
                await _context.SaveChangesAsync();
                return true;
            }    
            return false;
            
        }

        public async Task<bool> DeleteAsyncWithPicture(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product is not null)
            {
                if (!string.IsNullOrEmpty(product.ProductPicture))
                {
                    var picInfo = _fileProvider.GetFileInfo(product.ProductPicture);
                    var rootPath = picInfo.PhysicalPath;
                    File.Delete(rootPath);
                }
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
            
    }
}
