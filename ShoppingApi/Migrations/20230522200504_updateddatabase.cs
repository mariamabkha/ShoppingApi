using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingApi.Migrations
{
    /// <inheritdoc />
    public partial class updateddatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deliveries_UserAcounts_UserAccountId",
                table: "Deliveries");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_UserAcounts_UserAccountId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Payment_Products_ProductId",
                table: "Payment");

            migrationBuilder.DropForeignKey(
                name: "FK_Payment_UserAcounts_UserAccountId",
                table: "Payment");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_UserAcounts_UserAccountId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_UserAccountId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Payment_UserAccountId",
                table: "Payment");

            migrationBuilder.DropIndex(
                name: "IX_Order_UserAccountId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Deliveries_UserAccountId",
                table: "Deliveries");

            migrationBuilder.DropColumn(
                name: "UserAccountId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "UserAccountId",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "UserAccountId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "UserAccountId",
                table: "Deliveries");

            migrationBuilder.AddColumn<int>(
                name: "UserTypesId",
                table: "UserAcounts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserAcounts_UserTypesId",
                table: "UserAcounts",
                column: "UserTypesId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_UserId",
                table: "Products",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_UserId",
                table: "Order",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_UserId",
                table: "Deliveries",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Deliveries_UserAcounts_UserId",
                table: "Deliveries",
                column: "UserId",
                principalTable: "UserAcounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_UserAcounts_UserId",
                table: "Order",
                column: "UserId",
                principalTable: "UserAcounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_UserAcounts_ProductId",
                table: "Payment",
                column: "ProductId",
                principalTable: "UserAcounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_UserAcounts_UserId",
                table: "Products",
                column: "UserId",
                principalTable: "UserAcounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAcounts_UserTypes_UserTypesId",
                table: "UserAcounts",
                column: "UserTypesId",
                principalTable: "UserTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deliveries_UserAcounts_UserId",
                table: "Deliveries");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_UserAcounts_UserId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Payment_UserAcounts_ProductId",
                table: "Payment");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_UserAcounts_UserId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAcounts_UserTypes_UserTypesId",
                table: "UserAcounts");

            migrationBuilder.DropIndex(
                name: "IX_UserAcounts_UserTypesId",
                table: "UserAcounts");

            migrationBuilder.DropIndex(
                name: "IX_Products_UserId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Order_UserId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Deliveries_UserId",
                table: "Deliveries");

            migrationBuilder.DropColumn(
                name: "UserTypesId",
                table: "UserAcounts");

            migrationBuilder.AddColumn<int>(
                name: "UserAccountId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserAccountId",
                table: "Payment",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserAccountId",
                table: "Order",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserAccountId",
                table: "Deliveries",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_UserAccountId",
                table: "Products",
                column: "UserAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_UserAccountId",
                table: "Payment",
                column: "UserAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_UserAccountId",
                table: "Order",
                column: "UserAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_UserAccountId",
                table: "Deliveries",
                column: "UserAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Deliveries_UserAcounts_UserAccountId",
                table: "Deliveries",
                column: "UserAccountId",
                principalTable: "UserAcounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_UserAcounts_UserAccountId",
                table: "Order",
                column: "UserAccountId",
                principalTable: "UserAcounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_Products_ProductId",
                table: "Payment",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_UserAcounts_UserAccountId",
                table: "Payment",
                column: "UserAccountId",
                principalTable: "UserAcounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_UserAcounts_UserAccountId",
                table: "Products",
                column: "UserAccountId",
                principalTable: "UserAcounts",
                principalColumn: "Id");
        }
    }
}
