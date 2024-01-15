using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UdpChat.Migrations
{
    /// <inheritdoc />
    public partial class ContactMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Surname = table.Column<string>(type: "TEXT", nullable: true),
                    IpAddress = table.Column<string>(type: "TEXT", nullable: false),
                    Avatar = table.Column<byte[]>(type: "BLOB", nullable: false),
                    IsAvatarAdded = table.Column<bool>(type: "INTEGER", nullable: false),
                    NotReadedMessage = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Surname = table.Column<string>(type: "TEXT", nullable: false),
                    Avatar = table.Column<byte[]>(type: "BLOB", nullable: true),
                    IsAvatarAdded = table.Column<bool>(type: "INTEGER", nullable: false),
                    FolderPath = table.Column<string>(type: "TEXT", nullable: false),
                    Port = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserMessages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IpAddress = table.Column<string>(type: "TEXT", nullable: false),
                    Text = table.Column<string>(type: "TEXT", nullable: false),
                    FileLength = table.Column<long>(type: "INTEGER", nullable: false),
                    IsFileAdded = table.Column<bool>(type: "INTEGER", nullable: false),
                    FileName = table.Column<string>(type: "TEXT", nullable: false),
                    IsIncoming = table.Column<bool>(type: "INTEGER", nullable: false),
                    ContactId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserMessages_Contacts_ContactId",
                        column: x => x.ContactId,
                        principalTable: "Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Settings",
                columns: new[] { "Id", "Avatar", "FolderPath", "IsAvatarAdded", "Name", "Port", "Surname" },
                values: new object[] { new Guid("3a55189e-29e5-45b5-80a2-2134c343e033"), null, "C:\\Users\\Dasha\\Pictures\\StepTemp\\UdpChat\\UdpChat", false, "UserName", 10000, "" });

            migrationBuilder.CreateIndex(
                name: "IX_UserMessages_ContactId",
                table: "UserMessages",
                column: "ContactId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DropTable(
                name: "UserMessages");

            migrationBuilder.DropTable(
                name: "Contacts");
        }
    }
}
