using ASPBase.Models;
using ASPBase.Services;
using Microsoft.Extensions.Caching.Memory;

namespace ASPBase.GraphQL
{
    public class RequestGraphQL
    {
        private IProductRepository _productRepository;
        private IMemoryCache _memoryCache;

        public RequestGraphQL(IProductRepository productRepository, IMemoryCache memoryCache)
        {
            _productRepository = productRepository;
            _memoryCache = memoryCache;
        }

        public IEnumerable<Product> AllProductGraphQL()
        {
            return _productRepository.GetAll();
        }
    }

}
