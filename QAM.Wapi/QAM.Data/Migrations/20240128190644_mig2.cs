using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QAM.Data.Migrations
{
    /// <inheritdoc />
    public partial class mig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "InsertDate",
                table: "TagSubjects",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "InsertUserId",
                table: "TagSubjects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "TagSubjects",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "TagSubjects",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdateUserId",
                table: "TagSubjects",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InsertDate",
                table: "Favorites",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "InsertUserId",
                table: "Favorites",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Favorites",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "Favorites",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdateUserId",
                table: "Favorites",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InsertDate",
                table: "TagSubjects");

            migrationBuilder.DropColumn(
                name: "InsertUserId",
                table: "TagSubjects");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "TagSubjects");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "TagSubjects");

            migrationBuilder.DropColumn(
                name: "UpdateUserId",
                table: "TagSubjects");

            migrationBuilder.DropColumn(
                name: "InsertDate",
                table: "Favorites");

            migrationBuilder.DropColumn(
                name: "InsertUserId",
                table: "Favorites");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Favorites");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "Favorites");

            migrationBuilder.DropColumn(
                name: "UpdateUserId",
                table: "Favorites");
        }
    }
}
