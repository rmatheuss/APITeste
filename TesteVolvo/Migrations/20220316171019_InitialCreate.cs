using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TesteVolvo.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Modelos",
                columns: table => new
                {
                    IdModelo = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Ativo = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modelos", x => x.IdModelo);
                });

            migrationBuilder.CreateTable(
                name: "Caminhoes",
                columns: table => new
                {
                    IdCaminhao = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    AnoFabricacao = table.Column<int>(type: "INTEGER", nullable: false),
                    AnoModelo = table.Column<int>(type: "INTEGER", nullable: false),
                    IdModelo = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Caminhoes", x => x.IdCaminhao);
                    table.ForeignKey(
                        name: "FK_Caminhoes_Modelos_IdModelo",
                        column: x => x.IdModelo,
                        principalTable: "Modelos",
                        principalColumn: "IdModelo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Modelos",
                columns: new[] { "IdModelo", "Ativo", "Nome" },
                values: new object[] { 1, true, "FH" });

            migrationBuilder.InsertData(
                table: "Modelos",
                columns: new[] { "IdModelo", "Ativo", "Nome" },
                values: new object[] { 2, true, "FM" });

            migrationBuilder.CreateIndex(
                name: "IX_Caminhoes_IdModelo",
                table: "Caminhoes",
                column: "IdModelo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Caminhoes");

            migrationBuilder.DropTable(
                name: "Modelos");
        }
    }
}
