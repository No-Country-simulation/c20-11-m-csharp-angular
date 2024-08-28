using Tastys.BLL;
using Tastys.Domain;
public interface IUserService
{
    public UsuarioPublicDto PostUser(Usuario usuario);
    public UsuarioPublicDto GetUser(Usuario usuario);
    public UsuarioPublicDto DeleteUser(Usuario usuario);
    public UsuarioPublicDto PutUser(Usuario usuario);
    public Usuario AuthGetUserByEmail(string email);
}