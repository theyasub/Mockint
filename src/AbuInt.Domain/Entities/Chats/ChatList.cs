using AbuInt.Domain.Commons;
using AbuInt.Domain.Entities.Users;

namespace AbuInt.Domain.Entities.Chats;

public class ChatList : Auditable
{
    public int FirstUserId { get; set; }
    public User FirstUser { get; set; }

    public int SecondUserId { get; set; }
    public User SecondUser { get; set; }
}