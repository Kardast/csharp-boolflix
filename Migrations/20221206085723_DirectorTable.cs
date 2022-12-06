using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace csharpboolflix.Migrations
{
    /// <inheritdoc />
    public partial class DirectorTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Director",
                table: "Content");

            migrationBuilder.AddColumn<int>(
                name: "DirectorId",
                table: "Content",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Director",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Director", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Content_DirectorId",
                table: "Content",
                column: "DirectorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Content_Director_DirectorId",
                table: "Content",
                column: "DirectorId",
                principalTable: "Director",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Content_Director_DirectorId",
                table: "Content");

            migrationBuilder.DropTable(
                name: "Director");

            migrationBuilder.DropIndex(
                name: "IX_Content_DirectorId",
                table: "Content");

            migrationBuilder.DropColumn(
                name: "DirectorId",
                table: "Content");

            migrationBuilder.AddColumn<string>(
                name: "Director",
                table: "Content",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
