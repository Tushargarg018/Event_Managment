using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EM.Data.Migrations
{
    /// <inheritdoc />
    public partial class performerrelationupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_performer_organizer_organizer_id",
                table: "performer");

            migrationBuilder.DropForeignKey(
                name: "FK_venue_organizer_organizer_id",
                table: "venue");

            migrationBuilder.DropIndex(
                name: "IX_venue_organizer_id",
                table: "venue");

            migrationBuilder.DropIndex(
                name: "IX_performer_organizer_id",
                table: "performer");

            migrationBuilder.DropColumn(
                name: "organizer_id",
                table: "venue");

            migrationBuilder.DropColumn(
                name: "organizer_id",
                table: "performer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "organizer_id",
                table: "venue",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "organizer_id",
                table: "performer",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_venue_organizer_id",
                table: "venue",
                column: "organizer_id");

            migrationBuilder.CreateIndex(
                name: "IX_performer_organizer_id",
                table: "performer",
                column: "organizer_id");

            migrationBuilder.AddForeignKey(
                name: "FK_performer_organizer_organizer_id",
                table: "performer",
                column: "organizer_id",
                principalTable: "organizer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_venue_organizer_organizer_id",
                table: "venue",
                column: "organizer_id",
                principalTable: "organizer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
