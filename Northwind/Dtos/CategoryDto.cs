using Northwind.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Northwind.Dtos;

public class CategoryDto
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public string? Description { get; set; }

    public virtual IList<ProductDto> Products { get; set; } = new List<ProductDto>();
}
