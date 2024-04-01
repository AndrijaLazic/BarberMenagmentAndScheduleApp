using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendAPI.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkerTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkerType = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkerTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Workers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    LastName = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    PhoneNumber = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    WorkerTypeId = table.Column<int>(type: "int", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Workers_WorkerTypes",
                        column: x => x.WorkerTypeId,
                        principalTable: "WorkerTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WorkerCommunication",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    User1 = table.Column<int>(type: "int", nullable: false),
                    User2 = table.Column<int>(type: "int", nullable: false),
                    UnreadMessages = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkerCommunication", x => x.ID);
                    table.ForeignKey(
                        name: "FK_WorkerCommunication_Workers",
                        column: x => x.User1,
                        principalTable: "Workers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkerMessages",
                columns: table => new
                {
                    CommunicationID = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_WorkerMessages_WorkerCommunication",
                        column: x => x.CommunicationID,
                        principalTable: "WorkerCommunication",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "WorkerTypes",
                columns: new[] { "Id", "WorkerType" },
                values: new object[] { 1, "Menadzer" });

            migrationBuilder.InsertData(
                table: "WorkerTypes",
                columns: new[] { "Id", "WorkerType" },
                values: new object[] { 2, "Frizer" });

            migrationBuilder.InsertData(
                table: "Workers",
                columns: new[] { "Id", "Email", "LastName", "Name", "PasswordHash", "PasswordSalt", "PhoneNumber", "WorkerTypeId" },
                values: new object[] { 1, "andrija223@yahoo.com", "Lazic", "andrija", new byte[] { 48, 120, 69, 50, 52, 53, 65, 50, 65, 69, 56, 55, 70, 70, 69, 48, 48, 50, 56, 54, 66, 57, 57, 66, 50, 49, 56, 66, 66, 51, 69, 67, 53, 67, 68, 56, 70, 48, 51, 52, 69, 57, 50, 48, 51, 67, 50, 56, 52, 56, 68, 48, 55, 68, 57, 55, 55, 50, 56, 70, 70, 50, 50, 53, 69, 69, 66, 69, 56, 57, 50, 67, 48, 69, 52, 55, 54, 54, 50, 55, 70, 67, 55, 52, 53, 55, 57, 69, 65, 57, 56, 53, 69, 50, 55, 48, 50, 55, 53, 54, 57, 67, 56, 70, 54, 67, 55, 65, 53, 67, 68, 65, 69, 57, 51, 70, 67, 53, 69, 52, 70, 70, 66, 70, 67, 66, 50, 69, 55, 66 }, new byte[] { 48, 120, 49, 57, 52, 66, 70, 49, 56, 53, 66, 54, 57, 56, 56, 66, 70, 50, 57, 51, 50, 65, 52, 57, 52, 65, 67, 54, 57, 54, 55, 52, 57, 68, 53, 54, 52, 50, 66, 49, 48, 67, 53, 66, 55, 66, 69, 67, 65, 70, 70, 55, 53, 50, 65, 55, 69, 66, 66, 54, 54, 49, 56, 48, 68, 56, 53, 48, 54, 70, 68, 65, 54, 69, 70, 53, 49, 69, 56, 52, 69, 69, 70, 54, 69, 52, 49, 49, 51, 69, 66, 57, 65, 57, 66, 68, 48, 52, 49, 53, 52, 67, 52, 68, 49, 56, 69, 56, 55, 55, 55, 65, 50, 65, 68, 49, 53, 67, 50, 51, 54, 56, 69, 67, 48, 65, 70, 66, 57, 51, 52, 69, 52, 55, 49, 52, 49, 56, 65, 66, 48, 54, 57, 56, 68, 55, 69, 56, 56, 50, 55, 70, 55, 48, 66, 56, 49, 68, 54, 67, 69, 67, 49, 70, 54, 70, 54, 66, 48, 50, 51, 54, 70, 67, 70, 55, 65, 51, 57, 65, 50, 50, 53, 51, 65, 65, 68, 55, 53, 54, 49, 51, 52, 50, 66, 53, 56, 53, 65, 51, 51, 69, 52, 57, 65, 48, 65, 65, 67, 65, 55, 70, 53, 50, 65, 48, 56, 65, 66, 48, 67, 56, 48, 68, 56, 57, 52, 55, 51, 67, 68, 65, 69, 70, 48, 54, 48, 67, 51, 51, 69, 65, 69, 68, 55, 57, 57, 56, 69, 53, 48, 52, 57, 66, 55, 55, 57, 49 }, "0695561004", 1 });

            migrationBuilder.InsertData(
                table: "Workers",
                columns: new[] { "Id", "Email", "LastName", "Name", "PasswordHash", "PasswordSalt", "PhoneNumber", "WorkerTypeId" },
                values: new object[] { 2, "brzi223@yahoo.com", "Brzi", "Jovan", new byte[] { 48, 120, 69, 50, 52, 53, 65, 50, 65, 69, 56, 55, 70, 70, 69, 48, 48, 50, 56, 54, 66, 57, 57, 66, 50, 49, 56, 66, 66, 51, 69, 67, 53, 67, 68, 56, 70, 48, 51, 52, 69, 57, 50, 48, 51, 67, 50, 56, 52, 56, 68, 48, 55, 68, 57, 55, 55, 50, 56, 70, 70, 50, 50, 53, 69, 69, 66, 69, 56, 57, 50, 67, 48, 69, 52, 55, 54, 54, 50, 55, 70, 67, 55, 52, 53, 55, 57, 69, 65, 57, 56, 53, 69, 50, 55, 48, 50, 55, 53, 54, 57, 67, 56, 70, 54, 67, 55, 65, 53, 67, 68, 65, 69, 57, 51, 70, 67, 53, 69, 52, 70, 70, 66, 70, 67, 66, 50, 69, 55, 66 }, new byte[] { 48, 120, 49, 57, 52, 66, 70, 49, 56, 53, 66, 54, 57, 56, 56, 66, 70, 50, 57, 51, 50, 65, 52, 57, 52, 65, 67, 54, 57, 54, 55, 52, 57, 68, 53, 54, 52, 50, 66, 49, 48, 67, 53, 66, 55, 66, 69, 67, 65, 70, 70, 55, 53, 50, 65, 55, 69, 66, 66, 54, 54, 49, 56, 48, 68, 56, 53, 48, 54, 70, 68, 65, 54, 69, 70, 53, 49, 69, 56, 52, 69, 69, 70, 54, 69, 52, 49, 49, 51, 69, 66, 57, 65, 57, 66, 68, 48, 52, 49, 53, 52, 67, 52, 68, 49, 56, 69, 56, 55, 55, 55, 65, 50, 65, 68, 49, 53, 67, 50, 51, 54, 56, 69, 67, 48, 65, 70, 66, 57, 51, 52, 69, 52, 55, 49, 52, 49, 56, 65, 66, 48, 54, 57, 56, 68, 55, 69, 56, 56, 50, 55, 70, 55, 48, 66, 56, 49, 68, 54, 67, 69, 67, 49, 70, 54, 70, 54, 66, 48, 50, 51, 54, 70, 67, 70, 55, 65, 51, 57, 65, 50, 50, 53, 51, 65, 65, 68, 55, 53, 54, 49, 51, 52, 50, 66, 53, 56, 53, 65, 51, 51, 69, 52, 57, 65, 48, 65, 65, 67, 65, 55, 70, 53, 50, 65, 48, 56, 65, 66, 48, 67, 56, 48, 68, 56, 57, 52, 55, 51, 67, 68, 65, 69, 70, 48, 54, 48, 67, 51, 51, 69, 65, 69, 68, 55, 57, 57, 56, 69, 53, 48, 52, 57, 66, 55, 55, 57, 49 }, "0695561004", 2 });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_PhoneNumber",
                table: "Users",
                column: "PhoneNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkerCommunication",
                table: "WorkerCommunication",
                columns: new[] { "User1", "User2" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkerMessages_CommunicationID",
                table: "WorkerMessages",
                column: "CommunicationID");

            migrationBuilder.CreateIndex(
                name: "IX_Workers_WorkerTypeId",
                table: "Workers",
                column: "WorkerTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "WorkerMessages");

            migrationBuilder.DropTable(
                name: "WorkerCommunication");

            migrationBuilder.DropTable(
                name: "Workers");

            migrationBuilder.DropTable(
                name: "WorkerTypes");
        }
    }
}
