using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UdpChat.Migrations
{
    /// <inheritdoc />
    public partial class ContactMigrationv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: new Guid("3a55189e-29e5-45b5-80a2-2134c343e033"));

            migrationBuilder.AddColumn<string>(
                name: "AvatarFileName",
                table: "Settings",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Avatar",
                table: "Contacts",
                type: "BLOB",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "BLOB");

            migrationBuilder.InsertData(
                table: "Settings",
                columns: new[] { "Id", "Avatar", "AvatarFileName", "FolderPath", "IsAvatarAdded", "Name", "Port", "Surname" },
                values: new object[] { new Guid("2ad81c5d-42c1-4ae7-979f-09b29d3f22fd"), null, "", "C:\\Users\\Dasha\\Pictures\\StepTemp\\UdpChat\\UdpChat\\Downloads", false, "UserName", 10000, "" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: new Guid("2ad81c5d-42c1-4ae7-979f-09b29d3f22fd"));

            migrationBuilder.DropColumn(
                name: "AvatarFileName",
                table: "Settings");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Avatar",
                table: "Contacts",
                type: "BLOB",
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(byte[]),
                oldType: "BLOB",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Settings",
                columns: new[] { "Id", "Avatar", "FolderPath", "IsAvatarAdded", "Name", "Port", "Surname" },
                values: new object[] { new Guid("3a55189e-29e5-45b5-80a2-2134c343e033"), null, "C:\\Users\\Dasha\\Pictures\\StepTemp\\UdpChat\\UdpChat", false, "UserName", 10000, "" });
        }
    }
}
