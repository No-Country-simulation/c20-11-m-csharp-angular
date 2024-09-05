
using System.Net;
using AutoMapper;
using Tastys.BLL;
using Tastys.BLL.Utils;
using Tastys.Domain;

public class UserServices:IUserService
{
    private readonly ITastysContext _userService;
    private readonly IMapper _mapper;
    public UserServices(ITastysContext tastysContext,IMapper mapper)
    {
        _userService = tastysContext;
        _mapper = mapper;
    }
    public List<Usuario> GetAllUsers()
    {
        try
        {
            List<Usuario> usuarios = _userService.Usuarios.Where(u => u.IsDeleted == false).ToList();

            return usuarios;
        }
        catch (System.Exception)
        {
            
            throw;
        }
    }
    public UsuarioPublicDto PostUserAuth0(UserDataToken userData)
    {
        try
        {
            
            Usuario usuarioExist = _userService.Usuarios.FirstOrDefault( u => u.Auth0Id == userData.authId);

            if (usuarioExist == null)
            {
                Usuario newUsuario = new Usuario {
                    Auth0Id = userData.authId,
                    Email = userData.email,
                    Nombre = userData.authName
                };
                _userService.Usuarios.Add(newUsuario);
                _userService.SaveChanges();

                return _mapper.Map<UsuarioPublicDto>(usuarioExist);
            }else
            {
                throw new HttpRequestException("El usuario ya existe en la db",null,HttpStatusCode.BadRequest);
            }
        }
        catch (System.Exception)
        {
            
            throw;
        }
    } 
    public UsuarioPublicDto PostUser(Usuario usuario)
    {
        try
        {
            Usuario usuarioExist = _userService.Usuarios.FirstOrDefault( u => u.Auth0Id == usuario.Auth0Id);

            if (usuarioExist == null)
            {
                _userService.Usuarios.Add(usuario);
                _userService.SaveChanges();

                return _mapper.Map<UsuarioPublicDto>(usuarioExist);
            }else
            {
                throw new HttpRequestException("El usuario ya existe en la db",null,HttpStatusCode.BadRequest);
            }
        }
        catch (System.Exception)
        {
            
            throw;
        }
    } 
    public UsuarioPublicDto GetUserByEmail(string email)
    {
        try
        {

            Usuario usuarioExist = _userService.Usuarios.FirstOrDefault( u => u.Email == email);

            if (usuarioExist != null)
            {
                return _mapper.Map<UsuarioPublicDto>(usuarioExist);
            }else
            {
                throw new HttpRequestException("El usuario no existe en la db",null,HttpStatusCode.BadRequest);
            }
        }
        catch (System.Exception)
        {
            
            throw;
        }
    }    
    public UsuarioPublicDto GetUserAuth0(UserDataToken userdata)
    {
        try
        {

            Dictionary<string,string> claims = JwtValidate.ValidateClaimsToken(userdata.token,["custom_email_claim"]);

            Usuario usuarioExist = _userService.Usuarios.FirstOrDefault( u => u.Email == claims["custom_email_claim"]);

            if (usuarioExist != null)
            {
                return _mapper.Map<UsuarioPublicDto>(usuarioExist);
            }else
            {
                throw new HttpRequestException("El usuario no existe en la db",null,HttpStatusCode.BadRequest);
            }
        }
        catch (System.Exception)
        {
            
            throw;
        }
    }  
    public UsuarioPublicDto AuthDeleteUser(string Auth0Id)
    {
        try
        {
            Usuario usuarioExist = _userService.Usuarios.FirstOrDefault( u => u.Auth0Id == Auth0Id);

            if (usuarioExist != null)
            {
                usuarioExist.IsDeleted = true;
                _userService.SaveChanges();

                return _mapper.Map<UsuarioPublicDto>(usuarioExist);

            }else
            {
                throw new HttpRequestException("El usuario no existe en la db",null,HttpStatusCode.BadRequest);
            }
        }
        catch (System.Exception)
        {
            
            throw;
        }
    }    
    public UsuarioPublicDto PutUser(Usuario usuario)
    {
        try
        {
            Usuario usuarioExist = _userService.Usuarios.FirstOrDefault( u => u.Auth0Id == usuario.Auth0Id);

            if (usuarioExist != null)
            {
                usuarioExist = usuario;
                _userService.SaveChanges();

                return _mapper.Map<UsuarioPublicDto>(usuarioExist);
            }else
            {
                throw new HttpRequestException("El usuario no existe en la db",null,HttpStatusCode.BadRequest);
            }
        }
        catch (System.Exception)
        {
            
            throw;
        }
    }
    
}