using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertySearch.Data.Migrations
{
    /// <inheritdoc />
    public partial class Index : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Space_Size",
                table: "Space",
                column: "Size");

            migrationBuilder.CreateIndex(
                name: "IX_Space_Type",
                table: "Space",
                column: "Type");

            migrationBuilder.CreateIndex(
                name: "IX_Property_Price",
                table: "Property",
                column: "Price");

            migrationBuilder.CreateIndex(
                name: "IX_Property_Type",
                table: "Property",
                column: "Type");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Space_Size",
                table: "Space");

            migrationBuilder.DropIndex(
                name: "IX_Space_Type",
                table: "Space");

            migrationBuilder.DropIndex(
                name: "IX_Property_Price",
                table: "Property");

            migrationBuilder.DropIndex(
                name: "IX_Property_Type",
                table: "Property");
        }
    }
}
