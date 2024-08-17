using AspNetCoreHero.ToastNotification.Abstractions;
using BlogMainStructure.UI.Models;
using BlogMainStructure.Business.DTOs.AuthorDTOs;
using BlogMainStructure.UI.Models.AccountVMs;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using BlogMainStructure.Business.Services.MailServices;
using Microsoft.AspNetCore.Authorization;
using BlogMainStructure.Business.Services.AuthorServices;
using BlogMainStructure.Domain.Enums;
using System.Security.Claims;
using BlogMainStructure.UI.Extensions;

namespace BlogMainStructure.UI.Controllers
{
    [AllowAnonymous]
    public class AccountController : BaseController
    {
        // Dependency injection for services and utilities
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IMailService _mailService;
        private readonly IAuthorService _authorService;

        // Constructor, injects the dependencies of the services
        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IMailService mailService, IAuthorService authorService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mailService = mailService;
            _authorService = authorService;
        }

        // Returns the main page view
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> MyAccount()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(userEmail);
            var authorId = await _authorService.GetAuthorIdByEmail(userEmail);
            var userRole = await _userManager.GetRolesAsync(user);

            var result = await _authorService.GetByIdAsync(authorId);

            if (!result.IsSuccess)
            {
                return RedirectToAction("Index, Home", new { Area = userRole[0].ToString() });
            }
            return View(result.Data.Adapt<ProfileUpdateVM>());
        }

        [HttpPost]
        public async Task<IActionResult> MyAccount(ProfileUpdateVM model)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var authorId = await _authorService.GetAuthorIdByEmail(userEmail);
            var authorDTO = (await _authorService.GetByIdAsync(authorId)).Data;

            if (model.Image != null && model.Image.Length > 0)
            {
                authorDTO.Image = await model.Image.StringToByteArrayAsync();
            }

            var result = await _authorService.UpdateAsync(authorDTO.Adapt<AuthorUpdateDTO>());

            var user = await _userManager.FindByEmailAsync(userEmail);
            var userRole = await _userManager.GetRolesAsync(user);

            if (!result.IsSuccess)
            {
                return RedirectToAction("Index"," Home", new { Area = userRole[0].ToString() });
            }
            return RedirectToAction("Index", "Home", new { Area = userRole[0].ToString() });

        }

        // Displays the registration page
        public async Task<IActionResult> Register()
        {
            return View();
        }

        // Handles the registration process when the form is submitted
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            // If model validation fails, redisplay the registration form
            if (!ModelState.IsValid)
                return View(model);

            // Creates a new user object
            var user = new IdentityUser
            {
                UserName = model.Email,
                Email = model.Email
            };

            // Adds the user and checks the result
            var result = await _userManager.CreateAsync(user, "Password.1");
            await _userManager.AddToRoleAsync(user, Role.Author.ToString());

            // If successful, sends an email confirmation link
            if (result.Succeeded)
            {
                try
                {
                    var codeToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackURL = Url.Action("ConfirmEmail", "Home", new { userId = user.Id, code = codeToken }, protocol: Request.Scheme);
                    var mailMessage = $"Please confirm your account by <a href='{callbackURL}'> clicking here!</a>";
                    await _mailService.SendMailAsync(model.Email, "Confirm your email", mailMessage);

                    await _authorService.AddAsync(model.Adapt<AuthorCreateDTO>());

                    return RedirectToAction("Index");
                }
                catch (Exception)
                {

                    return View("Error");
                }
            }

            return RedirectToAction("Index");
        }

        // Handles email confirmation
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {

                return RedirectToAction("Index");
            }

            var user = await _userManager.FindByIdAsync(userId);
           
            if (user == null)
            {
                return NotFound("Invalid user");
            }

            var result = await _userManager.ConfirmEmailAsync(user, code);

            return View(result.Succeeded ? "Index" : "Error");
        }

        // Displays the error page
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // Displays the login page
        public async Task<IActionResult> Login()
        {
            return View();
        }

        // Handles the login process when the form is submitted
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {

                return View(model);
            }

            var checkPassword = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
            if (!checkPassword.Succeeded)
            {

                return View(model);
            }

            var userRole = await _userManager.GetRolesAsync(user);
            if (userRole == null)
            {
                return View(model);
            }

            return RedirectToAction("Index", "Home", new { Area = userRole[0].ToString() });
        }

        // Logs out the user from the system
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

    }
}
