using System.Text.Json;
using System.Text.Json.Serialization;
namespace types.common
{
    public interface IMediaHttpResponse
    {
        string? id {get; set;}
        string? title {get; set;}
        string? filePath {get; set;}
        string? timeStamp {get; set;}
    }
        public class RequestedFormat()
    {
        [JsonPropertyName("format_id")]
        public string? formatId {get; set;}

        [JsonPropertyName("url")]
        public string? url {get; set;}
    }

    public class RequestedDownloads()
    {
        [JsonPropertyName("requested_formats")]
        public List<RequestedFormat> formats {get; set;}

        [JsonPropertyName("url")]
        public string? url {get; set;}
    }

    public class MediaMetaData()
    {

        [JsonPropertyName("id")]
        public string? Id {get; set;}

        [JsonPropertyName("title")]
        public string? Title {get; set;}

        public string? filePath {get; set;}


   
    }
}