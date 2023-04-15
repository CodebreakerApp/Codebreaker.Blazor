using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeBreaker.UI.Shared.Services.JavaScript;
using Microsoft.JSInterop;

namespace CodeBreaker.UI.Services.Cache;
public class CodeBreakerCacheStorageAccessor : JSModule
{
    public CodeBreakerCacheStorageAccessor(IJSRuntime js)
        : base(js, "./_content/CodeBreaker.UI/js/CacheStorageAccessor.js")
    {
    }

    public async ValueTask PutAsync(HttpRequestMessage requestMessage, HttpResponseMessage responseMessage)
    {
        string requestMethod = requestMessage.Method.Method;
        string requestBody = await GetRequestBodyAsync(requestMessage);
        string responseBody = await responseMessage.Content.ReadAsStringAsync();

        await InvokeVoidAsync("put", requestMessage.RequestUri!, requestMethod, requestBody, responseBody);
    }

    public async ValueTask<string> PutAndGetAsync(HttpRequestMessage requestMessage, HttpResponseMessage responseMessage)
    {
        string requestMethod = requestMessage.Method.Method;
        string requestBody = await GetRequestBodyAsync(requestMessage);
        string responseBody = await responseMessage.Content.ReadAsStringAsync();

        await InvokeVoidAsync("put", requestMessage.RequestUri!, requestMethod, requestBody, responseBody);

        return responseBody;
    }

    public async ValueTask<string> GetAsync(HttpRequestMessage requestMessage)
    {
        string requestMethod = requestMessage.Method.Method;
        string requestBody = await GetRequestBodyAsync(requestMessage);
        string result = await InvokeAsync<string>("get", requestMessage.RequestUri!, requestMethod, requestBody);

        return result;
    }

    public async ValueTask RemoveAsync(HttpRequestMessage requestMessage)
    {
        string requestMethod = requestMessage.Method.Method;
        string requestBody = await GetRequestBodyAsync(requestMessage);

        await InvokeVoidAsync("remove", requestMessage.RequestUri!, requestMethod, requestBody);
    }

    public async ValueTask RemoveAllAsync()
    {
        await InvokeVoidAsync("removeAll");
    }
    private static async ValueTask<string> GetRequestBodyAsync(HttpRequestMessage requestMessage)
    {
        string requestBody = string.Empty;
        if (requestMessage.Content is not null)
        {
            requestBody = await requestMessage.Content.ReadAsStringAsync();
        }

        return requestBody;
    }
}
