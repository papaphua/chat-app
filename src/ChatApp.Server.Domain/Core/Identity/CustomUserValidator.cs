using ChatApp.Server.Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace ChatApp.Server.Domain.Core.Identity;

public sealed class CustomUserValidator : UserValidator<User>
{
    public override Task<IdentityResult> ValidateAsync(UserManager<User> manager, User user)
    {
        var errors = new List<IdentityError>();

        if (user.UserName!.Length < 5 || user.UserName!.Length > 32)
            errors.Add(new IdentityError
            {
                Description = "The username must be between 5 and 32 characters."
            });

        return errors.Count == 0
            ? Task.FromResult(IdentityResult.Success)
            : Task.FromResult(IdentityResult.Failed(errors.ToArray()));
    }
}