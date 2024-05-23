using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XMLDocCrowdSourcer.Migrations
{
    /// <inheritdoc />
    public partial class BasicProjectStructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MappingGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MappingGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MappingGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MappingGroups_MappingGroups_MappingGroupId",
                        column: x => x.MappingGroupId,
                        principalTable: "MappingGroups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MappingGroups_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Mappings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    XmlDocId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Documentation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MappingGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mappings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mappings_MappingGroups_MappingGroupId",
                        column: x => x.MappingGroupId,
                        principalTable: "MappingGroups",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MappingGroups_MappingGroupId",
                table: "MappingGroups",
                column: "MappingGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_MappingGroups_ProjectId",
                table: "MappingGroups",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Mappings_MappingGroupId",
                table: "Mappings",
                column: "MappingGroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mappings");

            migrationBuilder.DropTable(
                name: "MappingGroups");

            migrationBuilder.DropTable(
                name: "Projects");
        }
    }
}
