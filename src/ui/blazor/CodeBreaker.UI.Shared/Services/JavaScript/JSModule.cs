using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace CodeBreaker.UI.Shared.Services.JavaScript;

public abstract class JSModule : IAsyncDisposable
{
    private readonly Task<IJSObjectReference> moduleTask;

    // On construction, we start loading the JS module
    protected JSModule(IJSRuntime js, string moduleUrl)
        => moduleTask = js.InvokeAsync<IJSObjectReference>("import", moduleUrl).AsTask();

    // Methods for invoking exports from the module
    protected async ValueTask InvokeVoidAsync(string identifier, params object[]? args)
        => await (await moduleTask).InvokeVoidAsync(identifier, args);
    protected async ValueTask<T> InvokeAsync<T>(string identifier, params object[]? args)
        => await (await moduleTask).InvokeAsync<T>(identifier, args);

    // On disposal, we release the JS module
    public async ValueTask DisposeAsync()
        => await (await moduleTask).DisposeAsync();
}
