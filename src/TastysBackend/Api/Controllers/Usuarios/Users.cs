using Microsoft.AspNetCore.Mvc;
using Tastys.BLL;
using Tastys.Domain;
using Tastys.API.Middlewares;

namespace Tastys.Api.Controllers;

[ApiController]
[Route("users")]
public class UserController:ControllerBase
{
    private readonly UserServices _userService;
    public UserController(UserServices userService)
    {
        _userService = userService;
    }

    [HttpPost]
    [SetToken]
    [CheckToken]
    public ActionResult PostUsers([FromBody]Usuario user)
    {
        try
        {

            UsuarioPublicDto usuarioPublicDto = _userService.PostUser(user);

            return Ok(usuarioPublicDto);
            
        }
        catch (System.Exception)
        {

            throw;
        }
    }

    [HttpGet]
    public ActionResult GetUser([FromQuery]string email)
    {
        try
        {

            UsuarioPublicDto usuarioPublicDto= _userService.GetUserByEmail(email);

            return Ok(usuarioPublicDto);

        }
        catch (System.Exception)
        {
            throw;
        }
    }

    [HttpDelete]
    public ActionResult DeleteUser([FromQuery] string Auth0Id)
    {
        try
        {

            UsuarioPublicDto usuarioPublicDto= _userService.AuthDeleteUser(Auth0Id);

            return Ok(usuarioPublicDto);

        }
        catch (System.Exception)
        {
            
            throw;
        }
    }
    
    [HttpPut]
    public ActionResult PutUser([FromBody]Usuario user)
    {
        try
        {
            UsuarioPublicDto usuarioPublicDto= _userService.PutUser(user);

            return Ok(usuarioPublicDto);
        }
        catch (System.Exception)
        {
            
            throw;
        }
    }
}