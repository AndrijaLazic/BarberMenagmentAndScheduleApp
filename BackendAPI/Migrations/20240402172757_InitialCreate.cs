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
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommunicationID = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "text", nullable: false),
                    SenderID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkerMessages", x => x.Id);
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
                values: new object[] { 1, "andrija223@yahoo.com", "Lazic", "andrija", new byte[] { 226, 69, 162, 174, 135, 255, 224, 2, 134, 185, 155, 33, 139, 179, 236, 92, 216, 240, 52, 233, 32, 60, 40, 72, 208, 125, 151, 114, 143, 242, 37, 238, 190, 137, 44, 14, 71, 102, 39, 252, 116, 87, 158, 169, 133, 226, 112, 39, 86, 156, 143, 108, 122, 92, 218, 233, 63, 197, 228, 255, 191, 203, 46, 123 }, new byte[] { 25, 75, 241, 133, 182, 152, 139, 242, 147, 42, 73, 74, 198, 150, 116, 157, 86, 66, 177, 12, 91, 123, 236, 175, 247, 82, 167, 235, 182, 97, 128, 216, 80, 111, 218, 110, 245, 30, 132, 238, 246, 228, 17, 62, 185, 169, 189, 4, 21, 76, 77, 24, 232, 119, 122, 42, 209, 92, 35, 104, 236, 10, 251, 147, 78, 71, 20, 24, 171, 6, 152, 215, 232, 130, 127, 112, 184, 29, 108, 236, 31, 111, 107, 2, 54, 252, 247, 163, 154, 34, 83, 170, 215, 86, 19, 66, 181, 133, 163, 62, 73, 160, 170, 202, 127, 82, 160, 138, 176, 200, 13, 137, 71, 60, 218, 239, 6, 12, 51, 234, 237, 121, 152, 229, 4, 155, 119, 145 }, "0695561004", 1 });

            migrationBuilder.InsertData(
                table: "Workers",
                columns: new[] { "Id", "Email", "LastName", "Name", "PasswordHash", "PasswordSalt", "PhoneNumber", "WorkerTypeId" },
                values: new object[] { 2, "brzi223@yahoo.com", "Brzi", "Jovan", new byte[] { 226, 69, 162, 174, 135, 255, 224, 2, 134, 185, 155, 33, 139, 179, 236, 92, 216, 240, 52, 233, 32, 60, 40, 72, 208, 125, 151, 114, 143, 242, 37, 238, 190, 137, 44, 14, 71, 102, 39, 252, 116, 87, 158, 169, 133, 226, 112, 39, 86, 156, 143, 108, 122, 92, 218, 233, 63, 197, 228, 255, 191, 203, 46, 123 }, new byte[] { 25, 75, 241, 133, 182, 152, 139, 242, 147, 42, 73, 74, 198, 150, 116, 157, 86, 66, 177, 12, 91, 123, 236, 175, 247, 82, 167, 235, 182, 97, 128, 216, 80, 111, 218, 110, 245, 30, 132, 238, 246, 228, 17, 62, 185, 169, 189, 4, 21, 76, 77, 24, 232, 119, 122, 42, 209, 92, 35, 104, 236, 10, 251, 147, 78, 71, 20, 24, 171, 6, 152, 215, 232, 130, 127, 112, 184, 29, 108, 236, 31, 111, 107, 2, 54, 252, 247, 163, 154, 34, 83, 170, 215, 86, 19, 66, 181, 133, 163, 62, 73, 160, 170, 202, 127, 82, 160, 138, 176, 200, 13, 137, 71, 60, 218, 239, 6, 12, 51, 234, 237, 121, 152, 229, 4, 155, 119, 145 }, "0695561004", 2 });

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
