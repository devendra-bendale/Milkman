using Microsoft.EntityFrameworkCore;
using Milkman2.Data;
using Milkman2.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Milkman2.Features.LogIn
{
    public class LogInService
    {
        private readonly DataContext _context;
        private const string USER_SESSION_STORAGE_KEY = "user_account_session";
        private UserAccountSession? _userAccountSession;

        public LogInService(DataContext context)
        {
            _context = context;
        }

        public UserAccountSession? UserAccountSession => _userAccountSession;

        public async Task<UserAccountSession?> GetUserAccountSession()
        {
            UserAccountSession? _userAccountSession = null;

            var userAccountSessionJson = await SecureStorage.Default.GetAsync(USER_SESSION_STORAGE_KEY);
            if(!string.IsNullOrWhiteSpace(userAccountSessionJson))
                _userAccountSession = JsonSerializer.Deserialize<UserAccountSession>(userAccountSessionJson);

            return _userAccountSession;
        }

        public async Task SaveUserAccountSession(UserAccountSession userAccountSession)
        {
            await SecureStorage.Default.SetAsync(USER_SESSION_STORAGE_KEY, JsonSerializer.Serialize(userAccountSession));
        }

        public void RemoveUserAccountSession()
        {
            SecureStorage.Default.Remove(USER_SESSION_STORAGE_KEY);
            _userAccountSession = null;
        }

        public async Task<UserAccount> ValidateUserCredentials(string username, string password)
        {
            var userAccount = await _context.UserAccounts
                .FirstOrDefaultAsync(u => u.UserName == username && u.UserPassword == password);
            if (userAccount != null)
            {
                _userAccountSession = new UserAccountSession
                {
                    UserId = userAccount.Id,
                    UserName = userAccount.UserName
                };
                return userAccount;
            }
            return null;
        }
    }
}
