using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Milkman2.Data.Migrations
{
    /// <inheritdoc />
    public partial class FatsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Fats",
                table: "PurchaseOrders",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fats",
                table: "PurchaseOrders");
        }
    }
}
