using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IGenericRepository<Product> repo) : ControllerBase
    {
        //to get a list of all products
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts(string? brand, string? type, string? sort)
        {
            var spec = new ProductSpecification(brand, type, sort);
            var products = await repo.ListASync(spec);
            
            return Ok(products);
        }

        //to get product by id
        [HttpGet("{id:int}")] //api/Products/2
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await repo.GetByIdAsync(id);
            if (product == null) return NotFound(); 
            return product;
        }

        // To add a new product
        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            repo.Add(product);
            //to check and return if product is created properly
            if (await repo.SaveAllAsync())
            {
                return CreatedAtAction("GetProduct", new { id = product.Id }, product);
            }

            return BadRequest("Problem Creatign Product");
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateProduct(int id, Product product)
        {
            if (product.Id != id || !ProductExists(id))
            {
                return BadRequest("Can not update this product");
            }
            else
            {
                repo.Update(product);
                if (await repo.SaveAllAsync())
                {
                    return NoContent();
                }
            }
            return BadRequest("Problem Updating the Product");
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await repo.GetByIdAsync(id);

            if (product == null) return NotFound();

            // context.Products.Remove(product);
            repo.Remove(product);
            if (await repo.SaveAllAsync())
            {
                return NoContent();
            }
            return BadRequest("Problem deleting the Product");
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
        {
            var spec= new BrandListSpecification();
            return Ok(await repo.ListASync(spec));
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
        {
            var spec= new TypeListSpecification();
            return Ok(await repo.ListASync(spec));
        }

        private bool ProductExists(int id)
        {
            return repo.Exists(id);
        }
    }
}
