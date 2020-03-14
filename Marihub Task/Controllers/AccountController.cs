using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Marihub_Task.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Marihub_Task.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> userManager
            , SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Employee");
                }
               
                    ModelState.AddModelError("", "Invalid Login");
                
            }
            return View(model);
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(Manager manager)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = manager.Email, Email = manager.Email };
                var result = await userManager.CreateAsync(user, manager.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Employee");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View();
        }
        [HttpPost]
        [Route("api/Authorization/get")]
        public IActionResult GetToken()
        {
            //Create Security Key
            string securityKey = "This My Security key made by ";
            //Symetric Key
            var key = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));

            //var symmetricSecuritykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
            ////signingCredintals
            var signingCredintals = new Microsoft.IdentityModel.Tokens.SigningCredentials(key, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature);
            //var signingCredintals = new SigningCredentials(symmetricSecuritykey, SecurityAlgorithms.HmacSha256Signature);

            //add Claims
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Role, "Admin"));
            claims.Add(new Claim(ClaimTypes.Role, "User"));

            claims.Add(new Claim("LoggenInUserId", "123"));
            ////Create token
            var token = new JwtSecurityToken(
                issuer: "smesk.in",
                audience: "readers",
                expires: DateTime.Now.AddHours(1),
                signingCredentials: signingCredintals,
                claims: claims
                );
            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }

    }
    }
