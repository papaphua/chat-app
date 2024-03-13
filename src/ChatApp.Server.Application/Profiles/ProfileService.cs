using AutoMapper;
using ChatApp.Server.Application.Core;
using ChatApp.Server.Application.Core.Abstractions;
using ChatApp.Server.Application.Profiles.Dtos;
using ChatApp.Server.Application.Shared.Dtos;
using ChatApp.Server.Domain.Core.Abstractions.Results;
using ChatApp.Server.Domain.Resources;
using ChatApp.Server.Domain.Resources.Repositories;
using ChatApp.Server.Domain.Users;
using ChatApp.Server.Domain.Users.Errors;
using ChatApp.Server.Domain.Users.Repositories;
using Microsoft.AspNetCore.Identity;

namespace ChatApp.Server.Application.Profiles;

public sealed class ProfileService(
    IUserRepository userRepository,
    IUserAvatarRepository userAvatarRepository,
    IResourceRepository resourceRepository,
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

    public async Task<Result<AvatarDto>> AddAvatarAsync(Guid userId, NewResourceDto dto)
    {
        if (!AvatarValidator.IsValid(dto.Extension))
            return Result<AvatarDto>.Failure(UserAvatarErrors.Invalid);

        var resource = mapper.Map<Resource>(dto);
        var avatar = new UserAvatar(userId, resource.Id);

        var transaction = await unitOfWork.BeginTransactionAsync();

        try
        {
            await resourceRepository.AddAsync(resource);
            await userAvatarRepository.AddAsync(avatar);
            await unitOfWork.SaveChangesAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();

            return Result<AvatarDto>.Failure(UserAvatarErrors.AddError);
        }

        await transaction.CommitAsync();
        
        return Result<AvatarDto>.Success(mapper.Map<AvatarDto>(avatar));
    }

    public async Task<Result> RemoveAvatarAsync(Guid userId, Guid resourceId)
    {
        var avatar = await userAvatarRepository.GetByIdsAsync(userId, resourceId);
        
        if(avatar is null)
            return Result.Failure(UserAvatarErrors.NotFound);

        try
        {
            resourceRepository.Remove(avatar.Resource);
            await unitOfWork.SaveChangesAsync();
        }
        catch (Exception)
        {

            return Result.Failure(UserAvatarErrors.RemoveError);
        }

        return Result.Success();
    }
}