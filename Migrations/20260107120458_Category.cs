using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC_Pustokkk.Migrations
{
    /// <inheritdoc />
    public partial class Category : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Featureds",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Featureds_CategoryId",
                table: "Featureds",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Featureds_Categories_CategoryId",
                table: "Featureds",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Featureds_Categories_CategoryId",
                table: "Featureds");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Featureds_CategoryId",
                table: "Featureds");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Featureds");
        }
    }
}
