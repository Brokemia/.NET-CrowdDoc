using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XMLDocCrowdSourcer.Migrations
{
    /// <inheritdoc />
    public partial class ProposalDateAndProject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "MappingProposals",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectId",
                table: "MappingProposals",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_MappingProposals_ProjectId",
                table: "MappingProposals",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_MappingProposals_Projects_ProjectId",
                table: "MappingProposals",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MappingProposals_Projects_ProjectId",
                table: "MappingProposals");

            migrationBuilder.DropIndex(
                name: "IX_MappingProposals_ProjectId",
                table: "MappingProposals");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "MappingProposals");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "MappingProposals");
        }
    }
}
