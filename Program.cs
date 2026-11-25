

using controllers;
using System.Net.Http;
 
using System.Security.Cryptography;
using System.CodeDom;
using Microsoft.Extensions.DependencyInjection;
using caches;


var builder = WebApplication.CreateBuilder(args);

// Helper Dependencies
builder.Services.AddSingleton<HttpClient>();

// Caches
builder.Services.AddSingleton<MediaCache>();

// Controllers
builder.Services.AddSingleton<HttpService>();
builder.Services.AddSingleton<MediaService>();
builder.Services.AddControllers();

var app = builder.Build();
 
app.MapControllers();
app.Run();
