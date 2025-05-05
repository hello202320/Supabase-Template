using DotNetEnv;
using Supabase;
using Supabase.Postgrest.Models;
using Supabase.Postgrest.Attributes;
using MYAPI.Models;
using MYAPI.Dtos;


Env.Load();

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();

var app = builder.Build();

// Initialize Supabase client
var url = Environment.GetEnvironmentVariable("SUPABASE_URL");
var key = Environment.GetEnvironmentVariable("SUPABASE_KEY");

var options = new SupabaseOptions { AutoConnectRealtime = true };
var supabase = new Supabase.Client(url, key, options);
await supabase.InitializeAsync();

// API endpoint to fetch messages
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
