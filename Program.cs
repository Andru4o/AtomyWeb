using AtomyWeb.Components;
using AtomyWeb.Services;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using Microsoft.Extensions.Localization;

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

// Use localization
app.UseRequestLocalization(app.Services.GetRequiredService<Microsoft.Extensions.Options.IOptions<RequestLocalizationOptions>>().Value);
app.UseResponseCompression();
app.UseRouting();
app.MapBlazorHub();
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
