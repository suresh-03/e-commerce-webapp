using e_commerce_website.Database;
using e_commerce_website.Filters;
using e_commerce_website.Helpers;
using e_commerce_website.Models;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace e_commerce_website.Controllers
{
    [ServiceFilter(typeof(ApiResponseFilter))]
    public class AuthController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger<AuthController> _logger;
        private readonly IAntiforgery _antiforgery;
        public AuthController(AppDbContext context,ILogger<AuthController> logger,IAntiforgery antiforgery)
        {
            _context = context;
            _logger = logger;
            _antiforgery = antiforgery;
        }
        public IActionResult Index()
        {
            return RedirectToAction("SignUp");
        }
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        
        [HttpPost]
        public async Task<IActionResult> SignUpAPI([FromBody] User model)
        {
            try
            {
                await _antiforgery.ValidateRequestAsync(HttpContext);
                try
                {
                    if (!ModelState.IsValid)
                    {
                        return ObjectResultHelper.CreateObjectResult("error", "Invalid input data.", 400);
                    }

                    // Check if user already exists
                    var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
                    if (existingUser != null)
                    {
                        return ObjectResultHelper.CreateObjectResult("error", "Email already exists.", 400);
                    }

                    // Hash password
                    var passwordHasher = new PasswordHasher<User>();
                    model.Password = passwordHasher.HashPassword(model, model.Password); // assuming plain password is in PasswordHash prop

                    // Prepare user entity
                    var user = new User
                    {
                        FullName = model.FullName,
                        Email = model.Email,
                        Phone = model.Phone,
                        RoleType = model.RoleType,
                        Password = model.Password,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    };

                    await _context.Users.AddAsync(user);
                    await _context.SaveChangesAsync();

                }
                catch (DbUpdateException dbEx)
                {
                    _logger.LogError(dbEx, "Database error occurred during signup.");
                    return ObjectResultHelper.CreateObjectResult("error", "Database error occurred during signup. Please try again later.", 500);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An unexpected error occurred during signup.");
                    return ObjectResultHelper.CreateObjectResult("error", "An unexpected error occurred. Please try again later.", 500);

                }
            }
            catch(AntiforgeryValidationException ex)
            {
                _logger.LogError(ex, "Invalid anti-forgery token.");
                return ObjectResultHelper.CreateObjectResult("error", "Invalid anti-forgery token.", 403);

            }

            _logger.LogInformation("User registered successfully");
            return ObjectResultHelper.CreateObjectResult("success", "User registered successfully.", 201, Url.Action("SignIn", "Auth") ?? "");

        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignInAPI([FromBody] Dictionary<string,string> bodyData)
        {
            try
            {
                // Validate anti-forgery token (for [FromBody] manually)
                await _antiforgery.ValidateRequestAsync(HttpContext);

                // Input validation
                if (bodyData == null ||
                    !bodyData.TryGetValue("Email", out var email) ||
                    !bodyData.TryGetValue("Password", out var password))
                {
                    return ObjectResultHelper.CreateObjectResult("error", "Missing email or password.", 400);
                }

                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
                if (user == null)
                {
                    return ObjectResultHelper.CreateObjectResult("error", "Invalid Email", 401);
                }

                var passwordHasher = new PasswordHasher<User>();
                var result = passwordHasher.VerifyHashedPassword(user, user.Password, password);

                if (result != PasswordVerificationResult.Success)
                {
                    return ObjectResultHelper.CreateObjectResult("error", "Invalid Password", 401);
                }

                // Setup claims
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Email),
            new Claim(ClaimTypes.Role, user.RoleType.ToString())
        };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30)
                };

                // Sign in user
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                return ObjectResultHelper.CreateObjectResult("success", "User Signed in Successfully", 200, Url.Action("Index", "Home") ?? "");
            }
            catch (AntiforgeryValidationException ex)
            {
                _logger.LogWarning(ex, "Antiforgery validation failed.");
                return ObjectResultHelper.CreateObjectResult("error", "Invalid request (possible CSRF attempt).", 403);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error during signin.");
                return ObjectResultHelper.CreateObjectResult("error", "An unexpected error occurred. Please try again later.", 500);
            }
        }


        [HttpGet]
        public async Task<IActionResult> Signout()
        {
            try
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error during signout.");
                return ObjectResultHelper.CreateObjectResult("error", "An unexpected error occurred. Please try again later.", 500);
            }

            return RedirectToAction("SignIn", "Auth");
        }


    }

}
