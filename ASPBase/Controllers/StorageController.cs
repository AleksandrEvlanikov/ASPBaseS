using ASPBase.Models;
using ASPBase.Services.Impl;
using ASPBase.Services.Новая_папка;
using ASPBase.Services;
using Microsoft.AspNetCore.Mvc;

namespace ASPBase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StorageController : ControllerBase
    {
        private IStorageRepository _storageRepository;

        public StorageController(IStorageRepository storageRepository)
        {
            _storageRepository = storageRepository;
        }

        [HttpPost("createStorage")]
        public IActionResult Create([FromBody] CreateStorageRequest createStorageRequest)
        {
            Storage client = new Storage();
            client.Name = createStorageRequest.Name;
            client.Description = createStorageRequest.Description;   
            client.ProductId = createStorageRequest.ProductId;

            return Ok(_storageRepository.Create(client));

        }
        [HttpPut("editStorage")]
        public IActionResult Update([FromBody] UpdateStorageRequest updateStorageRequest)
        {
            Storage client = new Storage();
            client.Id = updateStorageRequest.Id;
            client.Name = updateStorageRequest.Name;
            client.Description = updateStorageRequest.Description;
            client.ProductId = updateStorageRequest.ProductId;
            return Ok(_storageRepository.Update(client));
        }

        [HttpDelete("deleteStorage")]
        public IActionResult Delete([FromQuery] int Id)
        {
            int res = _storageRepository.Delete(Id);
            return Ok(res);
        }

        [HttpDelete("delete-AllStorage")]
        public IActionResult DeleteAll()
        {
            int res = _storageRepository.DeleteAll();
            return Ok(res);
        }

        [HttpGet("get-allStorage")]
        public IActionResult GetAll()
        {
            return Ok(_storageRepository.GetAll());
        }

        [HttpGet("get-oneStorage/{Id}")]
        public IActionResult GetById([FromRoute] int Id)
        {
            return Ok(_storageRepository.GetById(Id));
        }

        [HttpGet("get-storageCSVFile")]
        public async Task<IActionResult> GetCSVFileGetProduct()
        {
            IEnumerable<Storage> storage = _storageRepository.GetAll();

            string csvString = _storageRepository.GetCsv(storage);

            byte[] csvBytes = System.Text.Encoding.UTF8.GetBytes(csvString);

            return File(csvBytes, "text/csv", "CSVproducts.csv");

        }
    }
}
