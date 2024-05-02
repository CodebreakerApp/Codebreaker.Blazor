namespace CodeBreaker.Blazor.Client.Contracts.Services;

public interface IMobileDetectorService
{
    ValueTask<bool> IsMobileAsync();
}
