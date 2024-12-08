using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SheWolf.Infrastructure.Migrations
{
    public partial class AddBookColumnToAuthor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Lägg till främmande nyckel i Book-tabellen
            migrationBuilder.AddColumn<Guid>(
                name: "AuthorId",
                table: "Books",
                nullable: false,
                defaultValue: Guid.Empty); // Eller en annan standardvärde om nödvändigt

            // Skapa en utländsk nyckelrelation
            migrationBuilder.AddForeignKey(
                name: "FK_Books_Authors_AuthorId",
                table: "Books",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Ta bort främmande nyckelrelationen om migrationen tas bort
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Authors_AuthorId",
                table: "Books");

            // Ta bort kolumnen för AuthorId
            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Books");
        }
    }
}
