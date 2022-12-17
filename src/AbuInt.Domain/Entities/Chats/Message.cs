using AbuInt.Domain.Commons;
using AbuInt.Domain.Entities.Users;

namespace AbuInt.Domain.Entities.Chats;

public class Message : Auditable
{
    public int FromUserId { get; set; }
    public User FromUser { get; set; }

    public int ToUserId { get; set; }
    public User ToUser { get; set; }

    public int ChatListId { get; set; }
    public ChatList ChatList { get; set; }
    
    public string Content { get; set; }
}