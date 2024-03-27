﻿using ChatApp.Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChatApp.Identity.Pages.Account.TwoFactor;

[SecurityHeaders]
[AllowAnonymous]
public class Index : PageModel
{
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;

    public Index(SignInManager<User> signInManager, UserManager<User> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    public IList<string> TwoFactorProviders { get; set; } = [];

    [BindProperty] public InputModel Input { get; set; } = default!;

    public async Task<IActionResult> OnGet(bool rememberLogin, string? returnUrl)
    {
        var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();

        if (user is null)
            return RedirectToPage("/Account/Login/Index", new { returnUrl });

        Input = new InputModel { RememberLogin = rememberLogin, ReturnUrl = returnUrl };

        TwoFactorProviders = await _userManager.GetValidTwoFactorProvidersAsync(user);

        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();

        if (user is null)
            return RedirectToPage("/Account/Login/Index", new { Input.ReturnUrl });

        if (Input.Button == TokenOptions.DefaultEmailProvider)
        {
            var token = await _userManager.GenerateTwoFactorTokenAsync(user, TokenOptions.DefaultEmailProvider);
            Serilog.Log.Information($"TOKEN: {token}");
        }
        else if (Input.Button == TokenOptions.DefaultPhoneProvider)
        {
            var token = await _userManager.GenerateTwoFactorTokenAsync(user, TokenOptions.DefaultPhoneProvider);
            Serilog.Log.Information($"TOKEN: {token}");
        }

        return RedirectToPage("/Account/TwoFactor/Confirm", new
        {
            Input.RememberLogin,
            Input.ReturnUrl,
            provider = Input.Button
        });
    }
}