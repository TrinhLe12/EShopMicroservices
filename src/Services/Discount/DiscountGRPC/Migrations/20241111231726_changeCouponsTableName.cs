using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiscountGRPC.Migrations
{
    /// <inheritdoc />
    public partial class changeCouponsTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Coupones",
                table: "Coupones");

            migrationBuilder.RenameTable(
                name: "Coupones",
                newName: "Coupons");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Coupons",
                table: "Coupons",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Coupons",
                table: "Coupons");

            migrationBuilder.RenameTable(
                name: "Coupons",
                newName: "Coupones");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Coupones",
                table: "Coupones",
                column: "Id");
        }
    }
}
