public class SetCookie
{
    public static CookieOptions Config(float expira = 10)
    {
        return new CookieOptions
        {
            HttpOnly = true,
            Secure = false,  // En desarrollo sin HTTPS
            SameSite = SameSiteMode.Lax,
            Expires = DateTimeOffset.UtcNow.AddHours(expira)
        };
    }
}