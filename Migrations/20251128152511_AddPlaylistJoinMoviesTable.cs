using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieAppApi.Migrations
{
    /// <inheritdoc />
    public partial class AddPlaylistJoinMoviesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlaylistJoinMovies",
                columns: table => new
                {
                    PlaylistId = table.Column<int>(type: "INTEGER", nullable: false),
                    MovieId = table.Column<int>(type: "INTEGER", nullable: false),
                    PlaylistEntityId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaylistJoinMovies", x => new { x.PlaylistId, x.MovieId });
                    table.ForeignKey(
                        name: "FK_PlaylistJoinMovies_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlaylistJoinMovies_Playlists_PlaylistEntityId",
                        column: x => x.PlaylistEntityId,
                        principalTable: "Playlists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlaylistJoinMovies_MovieId",
                table: "PlaylistJoinMovies",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_PlaylistJoinMovies_PlaylistEntityId",
                table: "PlaylistJoinMovies",
                column: "PlaylistEntityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlaylistJoinMovies");
        }
    }
}
