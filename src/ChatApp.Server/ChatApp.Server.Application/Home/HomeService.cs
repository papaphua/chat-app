using ChatApp.Server.Application.Home.Dtos;
using ChatApp.Server.Domain.Contacts.Repositories;
using ChatApp.Server.Domain.Core.Abstractions.Results;
using ChatApp.Server.Domain.Directs.Repositories;
using ChatApp.Server.Domain.Groups.Repositories;
using IMapper = AutoMapper.IMapper;

namespace ChatApp.Server.Application.Home;

public sealed class HomeService(
    IGroupRepository groupRepository,
    IDirectRepository directRepository,
    IDirectMessageRepository directMessageRepository,
    IContactRepository contactRepository,
    IGroupMessageRepository groupMessageRepository,
    IMapper mapper)
    : IHomeService
{
    public async Task<Result<List<ChatPreviewDto>>> GetChatPreviewsAsync(Guid userId)
    {
        List<ChatPreviewDto> previews = [];

        var directs = await GetDirectPreviewsAsync(userId);
        var groups = await GetGroupPreviewsAsync(userId);
        previews.AddRange(directs);
        previews.AddRange(groups);

        return Result<List<ChatPreviewDto>>.Success(
            previews.OrderByDescending(preview => preview.Timestamp)
                .ToList());
    }

    private async Task<List<ChatPreviewDto>> GetDirectPreviewsAsync(Guid userId)
    {
        List<ChatPreviewDto> previews = [];

        var directs = await directRepository.GetAllByUserIdAsync(userId, true, true);

        foreach (var direct in directs)
        {
            var preview = mapper.Map<ChatPreviewDto>(direct);

            var partner = direct.Memberships.Where(membership => membership.MemberId != userId)
                .Select(membership => membership.Member)
                .First();

            var contact = await contactRepository.GetByOwnerIdAndPartnerId(userId, partner.Id, true);

            var message = await directMessageRepository.GetLastMessageAsync(direct.Id, userId);

            if (message is not null)
            {
                preview.LastMessage = message.Content ??
                                      (message.Attachments.Count > 0 ? "Added attachments" : "New chat");
                preview.Timestamp = message.Timestamp;
            }

            if (contact is not null)
            {
                preview.Name = $"{contact.FirstName} {contact.LastName}";

                if (contact.Avatar is not null)
                {
                    preview.ResourceId = contact.Avatar.ResourceId;
                }
            }
            else
            {
                preview.Name = string.IsNullOrEmpty(partner.FirstName)
                    ? string.IsNullOrEmpty(partner.LastName) ? partner.UserName! : partner.LastName
                    : string.IsNullOrEmpty(partner.LastName)
                        ? partner.FirstName
                        : $"{partner.FirstName} {partner.LastName}";

                preview.ResourceId = partner.Avatars.FirstOrDefault()?.ResourceId;
            }

            previews.Add(preview);
        }

        return previews;
    }

    private async Task<List<ChatPreviewDto>> GetGroupPreviewsAsync(Guid userId)
    {
        List<ChatPreviewDto> previews = [];

        var groups = await groupRepository.GetAllByUserIdAsync(userId, true);

        foreach (var group in groups)
        {
            var preview = mapper.Map<ChatPreviewDto>(group);

            var message = await groupMessageRepository.GetLastMessageAsync(group.Id, userId);

            if (message is not null)
            {
                preview.LastMessage = message.Content ??
                                      (message.Attachments.Count > 0 ? "Added attachments" : "New chat");
                preview.Timestamp = message.Timestamp;
            }

            previews.Add(preview);
        }

        return previews;
    }
}