using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ORM_Ecommerce_MiniConsoleApp.Migrations
{
    public partial class addNullableDescriptionProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Products",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 12, 20, 35, 18, 953, DateTimeKind.Utc).AddTicks(895),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 11, 20, 34, 56, 190, DateTimeKind.Utc).AddTicks(6373));

            migrationBuilder.AddCheckConstraint(
                name: "CK_Stock",
                table: "Products",
                sql: "Stock >= 0");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Stock",
                table: "Products");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Products",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 11, 20, 34, 56, 190, DateTimeKind.Utc).AddTicks(6373),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 12, 20, 35, 18, 953, DateTimeKind.Utc).AddTicks(895));
        }
    }
}
