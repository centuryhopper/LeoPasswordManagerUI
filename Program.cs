using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using LeoPasswordManagerUI;
using Blazored.Modal;
using Radzen;
using LeoPasswordManagerUI.Utils;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.LocalStorage;
using LeoPasswordManagerUI.Handlers;
using System.Net;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazoredModal();
builder.Services.AddRadzenComponents();

// builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services
    .AddHttpClient(
        Constants.HTTP_CLIENT_FACTORY,
        client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
    )
    .AddHttpMessageHandler<CookieHandler>()
    .AddHttpMessageHandler<UnauthorizedDelegatingHandler>();

// Supply HttpClient instances that include access tokens when making requests
// to the server project
builder.Services.AddScoped(
    sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient(Constants.HTTP_CLIENT_FACTORY)
);

builder.Services.AddBlazoredLocalStorageAsSingleton();

builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
// builder.Services.AddCascadingAuthenticationState();
builder.Services.AddSingleton<CustomAuthStateProvider>();
builder.Services.AddSingleton<AuthenticationStateProvider>(s => s.GetRequiredService<CustomAuthStateProvider>());

// builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddScoped<IPasswordManagerAPIConsumerService, PasswordManagerAPIConsumerService>();
builder.Services.AddScoped<CookieHandler>();
builder.Services.AddScoped<UnauthorizedDelegatingHandler>();

await builder.Build().RunAsync();
