using e_commerce_website.Database;
using e_commerce_website.Helpers;
using e_commerce_website.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_website.ViewComponents
    {
    public class CategoryMenuViewComponent : ViewComponent
        {
        private readonly AppDbContext _context;
        private readonly ILogger<CategoryMenuViewComponent> _logger;
        public CategoryMenuViewComponent(AppDbContext context, ILogger<CategoryMenuViewComponent> logger)
            {
            _context = context;
            _logger = logger;
            }


        public async Task<IViewComponentResult> InvokeAsync()
            {
            try
                {
                var categoriesDb = await _context.Categories
                    .Where(c => c.ParentCategoryID != null)
                    .Include(c => c.SubCategories)// Exclude top-level or null-parent categories
                    .Select(c => new
                        {
                        c.CategoryName,
                        c.ParentCategoryID
                        })
                    .ToListAsync();

                var categoriesDictionary = categoriesDb
                    .GroupBy(c => c.ParentCategoryID) // Safe to use .Value since nulls are filtered
                    .ToDictionary(
                        g => ((GenderType)g.Key).ToString(),
                        g => g.Select(c => c.CategoryName).ToList()
                    );
                _logger.LogInformation(JsonHelper.AsJsonString(categoriesDictionary));
                return View(categoriesDictionary);
                }
            catch (Exception ex)
                {
                _logger.LogError(ex, "Exception occured when getting categories");
                }

            return View(null);

            }


        }
    }
