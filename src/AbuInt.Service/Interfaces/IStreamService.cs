using AbuInt.Service.DTOs.Meetings;

namespace AbuInt.Service.Interfaces;

public interface IStreamService
{
    /// <summary>
    /// Generate Zoom Meeting Interview
    /// </summary>
    /// <returns></returns>
    ValueTask<MeetingContent> GenerateStream();
}
