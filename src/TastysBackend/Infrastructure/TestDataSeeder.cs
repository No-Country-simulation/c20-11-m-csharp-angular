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

        var recetas = new Receta[] {
            new() {
                Nombre = "Tacos al Pastor",
                Descripcion =
                    "Los tacos al pastor son una deliciosa especialidad de la cocina mexicana, originaria de la Ciudad de México. Este plato se destaca por su carne de cerdo marinada, cocida en un trompo vertical, que se sirve en tortillas de maíz con una variedad de guarniciones frescas. La combinación de sabores es un festín para los sentidos, con un toque ligeramente picante y un sabor ahumado que proviene del proceso de cocción. Los tacos al pastor son perfectos para disfrutar en una comida informal o en una celebración.\n\n" +
                    "Ingredientes:\n" +
                    " - 1 kg de carne de cerdo (espaldilla o pierna), cortada en tiras delgadas\n" +
                    " - 1/2 taza de achiote en pasta\n" +
                    " - 1/4 taza de jugo de piña\n" +
                    " - 1/4 taza de vinagre blanco\n" +
                    " - 3 dientes de ajo, picados\n" +
                    " - 1 cucharadita de comino en polvo\n" +
                    " - 1 cucharadita de orégano seco\n" +
                    " - 1 cucharadita de pimentón (paprika)\n" +
                    " - 1/2 cucharadita de chile en polvo\n" +
                    " - Sal y pimienta al gusto\n" +
                    " - Tortillas de maíz\n" +
                    " - Cebolla picada\n" +
                    " - Cilantro fresco picado\n" +
                    " - Limones cortados en gajos\n" +
                    " - Salsa al gusto\n\n" +
                    "Instrucciones:\n" +
                    "1. En un bol grande, mezclar la pasta de achiote, jugo de piña, vinagre, ajo, comino, orégano, pimentón, chile en polvo, sal y pimienta hasta obtener una marinada uniforme.\n" +
                    "2. Agregar las tiras de carne a la marinada y mezclar bien para que queden completamente cubiertas. Cubrir el bol y marinar en el refrigerador durante al menos 2 horas, preferiblemente toda la noche.\n" +
                    "3. Precalentar una parrilla o sartén a fuego medio-alto. Cocinar las tiras de carne hasta que estén doradas y bien cocidas, aproximadamente 8-10 minutos por lado.\n" +
                    "4. Calentar las tortillas de maíz en un comal o sartén.\n" +
                    "5. Servir la carne en las tortillas calientes y agregar cebolla, cilantro, salsa y un chorrito de jugo de limón al gusto.\n" +
                    "6. Disfrutar de los tacos al pastor calientes, acompañados de salsa y limón.\n",
                ImageUrl = "https://www.comedera.com/wp-content/uploads/2017/08/tacos-al-pastor-receta.jpg",
                Categorias = [ categorias[2], categorias[3] ],
                IsDeleted = false
            },
            new() {
                Nombre = "Torta cuatro cuartos sin manteca",
                Descripcion =
                    "La torta cuatro cuartos sin manteca es un pastel tradicional de la repostería que se caracteriza por su textura esponjosa y su sabor suave. A diferencia de otras versiones que utilizan manteca, esta receta sustituye la manteca por aceite, ofreciendo una opción más ligera sin comprometer el sabor. Ideal para acompañar una taza de té o café, esta torta es perfecta para cualquier ocasión, ya sea una merienda informal o una celebración especial.\n\n" +
                    "Ingredientes:\n" +
                    " - 1 taza de azúcar\n" +
                    " - 1 taza de harina\n" +
                    " - 1 taza de aceite vegetal\n" +
                    " - 4 huevos\n" +
                    " - 1 cucharadita de polvo de hornear\n" +
                    " - 1 cucharadita de esencia de vainilla\n" +
                    " - Una pizca de sal\n" +
                    " - Ralladura de un limón (opcional)\n\n" +
                    "Instrucciones:\n" +
                    "1. Precalentar el horno a 180°C (350°F).\n" +
                    "2. En un bol grande, batir los huevos con el azúcar hasta obtener una mezcla cremosa y homogénea.\n" +
                    "3. Agregar el aceite y la esencia de vainilla, y mezclar bien.\n" +
                    "4. Tamizar la harina con el polvo de hornear y la sal, e incorporarlo gradualmente a la mezcla de huevos y azúcar, batiendo a baja velocidad.\n" +
                    "5. Si se desea, añadir la ralladura de limón para un toque de sabor adicional.\n" +
                    "6. Verter la masa en un molde enharinado y enmantecado.\n" +
                    "7. Hornear durante 30-35 minutos, o hasta que al insertar un palillo en el centro de la torta, éste salga limpio.\n" +
                    "8. Dejar enfriar antes de desmoldar y servir.\n",
                ImageUrl = "https://www.codigococina.com/wp-content/uploads/2019/07/receta_bizcocho_cuatro_cuartos_clasico.jpg",
                Categorias = [ categorias[0], categorias[1] ],
                IsDeleted = false,
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
                Recetas = [ recetas[0] ]
            },
            new() {
                UsuarioID = 2,
                Nombre = "Elsa Botaje",
                Email = "elsa.frozen.98@outlook.com",
                Auth0Id = "auth0|0987654321",
                IsDeleted = false,
                create_at = DateTime.UtcNow,
                Recetas = [ recetas[1] ]
            }
        };

        var reviews = new Review[] {
            new() {
                Comentario = "¡Salió muy buena!",
                Calificacion = 5
            },
            new() {
                Comentario = "Muy ricos, pero muy picantes!",
                Calificacion = 4
            }
        };

        recetas[0].Usuario = usuarios[0];
        recetas[1].Usuario = usuarios[1];

        reviews[0].Usuario = usuarios[1];
        reviews[0].Receta = recetas[0];
        reviews[1].Usuario = usuarios[0];
        reviews[1].Receta = recetas[1];

        context.Usuarios.AddRange(usuarios);
        context.Reviews.AddRange(reviews);
        context.SaveChanges();

        return true;
    }
}
