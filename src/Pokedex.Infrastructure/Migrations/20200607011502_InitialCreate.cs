using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pokedex.Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_POKEMONS",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Order = table.Column<int>(nullable: false),
                    Height = table.Column<int>(nullable: false),
                    Weight = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateUpdated = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_POKEMONS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TB_TYPES",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_TYPES", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TB_POKEMONTYPE",
                columns: table => new
                {
                    PokemonId = table.Column<int>(nullable: false),
                    TypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_POKEMONTYPE", x => new { x.PokemonId, x.TypeId });
                    table.ForeignKey(
                        name: "FK_TB_POKEMONTYPE_TB_POKEMONS_PokemonId",
                        column: x => x.PokemonId,
                        principalTable: "TB_POKEMONS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_POKEMONTYPE_TB_TYPES_TypeId",
                        column: x => x.TypeId,
                        principalTable: "TB_TYPES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_POKEMONTYPE_TypeId",
                table: "TB_POKEMONTYPE",
                column: "TypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_POKEMONTYPE");

            migrationBuilder.DropTable(
                name: "TB_POKEMONS");

            migrationBuilder.DropTable(
                name: "TB_TYPES");
        }
    }
}
