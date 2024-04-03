using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Northwind.Dtos;
using NorthwindAPI.Constants;
using NorthwindAPI.Dtos.ResponseDto;
using NorthwindAPI.Models;
using NorthwindAPI.NorthwindAppDbContext;

namespace NorthwindAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly NorthwindContext _context;

        public CategoriesController(NorthwindContext context)
        {
            _context = context;
        }

        // GET: api/Categories
        [HttpGet("GetCategories")]
        public async Task<IActionResult> GetCategories()
        {
            var response = new ApiResponse<List<CategoryDto>>();
            try
            {
                if (_context.Categories == null)
                {
                    return NotFound();
                }
                var categories = await _context.Categories
                    .Include(x => x.Products)
                    .ToListAsync();

                //

                response.Data = categories.Select(x => new CategoryDto
                {
                    CategoryId = x.CategoryId,
                    CategoryName = x.CategoryName,
                    Description = x.Description,
                    Products = x.Products.Select(p => new ProductDto
                    {
                        ProductId = p.ProductId,
                        ProductName = p.ProductName,
                        SupplierId = p.SupplierId,
                        CategoryId = p.CategoryId,
                        QuantityPerUnit = p.QuantityPerUnit,
                        UnitPrice = p.UnitPrice,
                        UnitsInStock = p.UnitsInStock,
                        UnitsOnOrder = p.UnitsOnOrder,
                        ReorderLevel = p.ReorderLevel,
                        Discontinued = p.Discontinued
                    }).ToList()
                }).ToList();
                response.Message = ResponseMessageConstant.SuccessMessage;
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {

                response.Message = ex.Message;
                response.IsSuccess = false;
            }

            return Ok(response);
        }
    }
}
