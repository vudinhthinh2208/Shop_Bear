using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop_Bear.Migrations
{
    /// <inheritdoc />
    public partial class ttt3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DatePayment",
                table: "tb_Order",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DatePayment",
                table: "tb_Order");
        }
    }
}
