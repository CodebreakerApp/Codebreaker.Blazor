using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace CodeBreaker.Blazor.Pages
{
    public record UserInformation(string FirstName, string LastName, string UserName, string GamerName);
    public partial class UserInformationPage
    {
        [Inject] private AuthenticationStateProvider _authenticationStateProvider { get; set; } = default!;

        private UserInformation? _user;

        protected override async Task OnInitializedAsync()
        {
            _user = await GetClaimsPrincipalData();
            await base.OnInitializedAsync();
        }

        private async Task<UserInformation> GetClaimsPrincipalData()
        {
            var authState = await _authenticationStateProvider
                .GetAuthenticationStateAsync();
            var user = authState.User;
            return new UserInformation(
                authState.User.Claims.FirstOrDefault(claim => claim.Type == "given_name")?.Value ?? "----",
                authState.User.Claims.FirstOrDefault(claim => claim.Type == "family_name")?.Value ?? "----",
                authState.User.Claims.FirstOrDefault(claim => claim.Type == "name")?.Value ?? "----",
                authState.User.Claims.FirstOrDefault(claim => claim.Type == "extension_GamerName")?.Value ?? "----"
            );
        }
    }
}
