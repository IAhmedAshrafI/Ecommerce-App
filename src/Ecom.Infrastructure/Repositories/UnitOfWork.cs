using AutoMapper;
using Ecom.core.Interfaces;
using Ecom.Core.Interfaces;
using Ecom.Infrastructure.Data;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace Ecom.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileProvider _fileProvider;
        private readonly IMapper _mapper;
        private readonly IConnectionMultiplexer _redis;

        public IcategoryRepository CategoryRepository { get; }

        public IProductRepository ProductRepository { get; }
     
        public IBasketRepository BasketRepository { get; }

    public UnitOfWork(ApplicationDbContext context, IFileProvider fileProvider, IMapper mapper, IConnectionMultiplexer redis)
        {
            _context = context;
            _fileProvider = fileProvider;
            _mapper = mapper;
            _redis = redis;
      CategoryRepository = new CategoryRepository(_context);
            ProductRepository = new ProductRepository(_context, _fileProvider, _mapper);
            BasketRepository = new BasketRepository(_redis);
    }
    }
}
