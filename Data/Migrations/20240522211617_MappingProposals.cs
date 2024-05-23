using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XMLDocCrowdSourcer.Migrations
{
    /// <inheritdoc />
    public partial class MappingProposals : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MappingProposals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AuthorId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    MappingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProposedValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MappingProposals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MappingProposals_AspNetUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MappingProposals_Mappings_MappingId",
                        column: x => x.MappingId,
                        principalTable: "Mappings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MappingProposals_AuthorId",
                table: "MappingProposals",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_MappingProposals_MappingId",
                table: "MappingProposals",
                column: "MappingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MappingProposals");
        }
    }
}
