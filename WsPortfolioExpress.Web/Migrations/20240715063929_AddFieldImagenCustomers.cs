using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WsPortfolioExpress.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldImagenCustomers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Imagen",
                table: "Customers",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Imagen",
                table: "Customers");
        }
    }
}
