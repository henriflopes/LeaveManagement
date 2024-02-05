using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeaveManagement.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangedDefaultDaysTypeToIntLeaveType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "DefaultDays",
                table: "LeaveTypes",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "208aa945-2d84-2421-2342-2269ec64d949",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9018ce37-5f36-41b5-a0db-59981772e8a1", "AQAAAAIAAYagAAAAEMgpqeHIz9ZJ2KhcF8dhwNpxuyiujyHapfcITwxXBayFUW8qQ5c5PAIWurPKKk+k/g==", "fc1fd38d-df15-49d2-9f0a-206b2d1786ea" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "408aa945-3d84-4421-8342-7269ec64d949",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "06d3fa7c-231b-45c7-b3b0-8149cc0781d9", "AQAAAAIAAYagAAAAELcNM8eWJQef3txpqNIq9aycaFtqjZ3HETKkBrPoBvV1ARnp9WbP/bdWwnqPzXbwTg==", "7b05abbb-e063-4ccc-a652-9eb552316983" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DefaultDays",
                table: "LeaveTypes",
                type: "nvarchar(max)",
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
        }
    }
}
