using AtomyWeb.Components;
using AtomyWeb.Services;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Builder;
using System.Globalization;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Telegram.Bot.Types;
using Telegram.Bot;
using Dadata;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddLocalization(options => {
    options.ResourcesPath = "Resources";
});
var supportedCultureCodes = new[] { "ru", "en", "de", "es", "ko", "zh-CN", "ja" };
var supportedCultures = supportedCultureCodes
    .Select(code => new CultureInfo(code))
    .ToList();

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture("ru");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
    // Allow culture selection via query string: "?culture=ru" or "?culture=en"
    options.RequestCultureProviders.Insert(0, new QueryStringRequestCultureProvider
    {
        QueryStringKey = "culture",
        UIQueryStringKey = "ui-culture"
    });
});
// Add response compression
builder.Services.AddResponseCompression(options => {
    options.EnableForHttps = true;
});
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpClient();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

var app = builder.Build();
app.MapFallbackToFile("/Components/App");
// Use localization
app.UseRequestLocalization(app.Services.GetRequiredService<Microsoft.Extensions.Options.IOptions<RequestLocalizationOptions>>().Value);
app.UseResponseCompression();
app.UseRouting();
app.MapBlazorHub();
app.UseDeveloperExceptionPage();
app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx => {
        var path = ctx.File.PhysicalPath;
        if (path.EndsWith(".css") || path.EndsWith(".js"))
        {
            ctx.Context.Response.Headers["Cache-Control"] = "public,max-age=31536000";
        }
    }
});

app.MapPost("/api/register", async (RegistrationDto dto) =>
{
  TelegramBotClient botClient = new TelegramBotClient("6305344699:AAG5shpez6RiKOBHjCOKeq6T3nzyAb_DdRI");
  ChatId chatId = new ChatId(-4637825390L);
  var message = $"Новая регистрация:\n" +
                $"ФИО: {dto.LastName} {dto.FirstName} {dto.MiddleName}\n" +
                $"Дата рождения: {dto.BirthDate}\n" +
                $"Адрес: {dto.Address}\n" +
                $"Email: {dto.Email}\n" +
                $"Телефон: {dto.Phone}";
  await botClient.SendMessage(chatId, message);
  return Results.Ok(new { status = "ok" });
});

app.MapPost("/api/checkAddress", async (DadataRequest req) =>
{
  var token = "9d29b2603755cdefc6dbf507415674e3ec99a12d";
  var api = new SuggestClientAsync(token);
  var result = await api.SuggestAddress(req.query);
  return result.suggestions.Select(z => z.value).ToList();
});

app.MapPost("/api/checkFIO", async (DadataRequest req) =>
{
  var token = "9d29b2603755cdefc6dbf507415674e3ec99a12d";
  var api = new SuggestClientAsync(token);
  var result = await api.SuggestName(req.query);
  return result.suggestions.Select(z => z.value).ToList();
});

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
