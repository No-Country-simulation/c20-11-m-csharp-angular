public class SetCookie
{
    public static CookieOptions Config(float expira = 10)
    {
        return new CookieOptions
        {
            HttpOnly = true,
            Secure = true,  // En desarrollo sin HTTPS
            SameSite = SameSiteMode.None,
            Expires = DateTimeOffset.UtcNow.AddHours(expira)
        };
    }
}