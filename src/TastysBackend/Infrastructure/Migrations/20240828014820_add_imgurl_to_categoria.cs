using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tastys.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add_imgurl_to_categoria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImgUrl",
                table: "Categorias",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImgUrl",
                table: "Categorias");
        }
    }
}
