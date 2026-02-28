using JwT_Core_MVC.Models;
using JwT_Core_MVC.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace JwT_Core_MVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly JwtTokenService _jwtService;

        public AuthController(JwtTokenService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            //  For demo only (replace with DB validation later)
            if (model.Username == "admin" && model.Password == "123")
            {
                var token = _jwtService.GenerateToken(model.Username);

                // Store token in cookie (for MVC usage)
                Response.Cookies.Append("jwt", token);

                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Invalid credentials";
            return View();
        }

        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt");
            return RedirectToAction("Login");
        }
    }
}
