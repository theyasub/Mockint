using AbuInt.Domain.Commons;
using AbuInt.Domain.Enums;

namespace AbuInt.Domain.Entities.Chats;

public class Room : BaseEntity
{
    public virtual ICollection<Participant> Participants { get; set; }
    public virtual ICollection<Message> Messages { get; set; }
    public virtual ICollection<Interview> Interviews { get; set; }

    public RoomType RoomType { get; set; }
}
