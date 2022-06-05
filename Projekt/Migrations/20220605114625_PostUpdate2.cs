using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projekt.Migrations
{
    public partial class PostUpdate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Animals_AnimalsId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_AnimalsId",
                table: "Posts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Posts_AnimalsId",
                table: "Posts",
                column: "AnimalsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Animals_AnimalsId",
                table: "Posts",
                column: "AnimalsId",
                principalTable: "Animals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
