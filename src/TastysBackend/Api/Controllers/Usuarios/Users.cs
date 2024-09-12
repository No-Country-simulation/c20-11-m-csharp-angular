using Microsoft.AspNetCore.Mvc;
using Tastys.BLL;
using Tastys.Domain;
using Tastys.API.Middlewares;

namespace Tastys.Api.Controllers;

[ApiController]
[Route("users")]
public class UserController : ControllerBase
{
    private readonly UserServices _userService;
    public UserController(UserServices userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    [SetToken]
    [CheckToken]
    public ActionResult PostUsers()
    {
        try
        {
            if (HttpContext.Items["userdata"] is not UserDataToken userData)
            {
                return BadRequest("No se encontró información del usuario.");
            }

            UsuarioPublicDto usuarioPublicDto = _userService.PostUserAuth0(userData);

            return Ok(usuarioPublicDto);

        }
        catch (System.Exception e)
        {
            throw new Exception("Error en la peticion:" + e);
        }
    }
    [HttpGet("all")]
    public ActionResult GetUser()
    {
        try
        {

            List<Usuario> usuarioPublicDto = _userService.GetAllUsers();

            return Ok(usuarioPublicDto);

        }
        catch (System.Exception e)
        {
            throw new Exception("Error en la peticion:" + e.Message);
        }
    }
    [HttpGet("email")]
    [CheckToken]
    [CheckPermissions("user:user")]
    public ActionResult GetUser([FromQuery] string email)
    {
        try
        {

            UsuarioPublicDto usuarioPublicDto = _userService.GetUserByEmail(email);

            return Ok(usuarioPublicDto);

        }
        catch (System.Exception e)
        {
            throw new Exception("Error en la peticion:" + e.Message);
        }
    }

    [HttpGet]
    [SetToken]
    [CheckToken]
    public ActionResult GetUserAuth()
    {
        try
        {
            UserDataToken userData = (UserDataToken)HttpContext.Items["userdata"];
            if (userData != null)
            {
                UsuarioPublicDto usuarioPublicDto = _userService.GetUserAuth0(userData);

                return Ok(usuarioPublicDto);
            }
            else
            {
                return BadRequest("No se proporciono el token para obtener el usuairo");
            }


        }
        catch (System.Exception e)
        {
            throw new Exception("Error en la peticion:" + e.Message);
        }
    }
    [HttpDelete]
    public ActionResult DeleteUser([FromQuery] string Auth0Id)
    {
        try
        {

            UsuarioPublicDto usuarioPublicDto = _userService.AuthDeleteUser(Auth0Id);

            return Ok(usuarioPublicDto);

        }
        catch (System.Exception e)
        {
            throw new Exception("Error en la peticion:" + e.Message);
        }
    }

    [HttpPut]
    public ActionResult PutUser([FromBody] Usuario user)
    {
        try
        {
            UsuarioPublicDto usuarioPublicDto = _userService.PutUser(user);

            return Ok(usuarioPublicDto);
        }
        catch (System.Exception e)
        {
            throw new Exception("Error en la peticion:" + e.Message);
        }
    }
}