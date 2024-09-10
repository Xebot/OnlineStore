 using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineStore.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class DeleteSomeFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "attributes");

            migrationBuilder.DropColumn(
                name: "Description1",
                table: "attributes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "attributes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description1",
                table: "attributes",
                type: "text",
                nullable: true);
        }
    }
}
