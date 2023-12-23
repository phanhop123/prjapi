using BusinessObject.Model;
using DataAccess.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectPRN231API.DTO;

namespace ProjectPRN231API.Controller
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductRepository repository = new ProductRepository();
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProduct()
        {
            try
            {
                return Ok(repository.GetProducts());
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet]
        [Route("brandId")]
        public ActionResult<IEnumerable<Product>> GetProductByBrandId(int id)
        {
            try
            {
                return Ok(repository.GetProductByBrandId(id));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet]
        [Route("productId")]
        public ActionResult<Product> GetProductById(int id)
        {
            try
            {
                return Ok(repository.GetProductById(id));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet]
        [Route("name")]
        public ActionResult<IEnumerable<Product>> GetProductByName(string name)
        {
            try
            {
                return Ok(repository.GetProductByName(name));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet]
        [Route("top3")]
        public ActionResult<IEnumerable<Product>> GetProductTop3()
        {
            try
            {   
                return Ok(repository.GetProductTop3());
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AddProduct(CProduct product)
        {
            try
            {
                var check = repository.CheckProductByName(product.productName);
                if (product != null)
                    return BadRequest();
                var p = new Product
                {
                    pImg = product.pImg,
                    productName = product.productName,
                    productDescription = product.productDescription,
                    brandId = product.brandId,
                    price = product.price,
                    quantity = product.quantity
                };
                repository.AddProduct(p);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public ActionResult UpdateProduct(UProduct product)
        {
            try
            {
                var check = repository.GetProductById(product.productId);
                if(check == null)
                    return NotFound();
                var p = new Product
                {
                    productId = product.productId,
                    pImg = product.pImg,
                    productName = product.productName,
                    productDescription = product.productDescription,
                    brandId = product.brandId,
                    price = product.price,
                    quantity = product.quantity
                };
                repository.UpdateProduct(p);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteProduct (int id)
        {
            try
            {
                var p = repository.GetProductById(id);
                if(p == null)
                    return NotFound();
                repository.DeleteProduct(p);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
