using Microsoft.AspNetCore.Localization;
using System.Globalization;
using Microsoft.Extensions.Localization;
using AtomyWeb;
using Microsoft.AspNetCore.Components;

var builder = WebApplication.CreateBuilder(args);

// �����������
builder.Services.AddLocalization(opts => opts.ResourcesPath = "Resources");
builder.Services.AddServerSideBlazor();

// ������ �������
builder.Services.AddResponseCompression();

var app = builder.Build();

// ������������� ������ ������
app.UseResponseCompression();

// ����������� �� ������ ����������
var supportedCultures = new[] { "ru", "en", "de", "es", "ko", "zh-CN", "ja" }
    .Select(c => new CultureInfo(c)).ToList();
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("ru"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
});

app.UseHttpsRedirection();
app.UseStaticFiles();
app.MapRazorComponents<App>()
   .AddInteractiveServerRenderMode();
app.Run();