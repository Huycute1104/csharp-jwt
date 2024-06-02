using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SE160548_IdetityAjaxASP.NETCoreWebAPI.Models;
using SE160548_IdetityAjaxASP.NETCoreWebAPI.Repository.Models;
using SE160548_IdetityAjaxASP.NETCoreWebAPI.Repository.UnitOfwork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SE160548_IdetityAjaxASP.NETCoreWebAPI.Controllers
{
    [Route("api/products")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfwork _unitOfwork;
        private readonly IMapper _mapper;

        public ProductsController(IUnitOfwork unitOfwork, IMapper mapper)
        {
            _unitOfwork = unitOfwork;
            _mapper = mapper;
        }

        public enum SortOrder
        {
            Asc,
            Desc
        }

        [HttpGet]
        public IActionResult GetProduct(
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

                var products = _unitOfwork.ProductRepo.Get(
                    filter: p => (string.IsNullOrEmpty(searchProductName) || p.ProductName.Contains(searchProductName)) &&
                                 (!minPrice.HasValue || p.UnitPrice >= minPrice.Value) &&
                                 (!maxPrice.HasValue || p.UnitPrice <= maxPrice.Value) &&
                                 (!categoryId.HasValue || p.CategoryId == categoryId),
                    orderBy: GetOrderBy(sortBy, sortOrder),
                    pageIndex: pageIndex,
                    pageSize: pageSize
                );

                var productMappers = _mapper.Map<IEnumerable<ProductMapper>>(products);

                return Ok(productMappers);
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
                "name" => descending
                    ? (Func<IQueryable<Product>, IOrderedQueryable<Product>>)(q => q.OrderByDescending(p => p.ProductName))
                    : q => q.OrderBy(p => p.ProductName),
                "price" => descending
                    ? (Func<IQueryable<Product>, IOrderedQueryable<Product>>)(q => q.OrderByDescending(p => p.UnitPrice))
                    : q => q.OrderBy(p => p.UnitPrice),
                "category" => descending
                    ? (Func<IQueryable<Product>, IOrderedQueryable<Product>>)(q => q.OrderByDescending(p => p.CategoryId))
                    : q => q.OrderBy(p => p.CategoryId),
                "unitsinstock" => descending
                    ? (Func<IQueryable<Product>, IOrderedQueryable<Product>>)(q => q.OrderByDescending(p => p.UnitsInStock))
                    : q => q.OrderBy(p => p.UnitsInStock),
                _ => descending
                    ? (Func<IQueryable<Product>, IOrderedQueryable<Product>>)(q => q.OrderByDescending(p => p.ProductName))
                    : q => q.OrderBy(p => p.ProductName) // Default sorting by ProductName
            };
        }

        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            try
            {
                Product product = _unitOfwork.ProductRepo.GetById(id);
                if (product == null)
                {
                    return NotFound(new { message = "Product Not Found" });
                }

                var productDto = _mapper.Map<ProductMapper>(product);

                return Ok(productDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateProduct(CreateProductMapper product)
        {
            try
            {
                var productLast = _unitOfwork.ProductRepo.Get().LastOrDefault();
                if (productLast == null)
                {
                    return BadRequest("No products found in repository.");
                }
                var create = _mapper.Map<Product>(product);
                create.ProductId = productLast.ProductId + 1;

                _unitOfwork.ProductRepo.Add(create);

                // Map the created Product back to ProductMapper
                var productDto = _mapper.Map<ProductMapper>(create);

                return Ok(productDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProductById(int id)
        {
            try
            {
                Product product = _unitOfwork.ProductRepo.GetById(id);
                if (product == null)
                {
                    return NotFound(new { message = "Product Not Found" });
                }

                _unitOfwork.ProductRepo.Delete(product);
                return Ok(new { message = "Product deleted successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateResult(int id, CreateProductMapper model)
        {
            try
            {
                Product product = _unitOfwork.ProductRepo.GetById(id);
                if (product == null)
                {
                    return NotFound(new { message = "Product Not Found" });
                }
                product.UnitPrice = model.UnitPrice;
                product.CategoryId = model.CategoryId;
                product.Weight = model.Weight;
                product.UnitsInStock = model.UnitsInStock;
                product.ProductName = model.Name; 

                _unitOfwork.ProductRepo.Update(product);

                CreateProductMapper updatedProduct = _mapper.Map<CreateProductMapper>(product);              
                return Ok(updatedProduct);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
