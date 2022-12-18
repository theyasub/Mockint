using AbuInt.Domain.Commons;
using AbuInt.Domain.Entities.Users;

namespace AbuInt.Domain.Entities.Chats;

public class Participant : Auditable
{
    public int UserId { get; set; }
    public User User { get; set; }

    public int RoomId { get; set; }
    public Room Room { get; set; }
}
