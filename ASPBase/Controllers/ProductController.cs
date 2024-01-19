using ASPBase.Models;
using ASPBase.Services;
using ASPBase.Services.Impl;
using ASPBase.Services.Новая_папка;
using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics;
using System.Globalization;
using System.Net.Http.Headers;

namespace ASPBase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductRepository _productRepository;
        private IMemoryCache _memoryCache;

        public ProductController(IProductRepository productRepository, IMemoryCache cache)
        {
            _productRepository = productRepository;
            _memoryCache = cache;
        }

        [HttpPost("createProduct")]
        public IActionResult Create([FromBody] CreateProductRequest createProductRequest)
        {
            Product client = new Product();
            client.Name = createProductRequest.Name;
            client.Description = createProductRequest.Description;
            client.Price = createProductRequest.Price;
            client.CategoryId = createProductRequest.CategoryId;
            
            
            return Ok(_productRepository.Create(client));

        }
        [HttpPut("editProduct")]
        public IActionResult Update([FromBody] UpdateProductRequest updateProductRequest)
        {
            Product client = new Product();
            client.Id = updateProductRequest.Id;
            client.Name = updateProductRequest.Name;
            client.Description = updateProductRequest.Description;
            client.Price = updateProductRequest.Price;
            client.CategoryId = updateProductRequest.CategoryId;
            return Ok(_productRepository.Update(client));
        }

        [HttpDelete("deleteProduct")]
        public IActionResult Delete([FromQuery] int Id)
        {
            int res = _productRepository.Delete(Id);
            return Ok(res);
        }

        [HttpDelete("delete-AllProduct")]
        public IActionResult DeleteAll()
        {
            int res = _productRepository.DeleteAll();
            return Ok(res);
        }

        [HttpGet("get-allProduct")]
        public IActionResult GetAll()
        {
            return Ok(_productRepository.GetAll());
        }

        [HttpGet("get-oneProduct/{Id}")]
        public IActionResult GetById([FromRoute] int Id)
        {
            return Ok(_productRepository.GetById(Id));
        }

        [HttpGet("get-productCSVFile")]
        public async Task<IActionResult> GetCSVFileGetProduct()
        {

            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            if (_memoryCache.TryGetValue("CSVCacheKey", out byte[] cashCsvBytes))
            {
                stopwatch.Stop();
                Console.WriteLine($"Время выполнение из кеша {stopwatch.ElapsedMilliseconds}МС");
                return File(cashCsvBytes, "text/csv", "CSVproducts.csv");
            }

            stopwatch.Restart();

            IEnumerable<Product> products = _productRepository.GetAll();

            string csvString = _productRepository.GetCsv(products);

            byte[] csvBytes = System.Text.Encoding.UTF8.GetBytes(csvString);

            _memoryCache.Set("CSVCacheKey", csvBytes, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)

            });

            stopwatch.Stop();
            Console.WriteLine($"Время выполнение без кеша {stopwatch.ElapsedMilliseconds}МС");

            return File(csvBytes, "text/csv", "CSVproducts.csv");
        }

    }
}
