using Microsoft.EntityFrameworkCore.Migrations;

namespace IPG_v2._0.Migrations
{
    public partial class categorias : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoriaId",
                table: "Projetos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    CategoriaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.CategoriaId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Projetos_CategoriaId",
                table: "Projetos",
                column: "CategoriaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projetos_Categorias_CategoriaId",
                table: "Projetos",
                column: "CategoriaId",
                principalTable: "Categorias",
                principalColumn: "CategoriaId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projetos_Categorias_CategoriaId",
                table: "Projetos");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropIndex(
                name: "IX_Projetos_CategoriaId",
                table: "Projetos");

            migrationBuilder.DropColumn(
                name: "CategoriaId",
                table: "Projetos");
        }
    }
}
