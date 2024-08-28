using AutoMapper;
using Tastys.Domain;
using Tastys.Infrastructure;

namespace Tastys.Testing.BLL;

public class UserServiceUnitTest : IDisposable
{
    private TastysContext _context;
    private IMapper _mapper;
    private UserServices _userService;

    public UserServiceUnitTest()
    {
        _context = DependencyFactory.CreateInMemoryContext();
        _mapper = DependencyFactory.CreateMapper();
        _userService = new UserServices(_context, _mapper);
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    [Fact]
    public void PostUser_UsuarioValido_AgregaAlUsuario()
    {
        // Arrange
        var testUser = new Usuario()
        {
            Nombre = "Pepe",
            Email = "pepe@gmail.com",
            Auth0Id = "999",
            IsDeleted = false
        };

        // Act
        _userService.PostUser(testUser);

        // Assert
        Assert.True(_context.Usuarios.Any(user => user.Nombre == testUser.Nombre));
    }

    [Fact]
    public void AuthDeleteUser_AuthValida_EliminaAlUsuario()
    {
        // Arrange
        var testUser = new Usuario()
        {
            Nombre = "Pepe",
            Email = "pepe@gmail.com",
            Auth0Id = "999",
            IsDeleted = false
        };

        _context.Usuarios.Add(testUser);
        _context.SaveChanges();

        // Act
        _userService.AuthDeleteUser("999");

        // Assert
        var user = _context.Usuarios.FirstOrDefault(user => user.Nombre == testUser.Nombre);

        Assert.NotNull(user);
        Assert.True(user.IsDeleted);
    }
}