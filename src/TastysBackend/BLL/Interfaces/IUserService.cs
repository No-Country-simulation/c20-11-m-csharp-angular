using Tastys.BLL;
using Tastys.Domain;
public interface IUserService
{
    public UsuarioPublicDto PostUser(Usuario usuario);
    public UsuarioPublicDto GetUserByEmail(string email);
    public UsuarioPublicDto AuthDeleteUser(string Auth0Id);
    public UsuarioPublicDto PutUser(Usuario usuario);
}