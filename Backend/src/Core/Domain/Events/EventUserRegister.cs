namespace DonghuaFlix.src.Core.Domain.Events;

public class EventUserRegister
{
    public Guid UserId { get; private set; }
    public DateTime DateRegister { get; private set; }

    public EventUserRegister(Guid userId, DateTime dateRegister)
    {
        UserId = userId;
        DateRegister = dateRegister;
    }
}