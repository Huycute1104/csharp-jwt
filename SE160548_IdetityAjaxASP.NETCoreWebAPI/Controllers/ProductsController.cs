using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SE160548_IdetityAjaxASP.NETCoreWebAPI.Repository.Models;
using SE160548_IdetityAjaxASP.NETCoreWebAPI.Repository.UnitOfwork;

namespace SE160548_IdetityAjaxASP.NETCoreWebAPI.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfwork unitOfwork;

        public ProductsController(IUnitOfwork unitOfwork)
        {
            this.unitOfwork = unitOfwork;
        }



        /*[HttpGet]
        public IActionResult GetAllProduct()
        {
            return Ok(unitOfwork.ProductRepo.GetAll());
        }*/
        public enum SortOrder
        {
            Asc,
            Desc
        }
        [HttpGet]
        public IActionResult GetAllProduct(
    [FromQuery] int pageIndex = 1,
    [FromQuery] int pageSize = 10,
    [FromQuery] string? searchProductName = null,
    [FromQuery] decimal? minPrice = null,
    [FromQuery] decimal? maxPrice = null,
    [FromQuery] int? categoryId = null,
    [FromQuery] string sortBy = "price",
    [FromQuery] SortOrder sortOrder = SortOrder.Asc)
        {
            try
            {
                var sortableFields = new List<string> { "name", "price", "category", "unitsinstock" };
                if (!sortableFields.Contains(sortBy.ToLower()))
                {
                    return BadRequest($"Invalid sortBy value. Allowed values are: {string.Join(", ", sortableFields)}");
                }

                var products = unitOfwork.ProductRepo.GetAll(
                    filter: p => (string.IsNullOrEmpty(searchProductName) || p.ProductName.Contains(searchProductName)) &&
                                 (!minPrice.HasValue || p.UnitPrice >= minPrice.Value) &&
                                 (!maxPrice.HasValue || p.UnitPrice <= maxPrice.Value) &&
                                 (!categoryId.HasValue || p.CategoryId == categoryId),
                    orderBy: GetOrderBy(sortBy, sortOrder),
                    pageIndex: pageIndex,
                    pageSize: pageSize
                );

                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private Func<IQueryable<Product>, IOrderedQueryable<Product>> GetOrderBy(string sortBy, SortOrder sortOrder)
        {
            bool descending = sortOrder == SortOrder.Desc;
            return sortBy.ToLower() switch
            {
                "name" => descending ? (Func<IQueryable<Product>, IOrderedQueryable<Product>>)(q => q.OrderByDescending(p => p.ProductName)) : q => q.OrderBy(p => p.ProductName),
                "price" => descending ? (Func<IQueryable<Product>, IOrderedQueryable<Product>>)(q => q.OrderByDescending(p => p.UnitPrice)) : q => q.OrderBy(p => p.UnitPrice),
                "category" => descending ? (Func<IQueryable<Product>, IOrderedQueryable<Product>>)(q => q.OrderByDescending(p => p.CategoryId)) : q => q.OrderBy(p => p.CategoryId),
                "unitsinstock" => descending ? (Func<IQueryable<Product>, IOrderedQueryable<Product>>)(q => q.OrderByDescending(p => p.UnitsInStock)) : q => q.OrderBy(p => p.UnitsInStock),
                _ => descending ? (Func<IQueryable<Product>, IOrderedQueryable<Product>>)(q => q.OrderByDescending(p => p.ProductName)) : q => q.OrderBy(p => p.ProductName) // Default sorting by ProductName
            };
        }



        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            try
            {
                Product product = unitOfwork.ProductRepo.GetById(id);
                if (product == null)
                {
                    return NotFound();
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public IActionResult CreateProduct(Models.CreateProductModel product)
        {
            try
            {
                var productlast = unitOfwork.ProductRepo.GetAll().LastOrDefault();
                Product create = new Product();
                create.ProductId = productlast.ProductId + 1;
                create.ProductName = product.ProductName;
                create.UnitPrice = product.UnitPrice;
                create.CategoryId = product.CategoryId;
                create.Weight = product.Weight;
                create.UnitsInStock = product.UnitsInStock;
                unitOfwork.ProductRepo.Add(create);

                return Ok(create);

            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteProductById(int id)
        {
            try
            {
                Product product = unitOfwork.ProductRepo.GetById(id);
                if (product == null)
                {
                    return NotFound();
                }
                unitOfwork.ProductRepo.Delete(product);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public IActionResult UpdateResult(int id, Models.CreateProductModel model)
        {
            try
            {
                Product product = unitOfwork.ProductRepo.GetById(id);
                if (product == null)
                {
                    return NotFound();
                }
                product.UnitPrice = model.UnitPrice;
                product.CategoryId = model.CategoryId;
                product.Weight = model.Weight;
                product.UnitsInStock = model.UnitsInStock;
                product.ProductName = model.ProductName;
                unitOfwork.ProductRepo.Update(product);
                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
