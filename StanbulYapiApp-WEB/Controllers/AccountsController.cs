using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ApplicationCore.DTO_s.AccountDTO;
using ApplicationCore.Entities.Concrete;


namespace StanbulYapiApp_WEB.Controllers
{

    public class AccountsController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IPasswordHasher<AppUser> _passwordHasher;

        public AccountsController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IPasswordHasher<AppUser> passwordHasher)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _passwordHasher = passwordHasher;
        }



        [AllowAnonymous]
        public IActionResult LogIn() => View();

        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        public async Task<IActionResult> LogIn(LogInDTO model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await _userManager.FindByNameAsync(model.UserName);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, false, false);
                    if (result.Succeeded)
                    {
            
                        TempData["Success"] = "Hoşgeldiniz " + user.UserName;
                        return RedirectToAction("Index", "Home");

                    }
                }
                TempData["Error"] = "Kullanıcı adı veya şifre yanlış!!";
                return View(model);
            }
            TempData["Error"] = "Lütfen aşağıdaki kurallara uyunuz!!";
            return View(model);
        }


        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            TempData["Succes"] = "Çıkış Yapıldı";
            return RedirectToAction("LogIn");
        }
    }
}
