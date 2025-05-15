using e_commerce_website.Database;
using e_commerce_website.Filters;
using e_commerce_website.Helpers;
using e_commerce_website.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_website.Controllers
{
    [ServiceFilter(typeof(ApiResponseFilter))]
    public class AuthController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger<AuthController> _logger;
        public AuthController(AppDbContext context,ILogger<AuthController> logger)
        {
            _context = context;
            _logger = logger;
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
        [Route("api/auth/signup")]
        public IActionResult SignUp([FromBody] User model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return ObjectResultHelper.CreateObjectResult("error", "Invalid input data.", 400);
                }

                // Check if user already exists
                var existingUser = _context.Users.FirstOrDefault(u => u.Email == model.Email);
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

                _context.Users.Add(user);
                _context.SaveChanges();

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

            _logger.LogInformation("User registered successfully");
            return ObjectResultHelper.CreateObjectResult("success", "User registered successfully.", 201, Url.Action("SignIn", "Auth") ?? "");

        }

    }

}
