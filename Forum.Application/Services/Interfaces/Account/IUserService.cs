using Forum.Domain.Enums.Admin.Account;
using Forum.Domain.Models.Account;
using Forum.Domain.ViewModels.Account.UserPanel;
using Forum.Domain.ViewModels.Auth;

namespace Forum.Application.Services.Interfaces.Account;

public interface IUserService
{
    #region Regitser

    Task<RegisterResult> RegisterUser(RegisterViewModel register);

    #endregion

    #region Login

    Task<LoginResult> CheckUserForLogin(LoginViewModel model);

    #endregion

    #region Commons

    Task<User> GetUserByUserName(string username);
    Task<User> GetUserByUserId(long userid);
    Task SaveChangeAsync();
    Task UpdateUser(User user);

    #endregion

    #region UserPanel

    Task ChangeUserAvatar(string username, string fileName);
    Task<EditUserResult> EditUserProfile(EditUserViewModel model);
    Task<EditUserViewModel> FillEditUserViewModel(string username);

    #endregion

    #region Permission

    Task<bool> CheckUserPermission(string permissionName, long userId);
    Task<bool> CheckUserBanOrDeletedAccount(long userId);

    #endregion

    #region Admin Panel

    Task<List<User>> GetUserListForAdminPanel();
    Task<DeleteOrRecoveryAccount> DeleteOrRecoveryUserAccountFromAdminPanel(long userId);
    Task<BanOrNotBanAccount> BanOrNotBanAccountFromAdminPanel(long userId);

    #endregion
}