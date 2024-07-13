namespace Forum.Application.Statics;

public static class Paths
{
    #region User

    public static readonly string UserAvatarServerPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/user/");
    public static readonly string UserAvatarPath = "/images/user/";

    #endregion
    
    #region Banner

    public static readonly string BannerServerPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/banner/");
    public static readonly string BannerPath = "/images/banner/";

    #endregion

    #region Site

    public static readonly string SiteAddress = "https://localhost:44372";

    #endregion
}