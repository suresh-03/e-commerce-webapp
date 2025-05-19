using e_commerce_website.Database;
using e_commerce_website.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;

namespace e_commerce_website.Controllers
    {
    public class SearchController : Controller
        {

        private readonly ILogger<SearchController> _logger;
        private readonly AppDbContext _context;

        public SearchController(ILogger<SearchController> logger, AppDbContext context)
            {
            _logger = logger;
            _context = context;
            }

        [NonAction]
        public IActionResult Index()
            {
            return View();
            }

        [HttpGet]
        [Route("api/search")]
        public async Task<IActionResult> Search([FromQuery] string query)
            {
            if (string.IsNullOrWhiteSpace(query))
                {
                _logger.LogWarning("Empty search query received.");
                return BadRequest("Search query cannot be empty.");
                }

            string filteredQuery = StringHelper.FilterQuery(query);

            var products = await _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Category).ThenInclude(c => c.ParentCategory)
                .Include(p => p.Variants).ThenInclude(v => v.Images)
                .ToListAsync();

            var matchedProducts = GetMatchedProducts(filteredQuery, products);

            _logger.LogInformation($"Fetched all matched products. Count: {matchedProducts.Count}");
            ViewData["Query"] = query;
            return PartialView("_ProductList", matchedProducts);
            }

        private List<Product> GetMatchedProducts(string query, List<Product> products)
            {
            string[] tokens = query.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            ConcurrentBag<Product> matchedProducts = new ConcurrentBag<Product>();

            Parallel.ForEach(products, product =>
                {
                    int matchCount = 0;
                    foreach (var token in tokens)
                        {
                        if (ProductMatch(product, token))
                            {
                            matchCount++;
                            }
                        }

                    double matchPercentage = (double)matchCount / tokens.Length * 100;
                    if (matchPercentage >= 75)
                        {
                        matchedProducts.Add(product);
                        }
                    // DEBUG
                    //Console.WriteLine($"Search Controller {Thread.CurrentThread.ManagedThreadId} {Thread.CurrentThread.Name}");
                });

            return matchedProducts.ToList();
            }

        private bool ProductMatch(Product product, string token)
            {
            return (product.Brand?.BrandName?.Contains(token, StringComparison.OrdinalIgnoreCase) ?? false) ||
                   (product.Category?.ParentCategory?.CategoryName?.Contains(token, StringComparison.OrdinalIgnoreCase) ?? false) ||
                   (product.Category?.CategoryName?.Contains(token, StringComparison.OrdinalIgnoreCase) ?? false) ||
                   (product.ProductName?.Contains(token, StringComparison.OrdinalIgnoreCase) ?? false) ||
                   (product.Description?.Contains(token, StringComparison.OrdinalIgnoreCase) ?? false) ||
                   (product.Variants?.Any(v => v.Color?.Contains(token, StringComparison.OrdinalIgnoreCase) ?? false) ?? false);
            }


        }
    }
