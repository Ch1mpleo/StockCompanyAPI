using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class Initializedb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8e5e830f-525f-4b55-9e0e-cf218237fad3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "de6cc382-1574-4735-a5f3-576206d1f187");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4936d2ff-5b21-4fc1-9bc2-44283fb2f21c", null, "Admin", "ADMIN" },
                    { "8da52e3a-0882-4044-8aa2-0b539ffb9b9e", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4936d2ff-5b21-4fc1-9bc2-44283fb2f21c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8da52e3a-0882-4044-8aa2-0b539ffb9b9e");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "8e5e830f-525f-4b55-9e0e-cf218237fad3", null, "Admin", "ADMIN" },
                    { "de6cc382-1574-4735-a5f3-576206d1f187", null, "User", "USER" }
                });
        }
    }
}
