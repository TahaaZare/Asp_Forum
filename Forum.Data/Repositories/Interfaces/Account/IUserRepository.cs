using Forum.Domain.Enums.Admin.Account;
using Forum.Domain.Models.Account;
using Forum.Domain.ViewModels.Account.UserPanel;

namespace Forum.Data.Repositories.Interfaces.Account;

public interface IUserRepository
{
    Task<User> GetUserByUserName(string username);
    Task<User> GetUserByUserId(long userid);
    Task<bool> IsExistsUserByUserName(string username);
    Task CreateUserAsync(User user);
    Task SaveChangeAsync();
    Task UpdateUser(User user);
    Task<bool> CheckUserHasPermission(string permissionName, long userId);

    #region UserPanel

    Task<bool> CheckNewUserName(string NewUsername);

    #endregion

    #region AdminPanel

    Task<List<User>> GetUserListForAdminPanel();
    Task<DeleteOrRecoveryAccount> DeleteOrRecoveryUserAccountFromAdminPanel(long userId);
    Task<BanOrNotBanAccount> BanOrNotBanAccountFromAdminPanel(long userId);

    #endregion
}