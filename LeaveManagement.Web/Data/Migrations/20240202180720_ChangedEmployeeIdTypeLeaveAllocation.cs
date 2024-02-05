using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeaveManagement.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangedEmployeeIdTypeLeaveAllocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "208aa945-2d84-2421-2342-2269ec64d949",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a258b97a-af30-4661-8dd9-38bd4407ed15", "AQAAAAIAAYagAAAAEB+lE97BiD3r0URI7suFaL4NP8nxIC2K/2rfMqchsfUO202gjwe3alghcGAxo9Pu4g==", "e8c5f7dc-2a6e-4d23-beb6-b2ce0b7c4c16" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "408aa945-3d84-4421-8342-7269ec64d949",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "eb552e18-1aff-409a-8bfb-cc4bcd32aca2", "AQAAAAIAAYagAAAAEIHCuD3JvuijzADsm/1m9/r4RVifGWk/9PyHWFs2lmicc1meALTY5T2Y47VF31P/Dw==", "7b8b539f-57ea-4036-9737-a25f27e8075c" });

            migrationBuilder.CreateIndex(
                name: "IX_LeaveAllocations_EmployeeId",
                table: "LeaveAllocations",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveAllocations_AspNetUsers_EmployeeId",
                table: "LeaveAllocations",
                column: "EmployeeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

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
    }
}
