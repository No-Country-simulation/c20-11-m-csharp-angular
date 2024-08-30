using Tastys.BLL;
using Tastys.Domain;
public interface IUserService
{
    public UsuarioPublicDto PostUser(Usuario usuario);
    public UsuarioPublicDto GetUserByEmail(string email);
    public UsuarioPublicDto AuthDeleteUser(string Auth0Id);
    public UsuarioPublicDto GetUserAuth0(string token);
    public UsuarioPublicDto PutUser(Usuario usuario);
    public UsuarioPublicDto PostUserAuth0(string token);
}