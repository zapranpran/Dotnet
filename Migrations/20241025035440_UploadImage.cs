using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectCatalog.Migrations
{
    /// <inheritdoc />
    public partial class UploadImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageProduk",
                table: "Produk",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageProduk",
                table: "Produk");
        }
    }
}
