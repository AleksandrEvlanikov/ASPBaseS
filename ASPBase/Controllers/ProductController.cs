using ASPBase.Models;
using ASPBase.Services;
using ASPBase.Services.Impl;
using ASPBase.Services.Новая_папка;
using Microsoft.AspNetCore.Mvc;

namespace ASPBase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
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
    }
}
