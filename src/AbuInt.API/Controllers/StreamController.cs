using AbuInt.Service.DTOs.Meetings;
using AbuInt.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

namespace AbuInt.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StreamController : RESTFulController
    {
        private readonly IStreamService streamService;
        public StreamController(IStreamService streamService)
        {
            this.streamService = streamService;
        }

        /// <summary>
        /// Generate Zoom interview meeting
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<MeetingContent> GenerateMeeting()
            => Ok(this.streamService.GenerateStream());

    }
}
