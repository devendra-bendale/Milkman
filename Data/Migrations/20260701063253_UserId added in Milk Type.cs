using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Milkman2.Data.Migrations
{
    /// <inheritdoc />
    public partial class UserIdaddedinMilkType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_UserAccount_UserId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_UserAccount_UserId",
                table: "Suppliers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAccount",
                table: "UserAccount");

            migrationBuilder.RenameTable(
                name: "UserAccount",
                newName: "UserAccounts");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "MilkTypes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAccounts",
                table: "UserAccounts",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_MilkTypes_UserId",
                table: "MilkTypes",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_UserAccounts_UserId",
                table: "Customers",
                column: "UserId",
                principalTable: "UserAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MilkTypes_UserAccounts_UserId",
                table: "MilkTypes",
                column: "UserId",
                principalTable: "UserAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_UserAccounts_UserId",
                table: "Suppliers",
                column: "UserId",
                principalTable: "UserAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_UserAccounts_UserId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_MilkTypes_UserAccounts_UserId",
                table: "MilkTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_UserAccounts_UserId",
                table: "Suppliers");

            migrationBuilder.DropIndex(
                name: "IX_MilkTypes_UserId",
                table: "MilkTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAccounts",
                table: "UserAccounts");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "MilkTypes");

            migrationBuilder.RenameTable(
                name: "UserAccounts",
                newName: "UserAccount");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAccount",
                table: "UserAccount",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_UserAccount_UserId",
                table: "Customers",
                column: "UserId",
                principalTable: "UserAccount",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_UserAccount_UserId",
                table: "Suppliers",
                column: "UserId",
                principalTable: "UserAccount",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
