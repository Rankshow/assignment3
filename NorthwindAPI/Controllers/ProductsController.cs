using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NorthwindAPI.Constants;
using NorthwindAPI.Dtos.ResponseDto;
using NorthwindAPI.Models;
using NorthwindAPI.NorthwindAppDbContext;

namespace NorthwindAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly NorthwindContext _context;

        public ProductsController(NorthwindContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet("ByCategory/{id}")]
        public async Task<IActionResult> GetProducts(int id)
        {
            var response = new ApiResponse<IEnumerable<Product>>();
            try
            {
                if (_context.Products == null)
                {
                    return NotFound();
                }
                var products = await _context.Products.Where(x => x.CategoryId == id).ToListAsync();
                if (products == null)
                {
                    return NotFound();
                }

                response.Data = products;
                response.IsSuccess = true;
                response.Message = ResponseMessageConstant.SuccessMessage;
            }
            catch (Exception ex)
            {

                response.Message = ex.Message;
            }
            return Ok(response);
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
          if (_context.Products == null)
          {
              return NotFound();
          }
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }
    }
}
