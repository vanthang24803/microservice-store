using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Product.Migrations
{
    /// <inheritdoc />
    public partial class fixdetailproduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Detail",
                table: "Books",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Introduction",
                table: "Books",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Detail",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Introduction",
                table: "Books");
        }
    }
}
