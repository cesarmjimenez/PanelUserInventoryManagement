using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PanelUserInventoryManagement;
using PanelUserInventoryManagement.Auth;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<CustomAuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(s =>
s.GetRequiredService<CustomAuthStateProvider>());

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:44347")
});

await builder.Build().RunAsync();
