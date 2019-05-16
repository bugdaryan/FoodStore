using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Shop.Data.Migrations
{
    public partial class FixOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Remain",
                table: "ShoppingCartItems");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Remain",
                table: "ShoppingCartItems",
                nullable: false,
                defaultValue: 0);
        }
    }
}
