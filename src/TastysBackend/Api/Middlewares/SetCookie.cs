public class SetCookie
{
    public static CookieOptions Config(float expira = 10)
    {
        return new CookieOptions{
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTimeOffset.UtcNow.AddHours(expira)
        };
    }
}