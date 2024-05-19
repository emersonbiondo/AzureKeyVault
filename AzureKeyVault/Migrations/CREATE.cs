using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AzureKeyVault.Migrations
{
    /// <inheritdoc />
    public partial class CREATE : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Samples",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Samples", x => x.ID);
                });

            migrationBuilder.InsertData(
                table: "Samples",
                columns: new[] { "ID", "Description", "Name" },
                values: new object[] { 1L, "Test", "Test" });

            migrationBuilder.CreateIndex(
                name: "IX_Sample",
                table: "Samples",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Samples");
        }
    }
}
