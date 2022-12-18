using Newtonsoft.Json;

namespace AbuInt.Service.DTOs.Meetings;

public class Meeting
{
    [JsonProperty("topic")]
    public string Topic { get; set; }

    [JsonProperty("type")]
    public int Type { get; set; }

    [JsonProperty("start_time")]
    public DateTime StartTime { get; set; }

    [JsonProperty("duration")]
    public int Duration { get; set; }
}
