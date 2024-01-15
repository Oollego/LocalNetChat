using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UdpChat.Migrations
{
    /// <inheritdoc />
    public partial class ContactMigrationv3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: new Guid("2ad81c5d-42c1-4ae7-979f-09b29d3f22fd"));

            migrationBuilder.InsertData(
                table: "Settings",
                columns: new[] { "Id", "Avatar", "AvatarFileName", "FolderPath", "IsAvatarAdded", "Name", "Port", "Surname" },
                values: new object[] { new Guid("4dba2944-b8cf-4460-a618-5ea175d3f924"), null, "", "C:\\Users\\Dasha\\Pictures\\StepTemp\\UdpChat\\UdpChat\\Downloads", false, "UserName", 10000, "" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: new Guid("4dba2944-b8cf-4460-a618-5ea175d3f924"));

            migrationBuilder.InsertData(
                table: "Settings",
                columns: new[] { "Id", "Avatar", "AvatarFileName", "FolderPath", "IsAvatarAdded", "Name", "Port", "Surname" },
                values: new object[] { new Guid("2ad81c5d-42c1-4ae7-979f-09b29d3f22fd"), null, "", "C:\\Users\\Dasha\\Pictures\\StepTemp\\UdpChat\\UdpChat\\Downloads", false, "UserName", 10000, "" });
        }
    }
}
