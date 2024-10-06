using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "ISBN", "Price", "Title" },
                values: new object[,]
                {
                    { 1, "J.D. Salinger", "9780316769488", 10.99m, "The Catcher in the Rye" },
                    { 2, "George Orwell", "9780451524935", 9.99m, "1984" },
                    { 3, "Harper Lee", "9780060935467", 14.99m, "To Kill a Mockingbird" },
                    { 4, "Jane Austen", "9781503290563", 8.99m, "Pride and Prejudice" },
                    { 5, "F. Scott Fitzgerald", "9780743273565", 10.29m, "The Great Gatsby" },
                    { 6, "Herman Melville", "9781503280786", 11.95m, "Moby-Dick" },
                    { 7, "Leo Tolstoy", "9781400079988", 12.99m, "War and Peace" },
                    { 8, "J.R.R. Tolkien", "9780547928227", 13.49m, "The Hobbit" },
                    { 9, "Fyodor Dostoevsky", "9780486415871", 9.59m, "Crime and Punishment" },
                    { 10, "J.R.R. Tolkien", "9780544003415", 24.99m, "The Lord of the Rings" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 10);
        }
    }
}
