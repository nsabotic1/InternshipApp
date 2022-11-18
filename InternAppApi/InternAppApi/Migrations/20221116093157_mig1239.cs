using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternAppApi.Migrations
{
    public partial class mig1239 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e91e639c-27b9-44de-9fd8-efd62be07517",
                column: "ConcurrencyStamp",
                value: "7e1cdfc2-b418-49fe-a8e3-d1db9fe06f6b");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "044fd6d7-e011-4d9d-b050-6302884a975d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6b81fd42-5f9a-41a3-8356-5dd8005735b5", "AQAAAAEAACcQAAAAEBNl6XRgMpH3Ka9A2q8VDLycdWCnb5cP3DyIa5MOMG2TJ1NH4A307ZxRKgRkAYFCcQ==", "558fa1d0-4bb1-4e41-8afd-7dc05b91a988" });
        }
    }
}
