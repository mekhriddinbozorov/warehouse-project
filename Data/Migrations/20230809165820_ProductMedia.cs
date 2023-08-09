using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace warehouse_project.Data.Migrations
{
    /// <inheritdoc />
    public partial class ProductMedia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Data",
                table: "ProductMedia",
                type: "longblob",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "ProductMedia",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Data",
                table: "ProductMedia");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "ProductMedia");
        }
    }
}
