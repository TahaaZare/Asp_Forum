using Forum.Application.Security;
using Forum.Application.Services.Interfaces.Account;
using Forum.Data.Repositories.Interfaces.Account;
using Forum.Domain.Enums.Admin.Account;
using Forum.Domain.Models.Account;
using Forum.Domain.ViewModels.Account.UserPanel;
using Forum.Domain.ViewModels.Auth;

namespace Forum.Application.Services.Implementations.Account;

public class UserService : IUserService
{
    #region ctor

    private IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    #endregion

    #region Register

    public async Task<RegisterResult> RegisterUser(RegisterViewModel register)
    {
        // Check Username Exists
        if (await _userRepository.IsExistsUserByUserName(register.Username.SanitizeText().Trim().Replace(" ", "-")
                .ToLower()))
        {
            return RegisterResult.UserNameExist;
        }

        // hash password
        var password = PasswordHelper.EncodePasswordMd5(register.Password.SanitizeText());

        // create user
        var user = new User()
        {
            Username = register.Username.Trim().ToLower(),
            Password = password,
        };

        #region gravavatar

        string hash = HashMD5.MD5Hash(user.Username);
        string gravatarUrl = $"https://www.gravatar.com/avatar/{hash}?d=identicon";
        user.Avatar = gravatarUrl;

        #endregion

        // Add To database
        await _userRepository.CreateUserAsync(user);
        await _userRepository.SaveChangeAsync();

        return RegisterResult.Success;
    }

    #endregion

    #region Login

    public async Task<LoginResult> CheckUserForLogin(LoginViewModel model)
    {
        var user = await _userRepository.GetUserByUserName(model.Username.SanitizeText().Trim().ToLower());
        if (user == null)
        {
            return LoginResult.NotFound;
        }

        var hashedPassword = PasswordHelper.EncodePasswordMd5(model.Password.SanitizeText());
        if (hashedPassword != user.Password)
        {
            return LoginResult.NotFound;
        }

        if (user.Deleted_at != null) return LoginResult.NotFound;
        if (user.Ban == true) return LoginResult.Ban;

        return LoginResult.Success;
    }

    #endregion

    #region Commons

    public async Task<User> GetUserByUserName(string username)
    {
        var user = await _userRepository.GetUserByUserName(username);
        return user;
    }

    public async Task<User> GetUserByUserId(long userid)
    {
        return await _userRepository.GetUserByUserId(userid);
    }


    public async Task SaveChangeAsync()
    {
        await _userRepository.SaveChangeAsync();
    }

    public async Task UpdateUser(User user)
    {
        await _userRepository.UpdateUser(user);
    }

    #endregion

    #region UserPanel

    public async Task ChangeUserAvatar(string username, string fileName)
    {
        var user = await _userRepository.GetUserByUserName(username);
        user.Avatar = fileName;
        await _userRepository.UpdateUser(user);
        await _userRepository.SaveChangeAsync();
    }


    public async Task<EditUserResult> EditUserProfile(EditUserViewModel model)
    {
        var user = await _userRepository.GetUserByUserName(model.Username);
        if (model.Username != model.NewUsername && await _userRepository.CheckNewUserName(model.NewUsername))
        {
            return EditUserResult.UserNameExists;
        }

        user.Username = model.Username;
        user.Bio = model.Bio;
        user.Display_Name = model.Display_Name;

        await this.UpdateUser(user);
        await this.SaveChangeAsync();

        return EditUserResult.Success;
    }

    public async Task<EditUserViewModel> FillEditUserViewModel(string username)
    {
        var user = await this.GetUserByUserName(username);
        var result = new EditUserViewModel()
        {
            Username = username,
            Bio = user.Bio,
            Display_Name = user.Display_Name
        };

        return result;
    }

    #endregion

    #region Permission

    public async Task<bool> CheckUserPermission(string permissionName, long userId)
    {
        var user = await _userRepository.GetUserByUserId(userId);
        if (user == null)
        {
            return false;
        }

        if (user.Admin)
        {
            return true;
        }

        if (!await _userRepository.CheckUserHasPermission(permissionName, userId))
        {
            return false;
        }

        return true;
    }

    public async Task<bool> CheckUserBanOrDeletedAccount(long userId)
    {
        var user = await _userRepository.GetUserByUserId(userId);
        if (user == null) return true;

        if (user.Ban || user.Deleted_at != null)
        {
            return true;
        }

        return false;
    }

    #endregion


    #region Admin Panel

    public async Task<List<User>> GetUserListForAdminPanel()
    {
        return await _userRepository.GetUserListForAdminPanel();
    }

    public async Task<DeleteOrRecoveryAccount> DeleteOrRecoveryUserAccountFromAdminPanel(long userId)
    {
        return await _userRepository.DeleteOrRecoveryUserAccountFromAdminPanel(userId);
    }

    public async Task<BanOrNotBanAccount> BanOrNotBanAccountFromAdminPanel(long userId)
    {
        return await _userRepository.BanOrNotBanAccountFromAdminPanel(userId);
    }

    #endregion
}