using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projekt.Migrations
{
    public partial class InitFileEntities2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileEntities_Animals_AnimalsId",
                table: "FileEntities");

            migrationBuilder.DropColumn(
                name: "AnimalsaId",
                table: "FileEntities");

            migrationBuilder.AlterColumn<int>(
                name: "AnimalsId",
                table: "FileEntities",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FileEntities_Animals_AnimalsId",
                table: "FileEntities",
                column: "AnimalsId",
                principalTable: "Animals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileEntities_Animals_AnimalsId",
                table: "FileEntities");

            migrationBuilder.AlterColumn<int>(
                name: "AnimalsId",
                table: "FileEntities",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "AnimalsaId",
                table: "FileEntities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_FileEntities_Animals_AnimalsId",
                table: "FileEntities",
                column: "AnimalsId",
                principalTable: "Animals",
                principalColumn: "Id");
        }
    }
}
