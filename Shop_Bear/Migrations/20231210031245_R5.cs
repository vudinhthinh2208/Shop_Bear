using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop_Bear.Migrations
{
    /// <inheritdoc />
    public partial class R5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "tb_Product",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "tb_Posts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "tb_News",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "tb_Category",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "tb_Product");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "tb_Posts");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "tb_News");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "tb_Category");
        }
    }
}
