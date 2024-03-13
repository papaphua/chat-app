using AutoMapper;
using ChatApp.Server.Application.Core.Abstractions;
using ChatApp.Server.Application.Profiles.Dtos;
using ChatApp.Server.Domain.Core.Abstractions.Results;
using ChatApp.Server.Domain.Users;
using ChatApp.Server.Domain.Users.Errors;
using ChatApp.Server.Domain.Users.Repositories;
using Microsoft.AspNetCore.Identity;

namespace ChatApp.Server.Application.Profiles;

public sealed class ProfileService(
    IUserRepository userRepository,
    UserManager<User> userManager,
    IUnitOfWork unitOfWork,
    IMapper mapper)
    : IProfileService
{
    public async Task<Result<ProfileDto>> GetProfileAsync(Guid userId)
    {
        var user = (await userRepository.GetByIdAsync(userId, true))!;

        return Result<ProfileDto>.Success(mapper.Map<ProfileDto>(user));
    }

    public async Task<Result<DetailsDto>> UpdateDetailsAsync(Guid userId, DetailsDto dto)
    {
        var user = (await userRepository.GetByIdAsync(userId))!;

        mapper.Map(dto, user);

        try
        {
            userRepository.Update(user);
            await unitOfWork.SaveChangesAsync();
        }
        catch (Exception)
        {
            return Result<DetailsDto>.Failure(UserErrors.UpdateError);
        }

        return Result<DetailsDto>.Success(mapper.Map<DetailsDto>(user));
    }

    public async Task<Result<string>> UpdateUserNameAsync(Guid userId, string userName)
    {
        var user = (await userRepository.GetByIdAsync(userId))!;
        
        var result = await userManager.SetUserNameAsync(user, userName);

        return result.Succeeded
            ? Result<string>.Success(user.UserName!)
            : Result<string>.Failure(UserErrors.IdentityError(result.Errors));
    }
}