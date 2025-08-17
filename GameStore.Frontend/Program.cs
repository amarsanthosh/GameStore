using GameStore.Frontend.Clients;
using GameStore.Frontend.Components;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddServerSideBlazor(options =>
{
    options.DetailedErrors = builder.Environment.IsDevelopment();
    options.DisconnectedCircuitRetentionPeriod = TimeSpan.FromMinutes(3);
    options.DisconnectedCircuitMaxRetained = 100;
});



// Add services to the container.
builder.Services.AddRazorComponents().AddInteractiveServerComponents();

var gameStoreApiUrl = builder.Configuration["GameStoreApiUrl"] ?? throw new Exception("GameStoreApiUrl is not set"); 

builder.Services.AddHttpClient<GamesClient>(
    Client => Client.BaseAddress = new Uri(gameStoreApiUrl)
);

builder.Services.AddHttpClient<GenresClient>(
    client => client.BaseAddress = new Uri(gameStoreApiUrl)
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


// Critical: Add cache control headers
// app.Use(async (context, next) =>
// {
//     context.Response.Headers.Add("Cache-Control", "no-store, no-cache, must-revalidate");
//     await next();
// });

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

app.Run();
