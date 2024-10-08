using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BeverageAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Beverages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Size = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beverages", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Beverages",
                columns: new[] { "Id", "Description", "Name", "Price", "Size" },
                values: new object[,]
                {
                    { 1, "Refreshing lemon-lime soda", "Faxe Kondi", 15.99m, 6 },
                    { 2, "Classic Danish pilsner beer", "Ceres Top", 20.50m, 2 },
                    { 3, "Light and crisp pilsner beer", "Royal Pilsner", 18.00m, 2 },
                    { 4, "Strong lager with a rich taste", "Royal Export", 19.50m, 3 },
                    { 5, "Smooth, amber lager", "Royal Classic", 17.50m, 2 },
                    { 6, "Organic pilsner with a mild taste", "Royal Økologisk", 22.00m, 1 },
                    { 7, "Energy drink with a lemon-lime flavor", "Faxe Kondi Booster", 12.99m, 5 },
                    { 8, "Sparkling water with a hint of citrus", "Egekilde Citrus", 10.00m, 1 },
                    { 9, "Sparkling water with elderflower taste", "Egekilde Elderflower", 10.00m, 1 },
                    { 10, "Full-bodied dark lager", "Albani Odense Classic", 18.50m, 2 },
                    { 11, "Strong beer with a distinct flavor", "Albani Giraf Beer", 24.00m, 4 },
                    { 12, "Hoppy IPA with tropical notes", "Albani Mosaic IPA", 25.00m, 2 },
                    { 13, "Sugar-free version of the classic soda", "Faxe Kondi Free", 15.99m, 5 },
                    { 14, "Sugar-free cola", "Pepsi Max", 16.50m, 6 },
                    { 15, "Rich, full-bodied lager", "Royal Beer", 18.99m, 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Beverages");
        }
    }
}
