using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop_Bear.Migrations
{
    /// <inheritdoc />
    public partial class h4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_ProductImage_tb_Product_ProductId",
                table: "tb_ProductImage");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "tb_ProductImage",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "tb_ProductImage",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "tb_Order",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_ProductImage_tb_Product_ProductId",
                table: "tb_ProductImage",
                column: "ProductId",
                principalTable: "tb_Product",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_ProductImage_tb_Product_ProductId",
                table: "tb_ProductImage");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "tb_Order");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "tb_ProductImage",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "tb_ProductImage",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_ProductImage_tb_Product_ProductId",
                table: "tb_ProductImage",
                column: "ProductId",
                principalTable: "tb_Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
