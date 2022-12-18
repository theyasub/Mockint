namespace AbuInt.Service.DTOs.Chats;

public class MessageCreationDto
{
    public int UserId { get; set; }

    public int RoomId { get; set; }

    public string Content { get; set; }
}
