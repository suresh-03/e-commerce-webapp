using e_commerce_website.Database;
using e_commerce_website.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace e_commerce_website.Controllers;

public class ProductController : Controller
    {
    private readonly ILogger<ProductController> _logger;
    private readonly AppDbContext _context;

    public ProductController(ILogger<ProductController> logger, AppDbContext context)
        {
        _logger = logger;
        _context = context;
        }

    [HttpGet]
    public async Task<IActionResult> Index(string category)
        {
        try
            {
            List<Product> products;

            if (string.IsNullOrEmpty(category))
                {
                // If no category provided, return all products
                products = await _context.Products
                    .Include(p => p.Brand)
                    .Include(p => p.Category)
                    .Include(p => p.Variants)
                        .ThenInclude(v => v.Images)
                    .ToListAsync();

                _logger.LogInformation($"Fetched all products. Count: {products.Count}");
                }
            else
                {
                // Filter products by category name
                products = await _context.Products
                    .Include(p => p.Brand)
                    .Include(p => p.Category)
                    .Include(p => p.Variants)
                        .ThenInclude(v => v.Images)
                    .Where(p => p.Category.CategoryName == category)
                    .ToListAsync();

                _logger.LogInformation($"Fetched products for category '{category}'. Count: {products.Count}");
                }

            return View(products);
            }
        catch (Exception ex)
            {
            _logger.LogError(ex, "Error fetching products in Index method.");
            return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }



    [HttpGet]
    public async Task<IActionResult> Details(int id, string category)
        {
        if (id <= 0)
            {
            _logger.LogWarning("Invalid product ID: {ProductId}", id);
            return BadRequest("Invalid product ID.");
            }

        if (string.IsNullOrWhiteSpace(category))
            {
            _logger.LogWarning("Missing category for product ID: {ProductId}", id);
            return BadRequest("Category is required.");
            }

        try
            {
            var product = await _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Include(p => p.Variants)
                    .ThenInclude(v => v.Images)
                .FirstOrDefaultAsync(p => p.ProductID == id && p.Category.CategoryName == category);

            if (product == null)
                {
                _logger.LogInformation("Product not found. ID: {ProductId}, Category: {Category}", id, category);
                return NotFound("Product not found.");
                }

            return View(product);
            }
        catch (Exception ex)
            {
            _logger.LogError(ex, "Error occurred while retrieving product details for ID: {ProductId}", id);
            return StatusCode(500, "An error occurred while processing your request.");
            }
        }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
        {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
