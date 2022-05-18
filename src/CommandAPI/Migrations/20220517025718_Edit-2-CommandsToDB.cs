using Microsoft.EntityFrameworkCore.Migrations;

namespace CommandAPI.Migrations
{
    public partial class Edit2CommandsToDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Commands",
                table: "Commands");

            migrationBuilder.RenameTable(
                name: "Commands",
                newName: "CommandItems");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommandItems",
                table: "CommandItems",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CommandItems",
                table: "CommandItems");

            migrationBuilder.RenameTable(
                name: "CommandItems",
                newName: "Commands");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Commands",
                table: "Commands",
                column: "Id");
        }
    }
}
