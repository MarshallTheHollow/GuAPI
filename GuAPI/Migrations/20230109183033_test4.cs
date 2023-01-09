using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GuAPI.Migrations
{
    /// <inheritdoc />
    public partial class test4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_InstituteLists",
                table: "InstituteLists");

            migrationBuilder.RenameTable(
                name: "InstituteLists",
                newName: "Institutes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Institutes",
                table: "Institutes",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Institutes",
                table: "Institutes");

            migrationBuilder.RenameTable(
                name: "Institutes",
                newName: "InstituteLists");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InstituteLists",
                table: "InstituteLists",
                column: "Id");
        }
    }
}
