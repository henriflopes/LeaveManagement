using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeaveManagement.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedForeignKeysLeaveAllocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaveAllocations_AspNetUsers_EmployeeId",
                table: "LeaveAllocations");

            migrationBuilder.DropIndex(
                name: "IX_LeaveAllocations_EmployeeId",
                table: "LeaveAllocations");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "LeaveAllocations",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeId1",
                table: "LeaveAllocations",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "208aa945-2d84-2421-2342-2269ec64d949",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d943921d-a0f8-4a45-abb2-a5abdbcdacb4", "AQAAAAIAAYagAAAAEGKM4wxfJvdKHSBIUGuXdE98Ww7FfIO0O/oOlQO4sVAeEqiMWupp8di4OQJo6ePEGA==", "4b502948-c884-413f-81bc-d03f999d9288" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "408aa945-3d84-4421-8342-7269ec64d949",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e1b6cbb5-7472-4646-9766-afd0a6da7157", "AQAAAAIAAYagAAAAEEr3MiVfS+xAgbmEM5RSGSKbQMKyhrC3CeArsUg4ZcF6UbUTtNJWW0BpVajSIimnjg==", "46664bef-df60-4466-ab23-e81c8c0a1c11" });

            migrationBuilder.CreateIndex(
                name: "IX_LeaveAllocations_EmployeeId1",
                table: "LeaveAllocations",
                column: "EmployeeId1");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveAllocations_AspNetUsers_EmployeeId1",
                table: "LeaveAllocations",
                column: "EmployeeId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaveAllocations_AspNetUsers_EmployeeId1",
                table: "LeaveAllocations");

            migrationBuilder.DropIndex(
                name: "IX_LeaveAllocations_EmployeeId1",
                table: "LeaveAllocations");

            migrationBuilder.DropColumn(
                name: "EmployeeId1",
                table: "LeaveAllocations");

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeId",
                table: "LeaveAllocations",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "208aa945-2d84-2421-2342-2269ec64d949",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9e65e46a-a12b-4cb1-a8f5-a1bd42d5c5d9", "AQAAAAIAAYagAAAAEGbrqUncqyIsnQdyOZYXB5TagXHIAz+gN7w2BEp1W9gwcb5orFkWODmzBBh40aTCXg==", "3928281d-915c-488c-8dcc-65d845ff2ac6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "408aa945-3d84-4421-8342-7269ec64d949",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b3d9500d-0c24-4393-aec1-56c64d40e282", "AQAAAAIAAYagAAAAEDiCDeiJLywzLfoI/bCsITHZ+gPvHO3KcF2xcmpVjhhxkU+gI9jM9oVvaG9mGBrxbQ==", "a4fd452d-85de-4f4b-a776-0d3ebef6437a" });

            migrationBuilder.CreateIndex(
                name: "IX_LeaveAllocations_EmployeeId",
                table: "LeaveAllocations",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveAllocations_AspNetUsers_EmployeeId",
                table: "LeaveAllocations",
                column: "EmployeeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
