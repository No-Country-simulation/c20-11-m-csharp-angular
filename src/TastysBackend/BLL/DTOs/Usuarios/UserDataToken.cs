using System.Text.Json.Serialization;
public class UserDataToken
{
    public string token { get; set; }
    public string authId { get; set; }
    public string email { get; set; }
    public string authName { get; set; }
}