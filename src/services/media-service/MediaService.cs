using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using controllers;
using types.common;
using caches;
namespace controllers
{
    public class MediaService
    {
        private readonly HttpService _httpService;

        private readonly MediaCache _mediaCache;

        public MediaService(HttpService httpService, MediaCache mediaCache)
        {   
            this._mediaCache = mediaCache;
            this._httpService = httpService;
        }   

        public async Task<MediaMetaData> GetMediaMetaData(string url)
        {

            Process tempProcess = new Process{
                StartInfo = new ProcessStartInfo
                {
                    FileName = Path.Combine(Directory.GetCurrentDirectory(), "yt-dlp.exe"),
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false
                }
            };

            tempProcess.StartInfo.ArgumentList.Add("--dump-single-json");
            tempProcess.StartInfo.ArgumentList.Add("--skip-download");

            tempProcess.StartInfo.ArgumentList.Add(url);

            tempProcess.Start();
            
            string output = tempProcess.StandardOutput.ReadToEnd();

            if (!tempProcess.HasExited)
            {   
                tempProcess.StartInfo.ArgumentList.Clear();
                tempProcess.Kill();
                await tempProcess.WaitForExitAsync();
            }
            
            
            return JsonSerializer.Deserialize<MediaMetaData>(output);


        }
       
        public async Task<MediaMetaData> DownloadMediaBySourceURL(string url)
        {
 
            MediaMetaData mediaMetaData = await this.GetMediaMetaData(url);
            
            Dictionary<string, MediaMetaData> tempMediaCache = this._mediaCache.GetMediaCache();

            // If file already exists in current cache session
            if (tempMediaCache.Keys.Contains(mediaMetaData.Id))
            {
              // Retrieve meta data of that audio
              return tempMediaCache[mediaMetaData.Id];
            }

            Process tempProcess = new Process {StartInfo = new ProcessStartInfo
                {
                    FileName = Path.Combine(Directory.GetCurrentDirectory(), "yt-dlp.exe"),
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false
                }
            };
            
            string urlPath = Path.Combine(Directory.GetCurrentDirectory(), "audio");
 
            string runtimePath = Path.Combine(AppContext.BaseDirectory, "runtimes/deno/deno.exe");

            // TODO: At the current moment, we are externalizing the retrieval of the audio file
            // to YT-DLP. This is a temporary solution to cut down system complexity.
            // Would be ideal if we could reverse engineer how this is achieved, and implement it homemade.
            tempProcess.StartInfo.ArgumentList.Add("-f");
            tempProcess.StartInfo.ArgumentList.Add("bestaudio");
            tempProcess.StartInfo.ArgumentList.Add("-P");
            tempProcess.StartInfo.ArgumentList.Add(urlPath);
            tempProcess.StartInfo.ArgumentList.Add(url);
 
            tempProcess.Start();
   
            // string output = tempProcess.StandardOutput.ReadToEnd();
            // string error = await this._process.StandardError.ReadToEndAsync();
            mediaMetaData.filePath = urlPath;
            this._mediaCache.CacheMediaInfo(mediaMetaData.Id, mediaMetaData);
 
            Console.WriteLine($"Finished downloading audio");
            // Console.WriteLine(error);
            await tempProcess.WaitForExitAsync();

            return mediaMetaData;
            // VideoMetaData data = JsonSerializer.Deserialize<VideoMetaData>(output);
            // // TODO: Check if downloaded formats retrieved can have multiple elements.
            // Console.WriteLine(data.downloads[0].url);
            // return data.downloads[0].url;


        }
    
        public async Task<string> RetrieveMediaSearch(string search)
        {
            Process tempProcess = new Process {StartInfo = new ProcessStartInfo
                {
                    FileName = Path.Combine(Directory.GetCurrentDirectory(), "yt-dlp.exe"),
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false
                }   
            };
            
            // tempProcess.StartInfo.ArgumentList.Add("--no-download");
            tempProcess.StartInfo.ArgumentList.Add("--dump-json");
            tempProcess.StartInfo.ArgumentList.Add("--flat-playlist");
            tempProcess.StartInfo.ArgumentList.Add($"ytsearch5:{search}");
            
            tempProcess.Start();

            Task<string> output = tempProcess.StandardOutput.ReadToEndAsync();
            Task<string> error = tempProcess.StandardError.ReadToEndAsync();

            await tempProcess.WaitForExitAsync();

            string stdOutput = await output;
            string stdError = await error;

            // TODO: ERROR HANDLE
            
            return stdOutput;

        }
    }   

}