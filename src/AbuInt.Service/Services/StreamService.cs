using AbuInt.Service.DTOs.Meetings;
using AbuInt.Service.Interfaces;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Net;
using System.Text;

namespace AbuInt.Service.Services;

public class StreamService : IStreamService
{
    public async ValueTask<MeetingContent> GenerateStream(DateTime interviewTime)
    {
        string token =
            "eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOm51bGwsImlzcyI6ImZqcGd6ZFZRUkxXZ1FYb0FTMXNkLVEiLCJleHAiOjE2NzE5ODEwOTMsImlhdCI6MTY3MTM3NjI5Mn0.1l-ocq8OOtyVtZQ7h6XkdHBjkar_a6oZPwUibc4J5zU";

        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

        string userId = "_TTLPIWiR-GmsD3acapOxw";
        string url = $"https://api.zoom.us/v2/users/{userId}/meetings";

        var meeting = new Meeting
        {
            Topic = "Job Interview",
            Type = 2,
            StartTime = interviewTime,
            Duration = 30
        };

        var jsonText = JsonConvert.SerializeObject(meeting);
        var content = new StringContent(jsonText, Encoding.UTF8, mediaType: "application/json");

        var response = await httpClient.PostAsync(url, content);

        var result = await response.Content.ReadAsStringAsync();

        var jsonResult = JsonConvert.DeserializeObject<MeetingContent>(result);

        return jsonResult;
    }
}
