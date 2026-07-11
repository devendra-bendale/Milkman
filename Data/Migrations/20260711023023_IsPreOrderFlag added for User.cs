using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Milkman2.Data.Migrations
{
    /// <inheritdoc />
    public partial class IsPreOrderFlagaddedforUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPreOrderApplicable",
                schema: "milkman",
                table: "UserAccount",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPreOrderApplicable",
                schema: "milkman",
                table: "UserAccount");
        }
    }
}
