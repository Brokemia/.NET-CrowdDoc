using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XMLDocCrowdSourcer.Migrations
{
    /// <inheritdoc />
    public partial class AllObjectsAreGroups : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mappings_MappingGroups_MappingGroupId",
                table: "Mappings");

            migrationBuilder.DropIndex(
                name: "IX_Mappings_MappingGroupId",
                table: "Mappings");

            migrationBuilder.DropColumn(
                name: "MappingGroupId",
                table: "Mappings");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Mappings");

            migrationBuilder.AddColumn<Guid>(
                name: "MappingId",
                table: "MappingGroups",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MappingGroups_MappingId",
                table: "MappingGroups",
                column: "MappingId");

            migrationBuilder.AddForeignKey(
                name: "FK_MappingGroups_Mappings_MappingId",
                table: "MappingGroups",
                column: "MappingId",
                principalTable: "Mappings",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MappingGroups_Mappings_MappingId",
                table: "MappingGroups");

            migrationBuilder.DropIndex(
                name: "IX_MappingGroups_MappingId",
                table: "MappingGroups");

            migrationBuilder.DropColumn(
                name: "MappingId",
                table: "MappingGroups");

            migrationBuilder.AddColumn<Guid>(
                name: "MappingGroupId",
                table: "Mappings",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Mappings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Mappings_MappingGroupId",
                table: "Mappings",
                column: "MappingGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Mappings_MappingGroups_MappingGroupId",
                table: "Mappings",
                column: "MappingGroupId",
                principalTable: "MappingGroups",
                principalColumn: "Id");
        }
    }
}
