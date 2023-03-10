using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shopping_App.Migrations
{
    public partial class khaingu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "sanpham",
                type: "nchar(1000)",
                fixedLength: true,
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nchar(10000)",
                oldFixedLength: true,
                oldMaxLength: 10000);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "sanpham",
                type: "nchar(10000)",
                fixedLength: true,
                maxLength: 10000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nchar(1000)",
                oldFixedLength: true,
                oldMaxLength: 1000);
        }
    }
}
