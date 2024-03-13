namespace ChatApp.Server.Domain.Core.Abstractions;

public interface IEntity<TKey> : IEntity
{
    TKey Id { get; set; }
}