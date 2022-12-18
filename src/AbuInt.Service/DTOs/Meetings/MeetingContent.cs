using Newtonsoft.Json;

namespace AbuInt.Service.DTOs.Meetings;

public class MeetingContent
{
    [JsonProperty("start_time")]
    public string StartTime { get; set; }

    [JsonProperty("duration")]
    public int Duration { get; set; }

    [JsonProperty("timezone")]
    public string TimeZone { get; set; }

    [JsonProperty("start_url")]
    public string Url { get; set; }
}
