using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EM.Data.Migrations
{
    /// <inheritdoc />
    public partial class latest1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "file_type",
                table: "event_document",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateIndex(
                name: "IX_venue_city",
                table: "venue",
                column: "city");

            migrationBuilder.CreateIndex(
                name: "IX_venue_state",
                table: "venue",
                column: "state");

            migrationBuilder.AddForeignKey(
                name: "FK_venue_city_city",
                table: "venue",
                column: "city",
                principalTable: "city",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_venue_state_state",
                table: "venue",
                column: "state",
                principalTable: "state",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_venue_city_city",
                table: "venue");

            migrationBuilder.DropForeignKey(
                name: "FK_venue_state_state",
                table: "venue");

            migrationBuilder.DropIndex(
                name: "IX_venue_city",
                table: "venue");

            migrationBuilder.DropIndex(
                name: "IX_venue_state",
                table: "venue");

            migrationBuilder.AlterColumn<string>(
                name: "file_type",
                table: "event_document",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
