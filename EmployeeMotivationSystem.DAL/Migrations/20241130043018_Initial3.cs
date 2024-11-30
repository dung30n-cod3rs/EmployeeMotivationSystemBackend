using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeMotivationSystem.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Initial3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Positions_Positions_ParentId",
                table: "Positions");

            migrationBuilder.DropIndex(
                name: "IX_Positions_ParentId",
                table: "Positions");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Positions");

            migrationBuilder.AddColumn<int>(
                name: "Weight",
                table: "Positions",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Weight",
                table: "Positions");

            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "Positions",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Positions_ParentId",
                table: "Positions",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Positions_Positions_ParentId",
                table: "Positions",
                column: "ParentId",
                principalTable: "Positions",
                principalColumn: "Id");
        }
    }
}
