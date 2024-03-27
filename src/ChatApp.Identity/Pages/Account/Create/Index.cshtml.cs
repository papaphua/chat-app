// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.

using ChatApp.Identity.Models;
using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Test;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChatApp.Identity.Pages.Create;

[SecurityHeaders]
[AllowAnonymous]
public class Index : PageModel
{
    private readonly IIdentityServerInteractionService _interaction;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    [BindProperty] public InputModel Input { get; set; } = default!;

    public Index(
        IIdentityServerInteractionService interaction, UserManager<User> userManager, SignInManager<User> signInManager,
        TestUserStore? users = null)
    {
        _interaction = interaction;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public IActionResult OnGet(string? returnUrl)
    {
        Input = new InputModel { ReturnUrl = returnUrl };
        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        // check if we are in the context of an authorization request
        var context = await _interaction.GetAuthorizationContextAsync(Input.ReturnUrl);

        // the user clicked the "cancel" button
        if (Input.Button != "create")
        {
            if (context != null)
            {
                // if the user cancels, send a result back into IdentityServer as if they 
                // denied the consent (even if this client does not require consent).
                // this will send back an access denied OIDC error response to the client.
                await _interaction.DenyAuthorizationAsync(context, AuthorizationError.AccessDenied);

                // we can trust model.ReturnUrl since GetAuthorizationContextAsync returned non-null
                if (context.IsNativeClient())
                {
                    // The client is native, so this change in how to
                    // return the response is for better UX for the end user.
                    return this.LoadingPage(Input.ReturnUrl);
                }

                return Redirect(Input.ReturnUrl ?? "~/");
            }
            else
            {
                // since we don't have a valid context, then we just go back to the home page
                return Redirect("~/");
            }
        }

        if (await _userManager.FindByNameAsync(Input.Username!) != null)
        {
            ModelState.AddModelError("Input.Username", "Invalid username");
        }

        if (ModelState.IsValid)
        {
            var createResult = await _userManager.CreateAsync(new User
            {
                UserName = Input.Username
            }, Input.Password!);

            if (createResult.Succeeded)
            {
                var user = (await _userManager.FindByNameAsync(Input.Username!))!;

                // issue authentication cookie with subject ID and username
                var issuer = new IdentityServerUser(user.Id.ToString())
                {
                    DisplayName = user.UserName
                };

                await _signInManager.PasswordSignInAsync(user, Input.Password!, false, false);
                await HttpContext.SignInAsync(issuer);

                if (context != null)
                {
                    if (context.IsNativeClient())
                    {
                        // The client is native, so this change in how to
                        // return the response is for better UX for the end user.
                        return this.LoadingPage(Input.ReturnUrl);
                    }

                    // we can trust Input.ReturnUrl since GetAuthorizationContextAsync returned non-null
                    return Redirect(Input.ReturnUrl ?? "~/");
                }

                // request for a local page
                if (Url.IsLocalUrl(Input.ReturnUrl))
                {
                    return Redirect(Input.ReturnUrl);
                }
                else if (string.IsNullOrEmpty(Input.ReturnUrl))
                {
                    return Redirect("~/");
                }
                else
                {
                    // user might have clicked on a malicious link - should be logged
                    throw new ArgumentException("invalid return URL");
                }
            }
        }

        return Page();
    }
}