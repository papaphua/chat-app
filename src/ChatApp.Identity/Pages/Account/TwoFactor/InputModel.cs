using System.ComponentModel.DataAnnotations;

namespace ChatApp.Identity.Pages.Account.TwoFactor;

public sealed class InputModel
{
    public bool RememberLogin { get; set; }

    public string? ReturnUrl { get; set; }

    public string? Button { get; set; }

    public string? Provider { get; set; }

    [Required]
    [Display(Name = "Confirmation code")]
    public string? Token { get; set; }
}