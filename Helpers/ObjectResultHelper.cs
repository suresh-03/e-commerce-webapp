using e_commerce_website.Models.auth;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce_website.Helpers
    {
    public sealed class ObjectResultHelper
        {
        public static ObjectResult CreateObjectResult(string status, string message, int statusCode, string redirectUrl = "")
            {
            return new ObjectResult(new ApiResponse
                {
                Status = status,
                Message = message,
                StatusCode = statusCode,
                RedirectUrl = redirectUrl
                });
            }
        }
    }
