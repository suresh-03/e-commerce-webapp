using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using e_commerce_website.Models;
using System.Text.Json;

namespace e_commerce_website.Controllers;

public class CartController : Controller
{
    private readonly ILogger<CartController> _logger;

    public CartController(ILogger<CartController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        try
        {
            Dictionary<string, List<Product>> cartItems = null;
            var cartItemsJson = HttpContext.Session.GetString("cartItems");
            if (!string.IsNullOrEmpty(cartItemsJson))
            {
                try
                {
                    cartItems = JsonSerializer.Deserialize<Dictionary<string, List<Product>>>(cartItemsJson);
                }
                catch (JsonException jsonEx)
                {
                    _logger.LogError(jsonEx, "Failed to deserialize cart items from session JSON.");
                    HttpContext.Session.Remove("cartItems");
                    cartItems = null;
                }
                catch (NotSupportedException notSupEx)
                {
                    _logger.LogError(notSupEx, "Deserialization not supported for cart items JSON.");
                    cartItems = null;
                }
            }
            return View(cartItems);
        }
        catch (ArgumentNullException argNullEx)
        {
            _logger.LogError(argNullEx, "Session key was null when loading the cart page.");
            return RedirectToAction("Error");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while loading the cart page.");
            return RedirectToAction("Error");
        }
    }

    [HttpPost]
    public JsonResult GetLocalStorageData([FromBody] Dictionary<string,List<Product>> data)
    {
        if (data == null || !data.Any())
        {
            return Json(new { success = false, message = "No data found" });
        }
        try 
        { 
            Dictionary<string, List<Product>> cartItems = GetCartItems(data);
            HttpContext.Session.SetString("cartItems", JsonSerializer.Serialize(cartItems));
            return Json(new { success = true, message = "data retrieved successfully",cart = cartItems });
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving local storage data.");
            return Json(new { success = false, message = "An error occurred while retrieving data" });
        }
    }

    private Dictionary<string, List<Product>> GetCartItems(Dictionary<string, List<Product>> products)
    {
        var cartItems = new Dictionary<string, List<Product>>();

        try
        {
            if (products == null)
                throw new ArgumentNullException(nameof(products), "Input products dictionary is null.");

            foreach (var product in products)
            {
                if (product.Value == null)
                {
                    _logger.LogWarning("Product list for key '{Key}' is null.", product.Key);
                    continue;
                }

                if (product.Value.Count == 0)
                    continue;

                foreach (var item in product.Value)
                {
                    try
                    {
                        if (item == null)
                        {
                            _logger.LogWarning("Null Product encountered in list for key '{Key}'.", product.Key);
                            continue;
                        }

                        if (item.IsAddedToCart == true)
                        {
                            if (!cartItems.ContainsKey(product.Key))
                            {
                                cartItems[product.Key] = new List<Product>();
                            }
                            cartItems[product.Key].Add(item);
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error processing product item in key '{Key}'.", product.Key);
                    }
                }
            }
        }
        catch (ArgumentNullException ex)
        {
            _logger.LogError(ex, "Input products dictionary was null in GetCartItems.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error in GetCartItems.");
        }

        return cartItems;
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
