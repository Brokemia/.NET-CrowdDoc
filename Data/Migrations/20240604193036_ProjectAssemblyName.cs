using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XMLDocCrowdSourcer.Migrations
{
    /// <inheritdoc />
    public partial class ProjectAssemblyName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AssemblyName",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssemblyName",
                table: "Projects");
        }
    }
}
