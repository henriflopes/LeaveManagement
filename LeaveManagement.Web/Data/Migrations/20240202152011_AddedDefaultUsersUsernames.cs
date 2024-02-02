using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeaveManagement.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedDefaultUsersUsernames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "208aa945-2d84-2421-2342-2269ec64d949",
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "ff549954-a746-45c6-a61c-0191274240ef", true, "USER@LOCALHOST.COM", "AQAAAAIAAYagAAAAEPpA7lj5sv5/6j3fs9sx5XFeuDNKUpLmuVH0RlP1H5VryCQNkVCnhy8G/zo0CgTtRg==", "6c876a26-40ac-441c-8771-6d699e02147c", "user@localhost.com" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "408aa945-3d84-4421-8342-7269ec64d949",
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "b5f874f1-4550-4422-948f-3e684b34a730", true, "ADMIN@LOCALHOST.COM", "AQAAAAIAAYagAAAAEGaWgjhVBb9wifr0X28Bk40Jpo+DLtiAeKVmayz9peB7UQJKVv0gkyKaOYOADCFODw==", "8e739a66-a292-48c7-bb18-2ac740ca3385", "admin@localhost.com" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "208aa945-2d84-2421-2342-2269ec64d949",
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "fcc6f2e1-31fe-46b8-a291-3898017c7ffb", false, null, "AQAAAAIAAYagAAAAEJ7R/Ivx72IyPsVjp2UePfRUt/vmkdmidWJQU3KsWNuLSyOPBvsDz9PyTV92n1balA==", "5df23c6c-9ffa-483f-9514-9780db5a9914", null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "408aa945-3d84-4421-8342-7269ec64d949",
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "5dbc3153-7c76-48a1-8484-515317e445c0", false, null, "AQAAAAIAAYagAAAAENuZdQ7YvfBf6OQh6aXBb6F/A6GEhIfdZzJSdLnJWBHiWMO4R255MUIzM/zxA4Qmgw==", "99f15b02-e14c-4f37-bdd0-07240f7eabe3", null });
        }
    }
}
