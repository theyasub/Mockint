using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Net;
using System.Text;

namespace AbuInt.Service.Services;

public class StreamService
{
    public void GenerateStream()
    {
        var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
        var now = DateTime.UtcNow;
        var apiSecret = "Your API secret";
        byte[] symmetricKey = Encoding.ASCII.GetBytes(apiSecret);


        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = "Your API Key",
            Expires = now.AddSeconds(300),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256),
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);


        var client = new RestClient("https://api.zoom.us/v2/users/{userid}/meetings");
        var request = new RestRequest(Method.Post.ToString());
        request.RequestFormat = DataFormat.Json;
        request.AddJsonBody(new { topic = "Meeting with Dev", duration = "10", start_time = "2021-03-20T05:00:00", type = "2" });
        request.AddHeader("authorization", String.Format("Bearer {0}", tokenString));


        var restResponse = client.Execute(request);
        HttpStatusCode statusCode = restResponse.StatusCode;
        int numericStatusCode = (int)statusCode;
        var jObject = JObject.Parse(restResponse.Content);
        var e = (string)jObject["start_url"];
        var a = (string)jObject["join_url"];
        var r = Convert.ToString(numericStatusCode);
    }
}
