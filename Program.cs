using DotNetEnv;
using Supabase;
using MYAPI.Models;
using MYAPI.Dtos;

var builder = WebApplication.CreateBuilder(args);

Env.Load();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:4200", "https://v101.netlify.app/") 
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddEndpointsApiExplorer();
var app = builder.Build();

app.UseCors();


var url = Environment.GetEnvironmentVariable("SUPABASE_URL");
var key = Environment.GetEnvironmentVariable("SUPABASE_KEY");

var options = new SupabaseOptions { AutoConnectRealtime = true };
var supabase = new Supabase.Client(url, key, options);
await supabase.InitializeAsync();

app.MapGet("/messages", async () =>
{
    var result = await supabase.From<Message>().Get();

    var dtoList = result.Models.Select(m => new MessageDto
    {
        Id = m.Id,
        CreatedAt = m.CreatedAt,
        Content = m.Content
    });

    return Results.Ok(dtoList);
});

app.Run();
