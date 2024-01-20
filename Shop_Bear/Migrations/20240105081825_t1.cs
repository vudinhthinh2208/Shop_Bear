using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop_Bear.Migrations
{
    /// <inheritdoc />
    public partial class t1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Controller",
                table: "tb_Category");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Controller",
                table: "tb_Category",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
