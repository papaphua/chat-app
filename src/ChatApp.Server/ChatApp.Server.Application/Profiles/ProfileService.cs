using AutoMapper;
using ChatApp.Server.Application.Core;
using ChatApp.Server.Application.Core.Abstractions;
using ChatApp.Server.Application.Core.Extensions;
using ChatApp.Server.Application.Profiles.Dtos;
using ChatApp.Server.Application.Shared.Dtos;
using ChatApp.Server.Domain.Core.Abstractions.Results;
using ChatApp.Server.Domain.Resources;
using ChatApp.Server.Domain.Resources.Repositories;
using ChatApp.Server.Domain.Users;
using ChatApp.Server.Domain.Users.Errors;
using ChatApp.Server.Domain.Users.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace ChatApp.Server.Application.Profiles;

public sealed class ProfileService(
    IUserRepository userRepository,
    IUserAvatarRepository userAvatarRepository,
    IResourceRepository resourceRepository,
    UserManager<User> userManager,
    IEmailService emailService,
    ISmsService smsService,
    IUnitOfWork unitOfWork,
    IMapper mapper)
    : IProfileService
{
    public async Task<Result<ProfileDto>> GetProfileAsync(Guid userId)
    {
        var user = (await userRepository.GetByIdAsync(userId, true))!;

        return Result<ProfileDto>.Success(mapper.Map<ProfileDto>(user));
    }

    public async Task<Result> UpdateNameAsync(Guid userId, ProfileNameDto dto)
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
            return Result.Failure(UserErrors.UpdateError);
        }

        return Result.Success();
    }

    public async Task<Result> UpdateUserNameAsync(Guid userId, string userName)
    {
        var user = (await userRepository.GetByIdAsync(userId))!;

        var result = await userManager.SetUserNameAsync(user, userName);

        return result.Succeeded
            ? Result.Success()
            : Result.Failure(UserErrors.IdentityError(result.Errors));
    }

    public async Task<Result<AvatarDto>> AddAvatarAsync(Guid userId, IFormFile file)
    {
        var resource = file.ToResource();

        if (!FileExtensionMapping.IsImage(resource))
            return Result<AvatarDto>.Failure(UserAvatarErrors.Invalid);

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

            return Result<AvatarDto>.Failure(UserAvatarErrors.CreateError);
        }

        await transaction.CommitAsync();

        return Result<AvatarDto>.Success(mapper.Map<AvatarDto>(avatar));
    }

    public async Task<Result> RemoveAvatarAsync(Guid userId, Guid resourceId)
    {
        var avatar = await userAvatarRepository.GetByIdsAsync(userId, resourceId, true);

        if (avatar is null)
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

    public async Task<Result> SendChangeEmailTokenAsync(Guid userId, string email)
    {
        var user = (await userRepository.GetByIdAsync(userId))!;

        var token = await userManager.GenerateChangeEmailTokenAsync(user, email);

        var message = MessageTemplate.Confirmation(email, token);

        await emailService.SendMessageAsync(message);

        return Result.Success();
    }

    public async Task<Result> ChangeEmailAsync(Guid userId, string email, string token)
    {
        var user = (await userRepository.GetByIdAsync(userId))!;

        var result = await userManager.ChangeEmailAsync(user, email, token);
        
        return result.Succeeded
            ? Result.Success()
            : Result.Failure(UserErrors.IdentityError(result.Errors));
    }

    public async Task<Result> SendChangePhoneTokenAsync(Guid userId, string number)
    {
        var user = (await userRepository.GetByIdAsync(userId))!;

        var token = await userManager.GenerateChangePhoneNumberTokenAsync(user, number);

        var message = MessageTemplate.Confirmation(number, token);

        await smsService.SendMessageAsync(message);

        return Result.Success();
    }

    public async Task<Result> ChangePhoneAsync(Guid userId, string number, string token)
    {
        var user = (await userRepository.GetByIdAsync(userId))!;

        var result = await userManager.ChangePhoneNumberAsync(user, number, token);
        
        return result.Succeeded
            ? Result.Success()
            : Result.Failure(UserErrors.IdentityError(result.Errors));
    }
}