using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XMLDocCrowdSourcer.Migrations
{
    /// <inheritdoc />
    public partial class GroupProjectReference : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MappingGroups_Projects_ProjectId",
                table: "MappingGroups");

            migrationBuilder.DropIndex(
                name: "IX_MappingGroups_ProjectId",
                table: "MappingGroups");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "MappingGroups");

            migrationBuilder.AddColumn<Guid>(
                name: "ParentProjectId",
                table: "MappingGroups",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_MappingGroups_ParentProjectId",
                table: "MappingGroups",
                column: "ParentProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_MappingGroups_Projects_ParentProjectId",
                table: "MappingGroups",
                column: "ParentProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MappingGroups_Projects_ParentProjectId",
                table: "MappingGroups");

            migrationBuilder.DropIndex(
                name: "IX_MappingGroups_ParentProjectId",
                table: "MappingGroups");

            migrationBuilder.DropColumn(
                name: "ParentProjectId",
                table: "MappingGroups");

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectId",
                table: "MappingGroups",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MappingGroups_ProjectId",
                table: "MappingGroups",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_MappingGroups_Projects_ProjectId",
                table: "MappingGroups",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id");
        }
    }
}
