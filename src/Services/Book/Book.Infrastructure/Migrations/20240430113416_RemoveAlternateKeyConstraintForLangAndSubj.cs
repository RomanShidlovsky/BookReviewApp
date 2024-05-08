using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Book.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveAlternateKeyConstraintForLangAndSubj : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Subjects_Name",
                table: "Subjects");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Languages_Name",
                table: "Languages");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddUniqueConstraint(
                name: "AK_Subjects_Name",
                table: "Subjects",
                column: "Name");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Languages_Name",
                table: "Languages",
                column: "Name");
        }
    }
}
