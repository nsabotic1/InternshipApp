using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternAppApi.Migrations
{
    public partial class migiii : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e91e639c-27b9-44de-9fd8-efd62be07517",
                column: "ConcurrencyStamp",
                value: "dd7e6217-0861-4eac-b955-e02fe46429aa");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "044fd6d7-e011-4d9d-b050-6302884a975d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "94928894-13ae-4578-88d5-998f66ad9d7d", "AQAAAAEAACcQAAAAEAaFxEGIPeLVVvDNmddu2iQ8eDERy5z5uVTT717vZx597ykJXqNjFDLUG6ooIpqQUA==", "4c9ea61f-15af-4bd6-a828-241bb7f03ebe" });
        }
    }
}
