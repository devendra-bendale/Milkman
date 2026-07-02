using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Milkman2.Data.Migrations
{
    /// <inheritdoc />
    public partial class Schemaadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerAttendances_Customers_CustomerId",
                table: "CustomerAttendances");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerDailyEntries_Customers_CustomerId",
                table: "CustomerDailyEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerDailyEntries_MilkTypes_MilkTypeId",
                table: "CustomerDailyEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_UserAccounts_UserId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_MilkTypes_UserAccounts_UserId",
                table: "MilkTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrders_MilkTypes_MilkTypeId",
                table: "PurchaseOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrders_Suppliers_SupplierId",
                table: "PurchaseOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrders_Customers_CustomerId",
                table: "SalesOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrders_MilkTypes_MilkTypeId",
                table: "SalesOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_UserAccounts_UserId",
                table: "Suppliers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAccounts",
                table: "UserAccounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Suppliers",
                table: "Suppliers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SalesOrders",
                table: "SalesOrders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PurchaseOrders",
                table: "PurchaseOrders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MilkTypes",
                table: "MilkTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customers",
                table: "Customers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerDailyEntries",
                table: "CustomerDailyEntries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerAttendances",
                table: "CustomerAttendances");

            migrationBuilder.EnsureSchema(
                name: "milkman");

            migrationBuilder.RenameTable(
                name: "UserAccounts",
                newName: "UserAccount",
                newSchema: "milkman");

            migrationBuilder.RenameTable(
                name: "Suppliers",
                newName: "Supplier",
                newSchema: "milkman");

            migrationBuilder.RenameTable(
                name: "SalesOrders",
                newName: "SalesOrder",
                newSchema: "milkman");

            migrationBuilder.RenameTable(
                name: "PurchaseOrders",
                newName: "PurchaseOrder",
                newSchema: "milkman");

            migrationBuilder.RenameTable(
                name: "MilkTypes",
                newName: "MilkType",
                newSchema: "milkman");

            migrationBuilder.RenameTable(
                name: "Customers",
                newName: "Customer",
                newSchema: "milkman");

            migrationBuilder.RenameTable(
                name: "CustomerDailyEntries",
                newName: "CustomerDailyEntry",
                newSchema: "milkman");

            migrationBuilder.RenameTable(
                name: "CustomerAttendances",
                newName: "CustomerAttendance",
                newSchema: "milkman");

            migrationBuilder.RenameIndex(
                name: "IX_Suppliers_UserId",
                schema: "milkman",
                table: "Supplier",
                newName: "IX_Supplier_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_SalesOrders_MilkTypeId",
                schema: "milkman",
                table: "SalesOrder",
                newName: "IX_SalesOrder_MilkTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_SalesOrders_CustomerId",
                schema: "milkman",
                table: "SalesOrder",
                newName: "IX_SalesOrder_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseOrders_SupplierId",
                schema: "milkman",
                table: "PurchaseOrder",
                newName: "IX_PurchaseOrder_SupplierId");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseOrders_MilkTypeId",
                schema: "milkman",
                table: "PurchaseOrder",
                newName: "IX_PurchaseOrder_MilkTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_MilkTypes_UserId",
                schema: "milkman",
                table: "MilkType",
                newName: "IX_MilkType_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_UserId",
                schema: "milkman",
                table: "Customer",
                newName: "IX_Customer_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerDailyEntries_MilkTypeId",
                schema: "milkman",
                table: "CustomerDailyEntry",
                newName: "IX_CustomerDailyEntry_MilkTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerDailyEntries_CustomerId",
                schema: "milkman",
                table: "CustomerDailyEntry",
                newName: "IX_CustomerDailyEntry_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerAttendances_CustomerId",
                schema: "milkman",
                table: "CustomerAttendance",
                newName: "IX_CustomerAttendance_CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAccount",
                schema: "milkman",
                table: "UserAccount",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Supplier",
                schema: "milkman",
                table: "Supplier",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SalesOrder",
                schema: "milkman",
                table: "SalesOrder",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PurchaseOrder",
                schema: "milkman",
                table: "PurchaseOrder",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MilkType",
                schema: "milkman",
                table: "MilkType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customer",
                schema: "milkman",
                table: "Customer",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerDailyEntry",
                schema: "milkman",
                table: "CustomerDailyEntry",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerAttendance",
                schema: "milkman",
                table: "CustomerAttendance",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_UserAccount_UserId",
                schema: "milkman",
                table: "Customer",
                column: "UserId",
                principalSchema: "milkman",
                principalTable: "UserAccount",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerAttendance_Customer_CustomerId",
                schema: "milkman",
                table: "CustomerAttendance",
                column: "CustomerId",
                principalSchema: "milkman",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerDailyEntry_Customer_CustomerId",
                schema: "milkman",
                table: "CustomerDailyEntry",
                column: "CustomerId",
                principalSchema: "milkman",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerDailyEntry_MilkType_MilkTypeId",
                schema: "milkman",
                table: "CustomerDailyEntry",
                column: "MilkTypeId",
                principalSchema: "milkman",
                principalTable: "MilkType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MilkType_UserAccount_UserId",
                schema: "milkman",
                table: "MilkType",
                column: "UserId",
                principalSchema: "milkman",
                principalTable: "UserAccount",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrder_MilkType_MilkTypeId",
                schema: "milkman",
                table: "PurchaseOrder",
                column: "MilkTypeId",
                principalSchema: "milkman",
                principalTable: "MilkType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrder_Supplier_SupplierId",
                schema: "milkman",
                table: "PurchaseOrder",
                column: "SupplierId",
                principalSchema: "milkman",
                principalTable: "Supplier",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrder_Customer_CustomerId",
                schema: "milkman",
                table: "SalesOrder",
                column: "CustomerId",
                principalSchema: "milkman",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrder_MilkType_MilkTypeId",
                schema: "milkman",
                table: "SalesOrder",
                column: "MilkTypeId",
                principalSchema: "milkman",
                principalTable: "MilkType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Supplier_UserAccount_UserId",
                schema: "milkman",
                table: "Supplier",
                column: "UserId",
                principalSchema: "milkman",
                principalTable: "UserAccount",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customer_UserAccount_UserId",
                schema: "milkman",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerAttendance_Customer_CustomerId",
                schema: "milkman",
                table: "CustomerAttendance");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerDailyEntry_Customer_CustomerId",
                schema: "milkman",
                table: "CustomerDailyEntry");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerDailyEntry_MilkType_MilkTypeId",
                schema: "milkman",
                table: "CustomerDailyEntry");

            migrationBuilder.DropForeignKey(
                name: "FK_MilkType_UserAccount_UserId",
                schema: "milkman",
                table: "MilkType");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrder_MilkType_MilkTypeId",
                schema: "milkman",
                table: "PurchaseOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrder_Supplier_SupplierId",
                schema: "milkman",
                table: "PurchaseOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrder_Customer_CustomerId",
                schema: "milkman",
                table: "SalesOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrder_MilkType_MilkTypeId",
                schema: "milkman",
                table: "SalesOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_Supplier_UserAccount_UserId",
                schema: "milkman",
                table: "Supplier");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAccount",
                schema: "milkman",
                table: "UserAccount");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Supplier",
                schema: "milkman",
                table: "Supplier");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SalesOrder",
                schema: "milkman",
                table: "SalesOrder");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PurchaseOrder",
                schema: "milkman",
                table: "PurchaseOrder");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MilkType",
                schema: "milkman",
                table: "MilkType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerDailyEntry",
                schema: "milkman",
                table: "CustomerDailyEntry");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerAttendance",
                schema: "milkman",
                table: "CustomerAttendance");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customer",
                schema: "milkman",
                table: "Customer");

            migrationBuilder.RenameTable(
                name: "UserAccount",
                schema: "milkman",
                newName: "UserAccounts");

            migrationBuilder.RenameTable(
                name: "Supplier",
                schema: "milkman",
                newName: "Suppliers");

            migrationBuilder.RenameTable(
                name: "SalesOrder",
                schema: "milkman",
                newName: "SalesOrders");

            migrationBuilder.RenameTable(
                name: "PurchaseOrder",
                schema: "milkman",
                newName: "PurchaseOrders");

            migrationBuilder.RenameTable(
                name: "MilkType",
                schema: "milkman",
                newName: "MilkTypes");

            migrationBuilder.RenameTable(
                name: "CustomerDailyEntry",
                schema: "milkman",
                newName: "CustomerDailyEntries");

            migrationBuilder.RenameTable(
                name: "CustomerAttendance",
                schema: "milkman",
                newName: "CustomerAttendances");

            migrationBuilder.RenameTable(
                name: "Customer",
                schema: "milkman",
                newName: "Customers");

            migrationBuilder.RenameIndex(
                name: "IX_Supplier_UserId",
                table: "Suppliers",
                newName: "IX_Suppliers_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_SalesOrder_MilkTypeId",
                table: "SalesOrders",
                newName: "IX_SalesOrders_MilkTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_SalesOrder_CustomerId",
                table: "SalesOrders",
                newName: "IX_SalesOrders_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseOrder_SupplierId",
                table: "PurchaseOrders",
                newName: "IX_PurchaseOrders_SupplierId");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseOrder_MilkTypeId",
                table: "PurchaseOrders",
                newName: "IX_PurchaseOrders_MilkTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_MilkType_UserId",
                table: "MilkTypes",
                newName: "IX_MilkTypes_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerDailyEntry_MilkTypeId",
                table: "CustomerDailyEntries",
                newName: "IX_CustomerDailyEntries_MilkTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerDailyEntry_CustomerId",
                table: "CustomerDailyEntries",
                newName: "IX_CustomerDailyEntries_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerAttendance_CustomerId",
                table: "CustomerAttendances",
                newName: "IX_CustomerAttendances_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Customer_UserId",
                table: "Customers",
                newName: "IX_Customers_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAccounts",
                table: "UserAccounts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Suppliers",
                table: "Suppliers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SalesOrders",
                table: "SalesOrders",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PurchaseOrders",
                table: "PurchaseOrders",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MilkTypes",
                table: "MilkTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerDailyEntries",
                table: "CustomerDailyEntries",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerAttendances",
                table: "CustomerAttendances",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customers",
                table: "Customers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerAttendances_Customers_CustomerId",
                table: "CustomerAttendances",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerDailyEntries_Customers_CustomerId",
                table: "CustomerDailyEntries",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerDailyEntries_MilkTypes_MilkTypeId",
                table: "CustomerDailyEntries",
                column: "MilkTypeId",
                principalTable: "MilkTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_PurchaseOrders_MilkTypes_MilkTypeId",
                table: "PurchaseOrders",
                column: "MilkTypeId",
                principalTable: "MilkTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrders_Suppliers_SupplierId",
                table: "PurchaseOrders",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrders_Customers_CustomerId",
                table: "SalesOrders",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrders_MilkTypes_MilkTypeId",
                table: "SalesOrders",
                column: "MilkTypeId",
                principalTable: "MilkTypes",
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
    }
}
