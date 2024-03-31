using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Product.Migrations
{
    /// <inheritdoc />
    public partial class changename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Start",
                table: "Reviews",
                newName: "Star");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Star",
                table: "Reviews",
                newName: "Start");
        }
    }
}
