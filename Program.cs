using Microsoft.AspNetCore.Mvc.Authorization;
using spotify_api.Factories.IFactories;
using spotify_api.Helper;
using spotify_api.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddHttpClient();

builder.Services.AddControllers();
builder.Services.AddControllersWithViews(options =>
{
}).AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization(options =>
    {
    }).AddJsonOptions(options =>
    {

        options.JsonSerializerOptions.ReferenceHandler =
        System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//add interfaces
builder.Services.AddTransient<IFactories, spotify_api.Factories.Factories>();
builder.Services.AddTransient<IMusicService, SpotifyService>();
builder.Services.AddTransient<IHttpClientHelper, HttpCLientHelper>();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
