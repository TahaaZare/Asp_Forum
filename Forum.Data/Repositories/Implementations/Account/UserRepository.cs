using Forum.Data.Context;
using Forum.Data.Repositories.Interfaces.Account;
using Forum.Domain.Enums.Admin.Account;
using Forum.Domain.Models.Account;
using Forum.Domain.ViewModels.Account.UserPanel;
using Microsoft.EntityFrameworkCore;

namespace Forum.Data.Repositories.Implementations.Account;

public class UserRepository : IUserRepository
{
    #region ctor

    private readonly ForumDbContext _context;

    public UserRepository(ForumDbContext context)
    {
        _context = context;
    }

    #endregion


    public async Task<User> GetUserByUserName(string username)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username && u.Deleted_at == null);
        return user;
    }

    public async Task<User> GetUserByUserId(long userid)
    {
        var user = await _context.Users.FindAsync(userid);
        return user;
    }

    public async Task<bool> IsExistsUserByUserName(string username)
    {
        return await _context.Users.AnyAsync(u => u.Username == username);
    }

    public async Task CreateUserAsync(User user)
    {
        await _context.AddAsync(user);
    }

    public async Task SaveChangeAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task UpdateUser(User user)
    {
        _context.Update(user);
    }

    public async Task<bool> CheckUserHasPermission(string permissionName, long userId)
    {
        var permission = await _context.Permissions.FirstOrDefaultAsync(s => s.Title == permissionName);
        if (permission == null)
        {
            return false;
        }

        var user = await GetUserByUserId(userId);
        if (user == null)
        {
            return false;
        }

        if (user.Admin)
        {
            return true;
        }

        var userPermission =
            await _context.UserPermissions.FirstOrDefaultAsync(s =>
                s.Permission_id == permission.Id && s.User_Id == user.Id);

        if (userPermission == null)
        {
            return false;
        }

        return true;
    }

    #region UserPanel

    public async Task<bool> CheckNewUserName(string NewUsername)
    {
        var findUser = await this.GetUserByUserName(NewUsername);
        if (findUser != null)
        {
            return false;
        }

        return true;
    }

    #endregion

    #region Admin Panel

    public async Task<List<User>> GetUserListForAdminPanel()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<DeleteOrRecoveryAccount> DeleteOrRecoveryUserAccountFromAdminPanel(long userId)
    {
        var user = await GetUserByUserId(userId);
        if (user == null) return DeleteOrRecoveryAccount.UserNotFound;

        if (user.Ban) return DeleteOrRecoveryAccount.UserIsBan;

        if (user.Deleted_at != null)
        {
            user.Deleted_at = null;
            await UpdateUser(user);
            await SaveChangeAsync();
            return DeleteOrRecoveryAccount.RecoverySuccess;
        }
        else
        {
            user.Deleted_at = DateTime.Now;
            await UpdateUser(user);
            await SaveChangeAsync();
            return DeleteOrRecoveryAccount.DeletedSuccess;
        }
    }

    public async Task<BanOrNotBanAccount> BanOrNotBanAccountFromAdminPanel(long userId)
    {
        var user = await GetUserByUserId(userId);
        if (user == null) return BanOrNotBanAccount.UserNotFound;

        if (user.Deleted_at != null) return BanOrNotBanAccount.UserDeletedAccount;

        if (user.Ban)
        {
            user.Ban = false;
            await UpdateUser(user);
            await SaveChangeAsync();
            return BanOrNotBanAccount.DisBan;
        }
        else
        {
            user.Ban = true;
            await UpdateUser(user);
            await SaveChangeAsync();
            return BanOrNotBanAccount.SuccessBan;
        }
    }

    #endregion
}