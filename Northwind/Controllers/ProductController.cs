using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Northwind.Data;
using Northwind.Dtos;
using Northwind.Models;


namespace Northwind.Controllers;

public class ProductController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly HttpClient _client;

    public ProductController(ApplicationDbContext context)
    {
        _context = context;
        _client = new HttpClient();

    }

    [HttpGet]
    public async Task<List<CategoryDto>>GetCategoriesFromApi()
    {
        ApiResponse<List<CategoryDto>> responseWrapper = new();
        HttpResponseMessage response = await _client.GetAsync($"http://localhost:5154/api/Categories/GetCategories");
        if (response.IsSuccessStatusCode)
        {
            string data = response.Content.ReadAsStringAsync().Result;

            responseWrapper = JsonConvert.DeserializeObject<ApiResponse<List<CategoryDto>>>(data);
            if (!responseWrapper.IsSuccess)
            {
                //
            }
        }
        var categories = responseWrapper.Data;
        return categories;
    }

    [HttpGet]
    public async Task<Product> GetProductFromApi(int id)
    {
        Product product = new();
        HttpResponseMessage response = await _client.GetAsync($"http://localhost:5154/api/Products/{id}");
        if (response.IsSuccessStatusCode)
        {
            string data = response.Content.ReadAsStringAsync().Result;

            product = JsonConvert.DeserializeObject<Product>(data);           
        }
        return product;
    }


    [HttpGet]
    public async Task<List<CategoryDto>> GetCategoriesAsync(int id)
    {
        ViewBag.categoryId = id;
        var categories = await GetCategoriesFromApi();
        return categories;
    }

    // GET: ProductController
    public async Task<ActionResult> Index(int id = 1)
    {
        var model = await GetCategoriesAsync(id);
        return View(model);
        //return View(ViewBag.Categories = new SelectList(List of categories for show in view, "Id", "Name"));
    }

    public async Task<IActionResult> GetProductsByCategoryId(int categoryId)
    {
        var categories = await GetCategoriesAsync(categoryId);
        var products = categories.Where(x => x.CategoryId == categoryId).FirstOrDefault().Products.ToList();
        return Ok(products);
    }

    // GET: ProductController/Details/5
    public ActionResult Details(int id)
    {
        return View();
    }

    // POST: ProductController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(IFormCollection collection)
    {
        try
        {
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }

    // POST: ProductController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(int id, IFormCollection collection)
    {
        try
        {
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }

    // POST: ProductController/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Delete(int id, IFormCollection collection)
    {
        try
        {
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }

    [HttpGet]
    public async Task<IActionResult>Detail(int id)
    {
        var product = await GetProductFromApi(id);
        return View(product);
    }
}
