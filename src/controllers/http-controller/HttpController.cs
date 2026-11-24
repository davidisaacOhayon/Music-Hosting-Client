using System.Collections.Generic;
using System;
using System.Net.Http;
using controllers.common;
using System.Net;
 
using System.IO;
using System.Net.Http.Json;
using types.common;

namespace controllers { 
    public class HttpController : BaseController{

        private Dictionary<string, HttpResponseMessage> cache {get;} = new Dictionary<string, HttpResponseMessage>();
        
        private MediaController _mediaProcessor {get;}
        
        private HttpClient _httpClient {get;}

        public HttpController(HttpClient HttpClient) {
            this._httpClient = HttpClient;
        }

        public async Task<HttpResponseMessage> requestDownloadMedia(string url, string filePath)
        {
            try
            {
                HttpResponseMessage response = await this._httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
 
                await using var mediaStream = await response.Content.ReadAsStreamAsync();
                await using var fileStream = File.Create(filePath);

                await mediaStream.CopyToAsync(fileStream);
                await fileStream.FlushAsync();

                return new HttpResponseMessage(HttpStatusCode.OK) {Content = new StringContent($"Media downloaded at path {filePath}"), StatusCode = HttpStatusCode.OK};

            } catch ( Exception e ) {
                return new HttpResponseMessage(HttpStatusCode.BadRequest) {Content = new StringContent($"Something had gone wrong issuing this request {e.Message}"), StatusCode = HttpStatusCode.BadRequest};
            }
        }
        
        public async Task<HttpResponseMessage> request(string url, HttpContent? body = null) 
        {
            HttpResponseMessage response;
            try
            {
                if ( body != null)
                {
                    response = await this._httpClient.PostAsync(url, body);
                } else
                {
                    response = await this._httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
                }
                
                response.EnsureSuccessStatusCode();

                return response;

            } catch (Exception e) {
                
                return new HttpResponseMessage(HttpStatusCode.BadRequest) {Content = new StringContent($"Something had gone wrong issuing this request {e.Message}"), StatusCode = HttpStatusCode.BadRequest};
                            
            }
        
        }
        public async Task<bool> TestFunctionality() {
            if(this._httpClient != null) {
                
                try {
                   HttpResponseMessage response = await this._httpClient.GetAsync("https://jsonplaceholder.typicode.com/posts");
                   response.EnsureSuccessStatusCode();

                   Console.WriteLine("Http Controller Confirmed working order.");
                    //    string responseBody = await response.Content.ReadAsStringAsync();
                    //    Console.WriteLine(responseBody);
                   return true;

                } catch (Exception e){
                    Console.WriteLine($"An Error Occured, HTTP Controller isn't working.: {e.Message}");
                    return false;

                }

            } else {
                Console.WriteLine("Http Controller isn't working, HttpClient instance is null.");
                return false;
            }
        }
        


    }


}