using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeaveManagement.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddingPeriodToAllocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Period",
                table: "LeaveAllocations",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Period",
                table: "LeaveAllocations");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "208aa945-2d84-2421-2342-2269ec64d949",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ff549954-a746-45c6-a61c-0191274240ef", "AQAAAAIAAYagAAAAEPpA7lj5sv5/6j3fs9sx5XFeuDNKUpLmuVH0RlP1H5VryCQNkVCnhy8G/zo0CgTtRg==", "6c876a26-40ac-441c-8771-6d699e02147c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "408aa945-3d84-4421-8342-7269ec64d949",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b5f874f1-4550-4422-948f-3e684b34a730", "AQAAAAIAAYagAAAAEGaWgjhVBb9wifr0X28Bk40Jpo+DLtiAeKVmayz9peB7UQJKVv0gkyKaOYOADCFODw==", "8e739a66-a292-48c7-bb18-2ac740ca3385" });
        }
    }
}
