using Ecom.core.Entities;
using Ecom.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.core.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<bool> AddAsync(CreateProductDto dto);
        Task<bool> UpdateAsync(int id, UpdateProductDto dto);
        Task<bool> DeleteAsyncWithPicture(int id);
    }
}
