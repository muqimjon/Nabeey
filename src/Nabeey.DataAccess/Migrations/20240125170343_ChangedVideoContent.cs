using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nabeey.DataAccess.Migrations
{
    public partial class ChangedVideoContent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "ContentVideos",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "Phone" },
                values: new object[] { new DateTime(2024, 1, 25, 17, 3, 43, 646, DateTimeKind.Utc).AddTicks(305), "+998987654321" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Author",
                table: "ContentVideos");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "Phone" },
                values: new object[] { new DateTime(2023, 12, 23, 12, 47, 44, 319, DateTimeKind.Utc).AddTicks(5871), "+998979565060" });
        }
    }
}
