using e_commerce_website.Database;
using e_commerce_website.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace e_commerce_website.Controllers
    {
    [Authorize]
    public class CartController : Controller
        {

        private readonly ILogger<CartController> _logger;
        private readonly AppDbContext _context;

        public CartController(ILogger<CartController> logger, AppDbContext context)
            {
            _logger = logger;
            _context = context;
            }

        public IActionResult Index()
            {
            return View();
            }

        [HttpGet]
        [Route("api/cart/add")]
        public async Task<IActionResult> AddToCart([FromQuery] int variantId)
            {
            try
                {
                var userIdentity = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(userIdentity))
                    {
                    _logger.LogWarning("AddToCart failed: User is not authenticated.");
                    return Unauthorized(new { message = "User is not authenticated." });
                    }

                var userId = int.Parse(userIdentity);

                var cartItem = await _context.Carts
                    .Where(cart => cart.VariantID == variantId && cart.UserID == userId)
                    .FirstOrDefaultAsync();

                if (cartItem != null)
                    {
                    return BadRequest("Product Already Exists in the Cart");
                    }




                // Create and add cart to both context and variant's collection
                var cart = new Cart
                    {
                    VariantID = variantId,
                    UserID = userId
                    };

                _context.Carts.Add(cart);          // Also add to context to ensure EF tracks it

                await _context.SaveChangesAsync();

                _logger.LogInformation($"Product variant {variantId} added to cart for user {userId}.");
                return Json(new { success = true, variantId });
                }
            catch (Exception ex)
                {
                _logger.LogError(ex, "Error occurred while adding to cart.");
                return StatusCode(500, new { success = false, message = "An error occurred while adding to cart.", variantId });
                }
            }

        [HttpGet]
        [Route("api/cart/remove")]
        public async Task<IActionResult> RemoveFromCart(int variantId)
            {
            try
                {
                var userIdentity = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userIdentity))
                    {
                    _logger.LogWarning("RemoveFromCart failed: User is not authenticated.");
                    return Unauthorized(new { message = "User is not authenticated." });
                    }
                var userId = int.Parse(userIdentity);
                var cartItem = await _context.Carts
                        .Where(cart => cart.VariantID == variantId && cart.UserID == userId).Select(cart => cart).FirstOrDefaultAsync();
                if (cartItem != null)
                    {
                    _context.Carts.Remove(cartItem);
                    await _context.SaveChangesAsync();
                    }
                _logger.LogInformation($"Product variant {variantId} removed from cart for user {userId}.");
                return Json(new { success = true, variantId });
                }
            catch (Exception ex)
                {
                _logger.LogError(ex, "Error occurred while removing from cart.");
                return StatusCode(500, new { success = false, message = "An error occurred while removing from cart." });
                }
            }


        [HttpGet]
        [Route("api/cart/count")]
        public async Task<IActionResult> GetCartCount()
            {
            try
                {
                var userIdentity = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(userIdentity))
                    {
                    _logger.LogWarning("GetCartCount: User is not authenticated.");
                    return Json(new { cartItemsCount = 0 });
                    }

                var userId = int.Parse(userIdentity);

                var cartItemsCount = await _context.Carts
                    .Where(cartItem => cartItem.UserID == userId)
                    .CountAsync();

                return Json(new { cartItemsCount });
                }
            catch (Exception ex)
                {
                _logger.LogError(ex, "Error occurred while fetching cart count.");
                return StatusCode(500, new { cartItemsCount = 0, message = "An error occurred." });
                }
            }



        [HttpGet]
        [Route("api/cart/exists")]
        public async Task<IActionResult> ItemExistsInCart([FromQuery] int variantId)
            {
            try
                {
                var userIdentity = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userIdentity))
                    {
                    _logger.LogWarning("RemoveFromCart failed: User is not authenticated.");
                    return Unauthorized(new { message = "User is not authenticated." });
                    }
                var userId = int.Parse(userIdentity);
                var cartItem = await _context.Carts
                        .Where(cart => cart.VariantID == variantId && cart.UserID == userId).Select(cart => cart).FirstOrDefaultAsync();
                bool itemExists = cartItem == null ? false : true;
                return Json(new { itemExists });
                }
            catch (Exception ex)
                {
                _logger.LogError(ex, "Error occurred while fetching cart count.");
                return StatusCode(500, new { cartItemsCount = 0, message = "An error occurred." });
                }
            }

        }
    }
