using Microsoft.AspNetCore.Components.WebAssembly.Http;
     
namespace LeoPasswordManagerUI.Handlers;
     
public class UnauthorizedDelegatingHandler : DelegatingHandler
{
    private readonly CustomAuthStateProvider customAuthStateProvider;

    public UnauthorizedDelegatingHandler(CustomAuthStateProvider customAuthStateProvider)
    {
        this.customAuthStateProvider = customAuthStateProvider;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var response = await base.SendAsync(request, cancellationToken);

        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            var currentUser = await customAuthStateProvider.GetCurrentUserAsync();

            if(currentUser != null)
            {
                await customAuthStateProvider.SetCurrentUserAsync(null);
            }
        }

        return response;
    }
}