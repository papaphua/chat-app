using System.Text;
using Microsoft.AspNetCore.Identity;

namespace ChatApp.Server.Domain.Core.Extensions;

public static class IdentityErrorExtensions
{
    public static string ConvertToString(this IEnumerable<IdentityError> errors)
    {
        var stringBuilder = new StringBuilder();

        foreach (var error in errors) stringBuilder.Append(error.Description);

        return stringBuilder.ToString();
    }
}