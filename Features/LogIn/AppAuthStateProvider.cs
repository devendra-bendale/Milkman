using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Milkman2.Features.LogIn
{
    public class AppAuthStateProvider : AuthenticationStateProvider
    {
        private const string AuthType = "app-auth";
        private const string StorageKey = "auth";

        private readonly static Task<AuthenticationState> _emptyAuthStateTask =
            Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));

        private Task<AuthenticationState> _currentAuthStateTask = _emptyAuthStateTask;
        public AppAuthStateProvider()
        {
            AuthenticationStateChanged += AppAuthStateProvider_AuthenticationStateChanged;
        }

        public string? Token { get; private set; }

        public LoggedInUser? CurrentUser { get; private set; }

        public bool IsLoggedIn => CurrentUser is not null;

        private async void AppAuthStateProvider_AuthenticationStateChanged(Task<AuthenticationState> task)
        {
            var authState = await task;
            if (authState.User.Identity?.IsAuthenticated is true)
            {
                // User is logged in
                // Add your code if you want to do something in this case
            }
            else
            {
                // User is logged out
                // Add your code if you want to do something in this case 
            }
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            // Initial Auth State
            // Get Auth State at App Initialization
            // Test Condition added to handle restricted access after 31st July 2026
            if (DateTime.Now <= new DateTime(2026, 07, 31))
            {
                var serializedLoginResponse = Preferences.Default.Get<string?>(StorageKey, null);
                if (serializedLoginResponse is not null)
                {
                    // We have login response in the preferences/storage
                    // Parse it and use it
                    var loginResponse = JsonSerializer.Deserialize<LoginResponseDto>(serializedLoginResponse)!;
                    Login(loginResponse, saveToStorage: false);
                    return _currentAuthStateTask;
                }
                return _emptyAuthStateTask;
            }
            else { 
                Logout();
                return _emptyAuthStateTask;
            }
        }

        public void Login(LoginResponseDto loginResponse, bool saveToStorage = true)
        {
            Token = loginResponse.Token;
            CurrentUser = loginResponse.User;

            var claims = loginResponse.User.ToClaims();
            var identity = new ClaimsIdentity(claims, AuthType);
            var principal = new ClaimsPrincipal(identity);
            var authState = new AuthenticationState(principal);

            _currentAuthStateTask = Task.FromResult(authState);

            NotifyAuthenticationStateChanged(_currentAuthStateTask);

            if (saveToStorage)
                Preferences.Default.Set<string>(StorageKey, JsonSerializer.Serialize(loginResponse));
        }

        public void Logout()
        {
            Token = null;
            CurrentUser = null;

            _currentAuthStateTask = _emptyAuthStateTask;
            NotifyAuthenticationStateChanged(_currentAuthStateTask);

            Preferences.Default.Remove(StorageKey);
        }
    }
}
