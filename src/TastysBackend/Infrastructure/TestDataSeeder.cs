using Tastys.Domain;

namespace Tastys.Infrastructure;

public class TestDataSeeder
{
    /// <summary>
    /// Carga datos de prueba a la DB y devuelve true.
    /// Si ya habían datos, no carga nada y devuelve false.
    /// </summary>
    public static bool SeedDataToContext(TastysContext context)
    {
        if (context.Usuarios.Any() || context.Recetas.Any() || context.Reviews.Any() || context.Categorias.Any())
            return false;

        var categorias = new Categoria[] {
            new()
            {
                Nombre = "Postres",
                ImgUrl = "https://content-cocina.lecturas.com/medio/2022/01/19/paso_a_paso_para_realizar_tarta_de_flan_con_galletas_y_chocolate_sin_azucar_resultado_final_1ce8842f_600x600.jpg"
            },
            new()
            {
                Nombre = "Recetas de abuela",
                ImgUrl = "https://editorialtelevisa.brightspotcdn.com/wp-content/uploads/2019/08/cocina-de-la-abuela.jpg"
            },
            new() {
                Nombre = "Comida mexicana",
                ImgUrl = "https://www.nortembio.nortem.info/wp-content/uploads/2020/04/guacamole-recetas-de-comida-mexicana-2.jpg"
            },
            new() {
                Nombre = "Picante",
                ImgUrl = "https://media.pilaradiario.com/p/8938527ffa98beeee75af1ba32383149/adjuntos/352/imagenes/100/100/0100100724/790x0/smart/f800x450-116112_167558_5050jpg.jpg"
            }
        };

        var ingredientes = new Ingrediente[] {
            new() { Nombre = "Carne de cerdo", Cantidad = "1 kg" },
            new() { Nombre = "Achiote en pasta", Cantidad = "1/2 taza" },
            new() { Nombre = "Jugo de piña", Cantidad = "1/4 taza" },
            new() { Nombre = "Azúcar", Cantidad = "1 taza" },
            new() { Nombre = "Harina", Cantidad = "1 taza" },
            new() { Nombre = "Aceite vegetal", Cantidad = "1 taza" },
            new() { Nombre = "Huevos", Cantidad = "4 unidades" }
        };

        var recetas = new Receta[] {
            new() {
                Nombre = "Tacos al Pastor",
                Descripcion =
                    "Los tacos al pastor son una deliciosa especialidad de la cocina mexicana, originaria de la Ciudad de México. Este plato se destaca por su carne de cerdo marinada, cocida en un trompo vertical, que se sirve en tortillas de maíz con una variedad de guarniciones frescas.",
                ImageUrl = "https://www.comedera.com/wp-content/uploads/2017/08/tacos-al-pastor-receta.jpg",
                TiempoCoccion = "30",  // Tiempo de cocción en minutos
                Ingredientes = new List<Ingrediente> { ingredientes[0], ingredientes[1], ingredientes[2] },
                Categorias = new List<Categoria> { categorias[2], categorias[3] },
                IsDeleted = false
            },
            new() {
                Nombre = "Torta cuatro cuartos sin manteca",
                Descripcion =
                    "La torta cuatro cuartos sin manteca es un pastel tradicional de la repostería que se caracteriza por su textura esponjosa y su sabor suave.",
                ImageUrl = "https://www.codigococina.com/wp-content/uploads/2019/07/receta_bizcocho_cuatro_cuartos_clasico.jpg",
                TiempoCoccion = "35",  // Tiempo de cocción en minutos
                Ingredientes = new List<Ingrediente> { ingredientes[3], ingredientes[4], ingredientes[5], ingredientes[6] },
                Categorias = new List<Categoria> { categorias[0], categorias[1] },
                IsDeleted = false
            }
        };

        var usuarios = new Usuario[] {
            new() {
                UsuarioID = 1,
                Nombre = "Mario Neta",
                Email = "mario.neta@example.com",
                Auth0Id = "auth0|1234567890",
                IsDeleted = false,
                create_at = DateTime.UtcNow,
                Recetas = new List<Receta> { recetas[0] }
            },
            new() {
                UsuarioID = 2,
                Nombre = "Elsa Botaje",
                Email = "elsa.frozen.98@outlook.com",
                Auth0Id = "auth0|0987654321",
                IsDeleted = false,
                create_at = DateTime.UtcNow,
                Recetas = new List<Receta> { recetas[1] }
            }
        };

        var reviews = new Review[] {
            new() {
                Comentario = "¡Salió muy buena!",
                Calificacion = 5,
                Usuario = usuarios[1],
                Receta = recetas[0]
            },
            new() {
                Comentario = "Muy ricos, pero muy picantes!",
                Calificacion = 4,
                Usuario = usuarios[0],
                Receta = recetas[1]
            }
        };

        context.Usuarios.AddRange(usuarios);
        context.Ingredientes.AddRange(ingredientes);
        context.Recetas.AddRange(recetas);
        context.Reviews.AddRange(reviews);
        context.SaveChanges();

        return true;
    }
}
