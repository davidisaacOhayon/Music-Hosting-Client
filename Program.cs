

using controllers;
using System.Net.Http;
 
using System.Security.Cryptography;
using System.CodeDom;
using Microsoft.Extensions.DependencyInjection;
using caches;

namespace Program {

    public class Program {

        static ServiceCollection _services = new ();

        static ServiceProvider _serviceProvider;

        private string LastPlayedSong;




        public static void Main(string[] args) {
            // InitializeEssentials();

            // InitializeControllers();

            // InitializeHelpers();
            
            registerServices();

            TestProcessor();

        }


        public static async void TestProcessor()
        {
            Console.WriteLine("Testing processor, downloading music.");
           _serviceProvider.GetRequiredService<MediaController>().DownloadMediaBySourceURL("https://www.youtube.com/watch?v=FF0s7dtEKIA");

        //    _serviceProvider.GetRequiredService<MediaController>().DownloadAudio(output);
        }
 
        public static void registerServices()
        {

            // Helper Dependencies
            _services.AddSingleton<HttpClient>();

            // Caches
            _services.AddSingleton<MediaCache>();
 
            
            // Controllers
            _services.AddSingleton<HttpController>();
            _services.AddSingleton<MediaController>();

            Console.WriteLine("Building service provider.");
            // Builds Service Provider
            _serviceProvider = _services.BuildServiceProvider();

        }


    }


}