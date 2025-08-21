using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertySearch.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
             migrationBuilder.Sql(@"
                INSERT INTO ""Property"" (""Address"", ""Type"", ""Price"") VALUES
                ('123 Main St', 'House', 250000),
                ('456 Oak Ave', 'Apartment', 150000),
                ('789 Pine Rd', 'Condo', 300000),
                ('321 Maple St', 'House', 200000),
                ('654 Elm St', 'Apartment', 180000),
                ('987 Cedar Ln', 'Condo', 220000),
                ('147 Spruce Dr', 'House', 270000),
                ('258 Birch Blvd', 'Apartment', 160000),
                ('369 Walnut St', 'Condo', 310000),
                ('159 Chestnut Ave', 'House', 240000);
            ");


                  
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
