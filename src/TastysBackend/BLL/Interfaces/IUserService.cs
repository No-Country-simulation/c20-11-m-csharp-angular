using Tastys.BLL;
using Tastys.Domain;
public interface IUserService
{
    public UsuarioPublicDto PostUser(Usuario usuario);
    public List<Usuario> GetAllUsers();
    public UsuarioPublicDto GetUserByEmail(string email);
    public UsuarioPublicDto AuthDeleteUser(string Auth0Id);
    public UsuarioPublicDto GetUserAuth0(UserDataToken userdata);
    public UsuarioPublicDto PutUser(Usuario usuario);
    public UsuarioPublicDto PostUserAuth0(UserDataToken userdata);
}