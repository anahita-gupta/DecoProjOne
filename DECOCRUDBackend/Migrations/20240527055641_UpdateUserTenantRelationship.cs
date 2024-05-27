using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DECOCRUDBackend.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserTenantRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TenantKey",
                table: "UserItems",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserItems_TenantKey",
                table: "UserItems",
                column: "TenantKey");

            migrationBuilder.AddForeignKey(
                name: "FK_UserItems_TenantItems_TenantKey",
                table: "UserItems",
                column: "TenantKey",
                principalTable: "TenantItems",
                principalColumn: "TenantId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserItems_TenantItems_TenantKey",
                table: "UserItems");

            migrationBuilder.DropIndex(
                name: "IX_UserItems_TenantKey",
                table: "UserItems");

            migrationBuilder.AlterColumn<string>(
                name: "TenantKey",
                table: "UserItems",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);
        }
    }
}
