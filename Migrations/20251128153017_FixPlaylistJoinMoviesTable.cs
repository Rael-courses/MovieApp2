using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieAppApi.Migrations
{
    /// <inheritdoc />
    public partial class FixPlaylistJoinMoviesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlaylistJoinMovies_Playlists_PlaylistEntityId",
                table: "PlaylistJoinMovies");

            migrationBuilder.DropIndex(
                name: "IX_PlaylistJoinMovies_PlaylistEntityId",
                table: "PlaylistJoinMovies");

            migrationBuilder.DropColumn(
                name: "PlaylistEntityId",
                table: "PlaylistJoinMovies");

            migrationBuilder.AddForeignKey(
                name: "FK_PlaylistJoinMovies_Playlists_PlaylistId",
                table: "PlaylistJoinMovies",
                column: "PlaylistId",
                principalTable: "Playlists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlaylistJoinMovies_Playlists_PlaylistId",
                table: "PlaylistJoinMovies");

            migrationBuilder.AddColumn<int>(
                name: "PlaylistEntityId",
                table: "PlaylistJoinMovies",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PlaylistJoinMovies_PlaylistEntityId",
                table: "PlaylistJoinMovies",
                column: "PlaylistEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlaylistJoinMovies_Playlists_PlaylistEntityId",
                table: "PlaylistJoinMovies",
                column: "PlaylistEntityId",
                principalTable: "Playlists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
