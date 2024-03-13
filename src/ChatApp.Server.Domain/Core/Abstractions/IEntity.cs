namespace ChatApp.Server.Domain.Core.Abstractions;

public interface IEntity
{
}

public interface IEntity<TKey> : IEntity
{
    TKey Id { get; set; }
}