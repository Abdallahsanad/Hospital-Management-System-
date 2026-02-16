using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital.Train.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Relationships1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WorkForId",
                table: "Consultants",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Consultants_WorkForId",
                table: "Consultants",
                column: "WorkForId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consultants_Departments_WorkForId",
                table: "Consultants",
                column: "WorkForId",
                principalTable: "Departments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consultants_Departments_WorkForId",
                table: "Consultants");

            migrationBuilder.DropIndex(
                name: "IX_Consultants_WorkForId",
                table: "Consultants");

            migrationBuilder.DropColumn(
                name: "WorkForId",
                table: "Consultants");
        }
    }
}
