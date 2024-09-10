using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OnlineStore.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataForProductAttribute : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description3",
                table: "attributes");

            migrationBuilder.InsertData(
                table: "attributes",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "Color" },
                    { 2, "NumberOfStrings" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "attributes",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "attributes",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.AddColumn<int>(
                name: "Description3",
                table: "attributes",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
