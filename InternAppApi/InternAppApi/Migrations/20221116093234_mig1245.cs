using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternAppApi.Migrations
{
    public partial class mig1245 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e91e639c-27b9-44de-9fd8-efd62be07517",
                column: "ConcurrencyStamp",
                value: "a70164ab-233e-44d8-9a7f-f20c69abb077");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "044fd6d7-e011-4d9d-b050-6302884a975d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "59623a6d-40b4-42be-8b8c-5cf276781150", "AQAAAAEAACcQAAAAEC5Nw0MzIXP9m8F0oHQp0xx72WkedpxTgIR6tOgD210PAOjEwjtlCICqTVLU8O4IZQ==", "8506384c-c809-4ef1-9360-c3d4e90b6482" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e91e639c-27b9-44de-9fd8-efd62be07517",
                column: "ConcurrencyStamp",
                value: "923867b6-77f4-4aaa-9984-9c715dbd0659");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "044fd6d7-e011-4d9d-b050-6302884a975d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dafb221e-aba9-4990-8abd-b0d836e5f863", "AQAAAAEAACcQAAAAEIvzWc6Rs2CWHEId/cz5yD6zKJMlEYUhjlWseRV9Fduq9LWp5bLq+OTXWrdCp7Va8A==", "e6bed955-1922-4bf9-9439-673828d416f9" });
        }
    }
}
