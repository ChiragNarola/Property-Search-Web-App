using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertySearch.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedDataInSpaceTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO ""Space"" (""PropertyId"", ""Type"", ""Size"", ""Description"") VALUES
                (28, 'Living Room', 250, 'Spacious living room'),
                (28, 'Bedroom', 150, 'Cozy bedroom'),
                (29, 'Bedroom', 120, 'Master bedroom'),
                (29, 'Kitchen', 80, 'Modern kitchen'),
                (30, 'Living Room', 200, 'Open living area'),
                (30, 'Bathroom', 50, 'Clean bathroom'),
                (31, 'Bedroom', 140, 'Bedroom'),
                (31, 'Kitchen', 70, 'Kitchen'),
                (31, 'Living Room', 180, 'Living area'),
                (33, 'Bathroom', 60, 'Bathroom'),
                (33, 'Bedroom', 130, 'Bedroom'),
                (34, 'Living Room', 210, 'Living room'),
                (34, 'Bedroom', 160, 'Bedroom'),
                (35, 'Kitchen', 90, 'Kitchen'),
                (35, 'Living Room', 190, 'Living area'),
                (35, 'Bathroom', 55, 'Bathroom'),
                (36, 'Bedroom', 150, 'Bedroom'),
                (37, 'Kitchen', 85, 'Kitchen'),
                (37, 'Living Room', 220, 'Living room'),
                (37, 'Bedroom', 140, 'Bedroom');
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
