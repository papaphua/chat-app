// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.

using System.ComponentModel.DataAnnotations;

namespace ChatApp.Identity.Pages.Account.Create;

public class InputModel
{
    [Required] public string? Username { get; set; }

    [Required] public string? Password { get; set; }

    public string? ReturnUrl { get; set; }

    public string? Button { get; set; }
}