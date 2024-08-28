
using System.Net;
using AutoMapper;
using Tastys.BLL;
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