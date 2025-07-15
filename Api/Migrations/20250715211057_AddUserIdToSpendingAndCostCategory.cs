using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIdToSpendingAndCostCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Spending",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "CostCategory",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Spending_UserId",
                table: "Spending",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CostCategory_UserId",
                table: "CostCategory",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CostCategory_users_UserId",
                table: "CostCategory",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Spending_users_UserId",
                table: "Spending",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CostCategory_users_UserId",
                table: "CostCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_Spending_users_UserId",
                table: "Spending");

            migrationBuilder.DropIndex(
                name: "IX_Spending_UserId",
                table: "Spending");

            migrationBuilder.DropIndex(
                name: "IX_CostCategory_UserId",
                table: "CostCategory");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Spending");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "CostCategory");
        }
    }
}
