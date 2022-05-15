using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlowersBEWebApi.Migrations
{
    public partial class FEB20UpdateShopEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Shops",
                type: "datetime",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP()");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Shops",
                type: "datetime",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP()");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Shops");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Shops");
        }
    }
}
