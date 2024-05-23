using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XMLDocCrowdSourcer.Migrations
{
    /// <inheritdoc />
    public partial class TreeReferenceShifting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MappingGroups_MappingGroups_MappingGroupId",
                table: "MappingGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_MappingGroups_Projects_ParentProjectId",
                table: "MappingGroups");

            migrationBuilder.RenameColumn(
                name: "MappingGroupId",
                table: "MappingGroups",
                newName: "ParentMappingGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_MappingGroups_MappingGroupId",
                table: "MappingGroups",
                newName: "IX_MappingGroups_ParentMappingGroupId");

            migrationBuilder.AlterColumn<Guid>(
                name: "ParentProjectId",
                table: "MappingGroups",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectId",
                table: "MappingGroups",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_MappingGroups_ProjectId",
                table: "MappingGroups",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_MappingGroups_MappingGroups_ParentMappingGroupId",
                table: "MappingGroups",
                column: "ParentMappingGroupId",
                principalTable: "MappingGroups",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MappingGroups_Projects_ParentProjectId",
                table: "MappingGroups",
                column: "ParentProjectId",
                principalTable: "Projects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MappingGroups_Projects_ProjectId",
                table: "MappingGroups",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MappingGroups_MappingGroups_ParentMappingGroupId",
                table: "MappingGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_MappingGroups_Projects_ParentProjectId",
                table: "MappingGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_MappingGroups_Projects_ProjectId",
                table: "MappingGroups");

            migrationBuilder.DropIndex(
                name: "IX_MappingGroups_ProjectId",
                table: "MappingGroups");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "MappingGroups");

            migrationBuilder.RenameColumn(
                name: "ParentMappingGroupId",
                table: "MappingGroups",
                newName: "MappingGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_MappingGroups_ParentMappingGroupId",
                table: "MappingGroups",
                newName: "IX_MappingGroups_MappingGroupId");

            migrationBuilder.AlterColumn<Guid>(
                name: "ParentProjectId",
                table: "MappingGroups",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MappingGroups_MappingGroups_MappingGroupId",
                table: "MappingGroups",
                column: "MappingGroupId",
                principalTable: "MappingGroups",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MappingGroups_Projects_ParentProjectId",
                table: "MappingGroups",
                column: "ParentProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
