using ChatApp.Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChatApp.Identity.Pages.Account.TwoFactor;

[SecurityHeaders]
[AllowAnonymous]
public class Confirm : PageModel
{
    private readonly SignInManager<User> _signInManager;

    public Confirm(SignInManager<User> signInManager)
    {
        _signInManager = signInManager;
    }

    [BindProperty] public InputModel Input { get; set; } = default!;

    public async Task<IActionResult> OnGet(bool rememberLogin, string? returnUrl, string provider)
    {
        var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();

        if (user is null)
            return RedirectToPage("/Account/Login/Index", new { returnUrl });

        Input = new InputModel { RememberLogin = rememberLogin, ReturnUrl = returnUrl, Provider = provider };

        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();

        if (user is null)
            return RedirectToPage("/Account/Login/Index", new { Input.ReturnUrl });

        if (ModelState.IsValid)
        {
            var result = await _signInManager.TwoFactorSignInAsync(Input.Provider!, Input.Token!, Input.RememberLogin,
                Input.RememberLogin);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("Input.Token", "Invalid confirmation code");
                return Page();
            }
        }

        return Redirect(Input.ReturnUrl!);
    }
}