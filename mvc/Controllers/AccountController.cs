using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using testing1.ViewModels;

namespace testing1.Controllers
{
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("index","home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if(ModelState.IsValid)
            {
                //KARENA CLASS UserManager menggunakan IdentityUser maka : 
                var user = new IdentityUser(){
                    UserName = model.Email,
                    Email = model.Email
                };
                var result = await userManager.CreateAsync(user,model.Password);
                if(result.Succeeded)
                {
                    // TERDAPAT 2 PARAMETER, PARAMETER 1 ADALAH USERNYA, PARAMTER 2 ADALAH CREATE SESSION COOKIE ATAU PERMANEN COOKIE
                    await signInManager.SignInAsync(user,isPersistent:false);
                    return RedirectToAction("index","home");
                }
                foreach (var err in result.Errors)
                {
                    //PARAMETER 1 KEY, 2 ERROR
                    ModelState.AddModelError("",err.Description);
                }

            }
            return View(model);
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
          
                var result = await signInManager.PasswordSignInAsync(model.Email,model.Password,model.RememberMe,false);
                if (result.Succeeded)
                {
                    return RedirectToAction("index", "home");
                }

                //PARAMETER 1 KEY, 2 ERROR
                ModelState.AddModelError(string.Empty, "Invalid Login");

            }
            return View(model);
        }
    }
}
