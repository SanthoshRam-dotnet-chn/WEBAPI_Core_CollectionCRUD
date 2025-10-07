using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WEBAPI_Core_CollectionCRUD.Models;
using WEBAPI_Core_CollectionCRUD.Repository;

namespace WEBAPI_Core_CollectionCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repo;

        public ProductsController(IProductRepository repo)
        {
            _repo = repo;
        }

        // GET: api/products
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_repo.GetAll());
        }

        // GET: api/products/2
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var product = _repo.GetById(id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }

        // POST: api/products
        [HttpPost]
        public IActionResult Create(Product product)
        {
            _repo.Add(product);
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

        // PUT: api/products/2
        [HttpPut("{id}")]
        public IActionResult Update(int id, Product product)
        {
            if (id != product.Id)
                return BadRequest("Id mismatch");

            if (!_repo.Exists(id))
                return NotFound();

            _repo.Update(product);
            return NoContent();
        }

        // PATCH: api/products/2
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] Product partial)
        {
            if (!_repo.Exists(id))
                return NotFound();

            _repo.Patch(id, partial.Name, partial.Price);
            return Ok(_repo.GetById(id));
        }

        // DELETE: api/products/2
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!_repo.Exists(id))
                return NotFound();

            _repo.Delete(id);
            return NoContent();
        }

        // HEAD: api/products
        [HttpHead]
        public IActionResult Head()
        {
            var count = _repo.GetAll().Count();
            Response.Headers.Add("Total-Product-Count", count.ToString());
            return Ok();
        }

        // OPTIONS: api/products
        [HttpOptions]
        public IActionResult Options()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, PATCH, DELETE, HEAD, OPTIONS");
            return Ok();
        }
    }
}
