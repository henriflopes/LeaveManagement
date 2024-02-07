using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeaveManagement.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedRequestComments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RequestComments",
                table: "LeaveRequests",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "208aa945-2d84-2421-2342-2269ec64d949",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "23cbbf86-7f78-4d9a-9c7e-58b6c08d844c", "AQAAAAIAAYagAAAAEM+U8CzHSHWc4YMhsc2WhjW/71g1T282xcQmIuY66xpFBMiSz9Mcrtt2g0K0FWJgkw==", "905a5244-edcf-435d-8830-2599a805e230" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "408aa945-3d84-4421-8342-7269ec64d949",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ce1e530e-9066-42d6-8b84-8e1e586e6d07", "AQAAAAIAAYagAAAAEAabvfNBhZt0EpboJ24SjxOSlo/l+V9Wc1pWqf/SEmHu12TOxrTgT9oVq7WaoXayxQ==", "30cbe2bc-6165-49bf-b409-d99610609241" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RequestComments",
                table: "LeaveRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "208aa945-2d84-2421-2342-2269ec64d949",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "46d13e1b-111d-43e2-931b-56d3c1c21e68", "AQAAAAIAAYagAAAAEGGZGHLXy1RVV2tiXSB7rDP+ptYRl2DEcSF1AfrxETccGEhVg5MFGMaMLntmf1RR5w==", "83087495-70bd-4a21-9b4f-5873155a9d2c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "408aa945-3d84-4421-8342-7269ec64d949",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "678658a9-3756-4db5-ad03-dffb52b113e8", "AQAAAAIAAYagAAAAEOodA8P714b+iizCA4uLOXB5Wklehz5Z7fBPV8LT482NlQWWatc57Jw1htqSZhSwgA==", "4d4f06e2-9c58-483a-bb67-1cffcc7c02db" });
        }
    }
}
