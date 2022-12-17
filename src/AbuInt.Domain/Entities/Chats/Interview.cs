using AbuInt.Domain.Commons;

namespace AbuInt.Domain.Entities.Chats;

public class Interview : Auditable
{
    public int RoomId { get; set; }
    public Room Room { get; set; }
    public DateTime StartTime { get; set; }
}