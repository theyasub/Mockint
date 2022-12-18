using AbuInt.Domain.Commons;
using AbuInt.Domain.Entities.Users;

namespace AbuInt.Domain.Entities.Chats;

public class Message : Auditable
{
    public int UserId { get; set; }
    public virtual User User { get; set; }

    public int RoomId { get; set; }
    public virtual Room Room { get; set; }

    public string Content { get; set; }
}