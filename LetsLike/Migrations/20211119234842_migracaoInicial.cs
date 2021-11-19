using Microsoft.EntityFrameworkCore.Migrations;

namespace LetsLike.Migrations
{
    public partial class migracaoInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "USUARIO",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOME = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EMAIL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    URSERNAME = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SENHA = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USUARIO", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PROJETO",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOME = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    URL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IMAGEM = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LIKE_CONTADOR = table.Column<int>(type: "int", nullable: false),
                    IdUsuarioCadastro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PROJETO", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PROJETO_USUARIO_ID",
                        column: x => x.IdUsuarioCadastro,
                        principalTable: "USUARIO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "USUARIO_LIKE_PROJETO",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_USUARIO_LIKE = table.Column<int>(type: "int", nullable: false),
                    ID_PROJETO_LIKE = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USUARIO_LIKE_PROJETO", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PROJETO_USUARIO_LIKE_PROJETO",
                        column: x => x.ID_PROJETO_LIKE,
                        principalTable: "PROJETO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_USUARIO_USUARIO_LIKE_PROJETO",
                        column: x => x.ID_USUARIO_LIKE,
                        principalTable: "USUARIO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PROJETO_IdUsuarioCadastro",
                table: "PROJETO",
                column: "IdUsuarioCadastro");

            migrationBuilder.CreateIndex(
                name: "IX_USUARIO_LIKE_PROJETO_ID_PROJETO_LIKE",
                table: "USUARIO_LIKE_PROJETO",
                column: "ID_PROJETO_LIKE");

            migrationBuilder.CreateIndex(
                name: "IX_USUARIO_LIKE_PROJETO_ID_USUARIO_LIKE",
                table: "USUARIO_LIKE_PROJETO",
                column: "ID_USUARIO_LIKE");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "USUARIO_LIKE_PROJETO");

            migrationBuilder.DropTable(
                name: "PROJETO");

            migrationBuilder.DropTable(
                name: "USUARIO");
        }
    }
}
