 
using Microsoft.AspNetCore.Mvc;
using types.common;
using System.Diagnostics;
using System.Text.Json.Serialization;
using System.Text.Json;
using types.common;

namespace controllers
{
    

    [ApiController]
    [Route("[controller]/[action]")]
    public class ClientController : ControllerBase
    {

        private readonly MediaService _mediaService;

        private readonly HttpService _httpService;

        public ClientController(MediaService mediaController, HttpService httpController) {
            this._mediaService = mediaController;
            this._httpService = httpController;
        }

        [HttpPost]
        public async Task<IActionResult> DownloadMedia([FromBody] DownloadMediaRequest request)
        {
            MediaMetaData response = await this._mediaService.DownloadMediaBySourceURL(request.Url);

            if (response != null)
            {
                return Ok(new {message = $"Audio downloaded at {response!.filePath}", mediapath = response.filePath, id = response.Id, title = response.Title});
            } else {
                 return BadRequest(new {message = "Error downloading media"});
            }    
                
        }

        [HttpPost]
        public async Task<IActionResult> SearchSong([FromBody] SearchMediaRequest request)
        {
            string output = await this._mediaService.RetrieveMediaSearch(request.SongName);

            List<MediaMetaData> results = output.Split("\n", StringSplitOptions.RemoveEmptyEntries)
            .Select(line => JsonSerializer.Deserialize<MediaMetaData>(line)).ToList();
         
            return Ok(new {message = results});
        }

    }
}