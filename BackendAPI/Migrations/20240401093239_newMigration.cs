﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendAPI.Migrations
{
    public partial class newMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Workers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 226, 69, 162, 174, 135, 255, 224, 2, 134, 185, 155, 33, 139, 179, 236, 92, 216, 240, 52, 233, 32, 60, 40, 72, 208, 125, 151, 114, 143, 242, 37, 238, 190, 137, 44, 14, 71, 102, 39, 252, 116, 87, 158, 169, 133, 226, 112, 39, 86, 156, 143, 108, 122, 92, 218, 233, 63, 197, 228, 255, 191, 203, 46, 123 }, new byte[] { 25, 75, 241, 133, 182, 152, 139, 242, 147, 42, 73, 74, 198, 150, 116, 157, 86, 66, 177, 12, 91, 123, 236, 175, 247, 82, 167, 235, 182, 97, 128, 216, 80, 111, 218, 110, 245, 30, 132, 238, 246, 228, 17, 62, 185, 169, 189, 4, 21, 76, 77, 24, 232, 119, 122, 42, 209, 92, 35, 104, 236, 10, 251, 147, 78, 71, 20, 24, 171, 6, 152, 215, 232, 130, 127, 112, 184, 29, 108, 236, 31, 111, 107, 2, 54, 252, 247, 163, 154, 34, 83, 170, 215, 86, 19, 66, 181, 133, 163, 62, 73, 160, 170, 202, 127, 82, 160, 138, 176, 200, 13, 137, 71, 60, 218, 239, 6, 12, 51, 234, 237, 121, 152, 229, 4, 155, 119, 145 } });

            migrationBuilder.UpdateData(
                table: "Workers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 226, 69, 162, 174, 135, 255, 224, 2, 134, 185, 155, 33, 139, 179, 236, 92, 216, 240, 52, 233, 32, 60, 40, 72, 208, 125, 151, 114, 143, 242, 37, 238, 190, 137, 44, 14, 71, 102, 39, 252, 116, 87, 158, 169, 133, 226, 112, 39, 86, 156, 143, 108, 122, 92, 218, 233, 63, 197, 228, 255, 191, 203, 46, 123 }, new byte[] { 25, 75, 241, 133, 182, 152, 139, 242, 147, 42, 73, 74, 198, 150, 116, 157, 86, 66, 177, 12, 91, 123, 236, 175, 247, 82, 167, 235, 182, 97, 128, 216, 80, 111, 218, 110, 245, 30, 132, 238, 246, 228, 17, 62, 185, 169, 189, 4, 21, 76, 77, 24, 232, 119, 122, 42, 209, 92, 35, 104, 236, 10, 251, 147, 78, 71, 20, 24, 171, 6, 152, 215, 232, 130, 127, 112, 184, 29, 108, 236, 31, 111, 107, 2, 54, 252, 247, 163, 154, 34, 83, 170, 215, 86, 19, 66, 181, 133, 163, 62, 73, 160, 170, 202, 127, 82, 160, 138, 176, 200, 13, 137, 71, 60, 218, 239, 6, 12, 51, 234, 237, 121, 152, 229, 4, 155, 119, 145 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Workers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 48, 0, 120, 0, 69, 0, 50, 0, 52, 0, 53, 0, 65, 0, 50, 0, 65, 0, 69, 0, 56, 0, 55, 0, 70, 0, 70, 0, 69, 0, 48, 0, 48, 0, 50, 0, 56, 0, 54, 0, 66, 0, 57, 0, 57, 0, 66, 0, 50, 0, 49, 0, 56, 0, 66, 0, 66, 0, 51, 0, 69, 0, 67, 0, 53, 0, 67, 0, 68, 0, 56, 0, 70, 0, 48, 0, 51, 0, 52, 0, 69, 0, 57, 0, 50, 0, 48, 0, 51, 0, 67, 0, 50, 0, 56, 0, 52, 0, 56, 0, 68, 0, 48, 0, 55, 0, 68, 0, 57, 0, 55, 0, 55, 0, 50, 0, 56, 0, 70, 0, 70, 0, 50, 0, 50, 0, 53, 0, 69, 0, 69, 0, 66, 0, 69, 0, 56, 0, 57, 0, 50, 0, 67, 0, 48, 0, 69, 0, 52, 0, 55, 0, 54, 0, 54, 0, 50, 0, 55, 0, 70, 0, 67, 0, 55, 0, 52, 0, 53, 0, 55, 0, 57, 0, 69, 0, 65, 0, 57, 0, 56, 0, 53, 0, 69, 0, 50, 0, 55, 0, 48, 0, 50, 0, 55, 0, 53, 0, 54, 0, 57, 0, 67, 0, 56, 0, 70, 0, 54, 0, 67, 0, 55, 0, 65, 0, 53, 0, 67, 0, 68, 0, 65, 0, 69, 0, 57, 0, 51, 0, 70, 0, 67, 0, 53, 0, 69, 0, 52, 0, 70, 0, 70, 0, 66, 0, 70, 0, 67, 0, 66, 0, 50, 0, 69, 0, 55, 0, 66, 0 }, new byte[] { 48, 0, 120, 0, 49, 0, 57, 0, 52, 0, 66, 0, 70, 0, 49, 0, 56, 0, 53, 0, 66, 0, 54, 0, 57, 0, 56, 0, 56, 0, 66, 0, 70, 0, 50, 0, 57, 0, 51, 0, 50, 0, 65, 0, 52, 0, 57, 0, 52, 0, 65, 0, 67, 0, 54, 0, 57, 0, 54, 0, 55, 0, 52, 0, 57, 0, 68, 0, 53, 0, 54, 0, 52, 0, 50, 0, 66, 0, 49, 0, 48, 0, 67, 0, 53, 0, 66, 0, 55, 0, 66, 0, 69, 0, 67, 0, 65, 0, 70, 0, 70, 0, 55, 0, 53, 0, 50, 0, 65, 0, 55, 0, 69, 0, 66, 0, 66, 0, 54, 0, 54, 0, 49, 0, 56, 0, 48, 0, 68, 0, 56, 0, 53, 0, 48, 0, 54, 0, 70, 0, 68, 0, 65, 0, 54, 0, 69, 0, 70, 0, 53, 0, 49, 0, 69, 0, 56, 0, 52, 0, 69, 0, 69, 0, 70, 0, 54, 0, 69, 0, 52, 0, 49, 0, 49, 0, 51, 0, 69, 0, 66, 0, 57, 0, 65, 0, 57, 0, 66, 0, 68, 0, 48, 0, 52, 0, 49, 0, 53, 0, 52, 0, 67, 0, 52, 0, 68, 0, 49, 0, 56, 0, 69, 0, 56, 0, 55, 0, 55, 0, 55, 0, 65, 0, 50, 0, 65, 0, 68, 0, 49, 0, 53, 0, 67, 0, 50, 0, 51, 0, 54, 0, 56, 0, 69, 0, 67, 0, 48, 0, 65, 0, 70, 0, 66, 0, 57, 0, 51, 0, 52, 0, 69, 0, 52, 0, 55, 0, 49, 0, 52, 0, 49, 0, 56, 0, 65, 0, 66, 0, 48, 0, 54, 0, 57, 0, 56, 0, 68, 0, 55, 0, 69, 0, 56, 0, 56, 0, 50, 0, 55, 0, 70, 0, 55, 0, 48, 0, 66, 0, 56, 0, 49, 0, 68, 0, 54, 0, 67, 0, 69, 0, 67, 0, 49, 0, 70, 0, 54, 0, 70, 0, 54, 0, 66, 0, 48, 0, 50, 0, 51, 0, 54, 0, 70, 0, 67, 0, 70, 0, 55, 0, 65, 0, 51, 0, 57, 0, 65, 0, 50, 0, 50, 0, 53, 0, 51, 0, 65, 0, 65, 0, 68, 0, 55, 0, 53, 0, 54, 0, 49, 0, 51, 0, 52, 0, 50, 0, 66, 0, 53, 0, 56, 0, 53, 0, 65, 0, 51, 0, 51, 0, 69, 0, 52, 0, 57, 0, 65, 0, 48, 0, 65, 0, 65, 0, 67, 0, 65, 0, 55, 0, 70, 0, 53, 0, 50, 0, 65, 0, 48, 0, 56, 0, 65, 0, 66, 0, 48, 0, 67, 0, 56, 0, 48, 0, 68, 0, 56, 0, 57, 0, 52, 0, 55, 0, 51, 0, 67, 0, 68, 0, 65, 0, 69, 0, 70, 0, 48, 0, 54, 0, 48, 0, 67, 0, 51, 0, 51, 0, 69, 0, 65, 0, 69, 0, 68, 0, 55, 0, 57, 0, 57, 0, 56, 0, 69, 0, 53, 0, 48, 0, 52, 0, 57, 0, 66, 0, 55, 0, 55, 0, 57, 0, 49, 0 } });

            migrationBuilder.UpdateData(
                table: "Workers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 48, 0, 120, 0, 69, 0, 50, 0, 52, 0, 53, 0, 65, 0, 50, 0, 65, 0, 69, 0, 56, 0, 55, 0, 70, 0, 70, 0, 69, 0, 48, 0, 48, 0, 50, 0, 56, 0, 54, 0, 66, 0, 57, 0, 57, 0, 66, 0, 50, 0, 49, 0, 56, 0, 66, 0, 66, 0, 51, 0, 69, 0, 67, 0, 53, 0, 67, 0, 68, 0, 56, 0, 70, 0, 48, 0, 51, 0, 52, 0, 69, 0, 57, 0, 50, 0, 48, 0, 51, 0, 67, 0, 50, 0, 56, 0, 52, 0, 56, 0, 68, 0, 48, 0, 55, 0, 68, 0, 57, 0, 55, 0, 55, 0, 50, 0, 56, 0, 70, 0, 70, 0, 50, 0, 50, 0, 53, 0, 69, 0, 69, 0, 66, 0, 69, 0, 56, 0, 57, 0, 50, 0, 67, 0, 48, 0, 69, 0, 52, 0, 55, 0, 54, 0, 54, 0, 50, 0, 55, 0, 70, 0, 67, 0, 55, 0, 52, 0, 53, 0, 55, 0, 57, 0, 69, 0, 65, 0, 57, 0, 56, 0, 53, 0, 69, 0, 50, 0, 55, 0, 48, 0, 50, 0, 55, 0, 53, 0, 54, 0, 57, 0, 67, 0, 56, 0, 70, 0, 54, 0, 67, 0, 55, 0, 65, 0, 53, 0, 67, 0, 68, 0, 65, 0, 69, 0, 57, 0, 51, 0, 70, 0, 67, 0, 53, 0, 69, 0, 52, 0, 70, 0, 70, 0, 66, 0, 70, 0, 67, 0, 66, 0, 50, 0, 69, 0, 55, 0, 66, 0 }, new byte[] { 48, 0, 120, 0, 49, 0, 57, 0, 52, 0, 66, 0, 70, 0, 49, 0, 56, 0, 53, 0, 66, 0, 54, 0, 57, 0, 56, 0, 56, 0, 66, 0, 70, 0, 50, 0, 57, 0, 51, 0, 50, 0, 65, 0, 52, 0, 57, 0, 52, 0, 65, 0, 67, 0, 54, 0, 57, 0, 54, 0, 55, 0, 52, 0, 57, 0, 68, 0, 53, 0, 54, 0, 52, 0, 50, 0, 66, 0, 49, 0, 48, 0, 67, 0, 53, 0, 66, 0, 55, 0, 66, 0, 69, 0, 67, 0, 65, 0, 70, 0, 70, 0, 55, 0, 53, 0, 50, 0, 65, 0, 55, 0, 69, 0, 66, 0, 66, 0, 54, 0, 54, 0, 49, 0, 56, 0, 48, 0, 68, 0, 56, 0, 53, 0, 48, 0, 54, 0, 70, 0, 68, 0, 65, 0, 54, 0, 69, 0, 70, 0, 53, 0, 49, 0, 69, 0, 56, 0, 52, 0, 69, 0, 69, 0, 70, 0, 54, 0, 69, 0, 52, 0, 49, 0, 49, 0, 51, 0, 69, 0, 66, 0, 57, 0, 65, 0, 57, 0, 66, 0, 68, 0, 48, 0, 52, 0, 49, 0, 53, 0, 52, 0, 67, 0, 52, 0, 68, 0, 49, 0, 56, 0, 69, 0, 56, 0, 55, 0, 55, 0, 55, 0, 65, 0, 50, 0, 65, 0, 68, 0, 49, 0, 53, 0, 67, 0, 50, 0, 51, 0, 54, 0, 56, 0, 69, 0, 67, 0, 48, 0, 65, 0, 70, 0, 66, 0, 57, 0, 51, 0, 52, 0, 69, 0, 52, 0, 55, 0, 49, 0, 52, 0, 49, 0, 56, 0, 65, 0, 66, 0, 48, 0, 54, 0, 57, 0, 56, 0, 68, 0, 55, 0, 69, 0, 56, 0, 56, 0, 50, 0, 55, 0, 70, 0, 55, 0, 48, 0, 66, 0, 56, 0, 49, 0, 68, 0, 54, 0, 67, 0, 69, 0, 67, 0, 49, 0, 70, 0, 54, 0, 70, 0, 54, 0, 66, 0, 48, 0, 50, 0, 51, 0, 54, 0, 70, 0, 67, 0, 70, 0, 55, 0, 65, 0, 51, 0, 57, 0, 65, 0, 50, 0, 50, 0, 53, 0, 51, 0, 65, 0, 65, 0, 68, 0, 55, 0, 53, 0, 54, 0, 49, 0, 51, 0, 52, 0, 50, 0, 66, 0, 53, 0, 56, 0, 53, 0, 65, 0, 51, 0, 51, 0, 69, 0, 52, 0, 57, 0, 65, 0, 48, 0, 65, 0, 65, 0, 67, 0, 65, 0, 55, 0, 70, 0, 53, 0, 50, 0, 65, 0, 48, 0, 56, 0, 65, 0, 66, 0, 48, 0, 67, 0, 56, 0, 48, 0, 68, 0, 56, 0, 57, 0, 52, 0, 55, 0, 51, 0, 67, 0, 68, 0, 65, 0, 69, 0, 70, 0, 48, 0, 54, 0, 48, 0, 67, 0, 51, 0, 51, 0, 69, 0, 65, 0, 69, 0, 68, 0, 55, 0, 57, 0, 57, 0, 56, 0, 69, 0, 53, 0, 48, 0, 52, 0, 57, 0, 66, 0, 55, 0, 55, 0, 57, 0, 49, 0 } });
        }
    }
}