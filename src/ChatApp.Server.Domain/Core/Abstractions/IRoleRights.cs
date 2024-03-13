namespace ChatApp.Server.Domain.Core.Abstractions;

public interface IRoleRights<TRole, TRightType>
    where TRole : IEntity
    where TRightType : Enum
{
    Guid RoleId { get; set; }
    
    TRole Role { get; set; }
    
    TRightType Type { get; set; }
}