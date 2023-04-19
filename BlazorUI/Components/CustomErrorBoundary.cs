using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace BlazorUI.Components;

public class CustomErrorBoundary : ErrorBoundary
{
    [Inject] public IWebAssemblyHostEnvironment Environment { get; set; } = default!;
    
    protected override Task OnErrorAsync(Exception exception)
    {
        // return Environment.IsDevelopment() 
        return Environment.IsProduction() 
            ? base.OnErrorAsync(exception) 
            : Task.CompletedTask;

    }
}