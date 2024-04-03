using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NorthwindAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    category_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    category_name = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    description = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories", x => x.category_id);
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    product_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    product_name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    supplier_id = table.Column<int>(type: "int", nullable: true),
                    category_id = table.Column<int>(type: "int", nullable: true),
                    quantity_per_unit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    unit_price = table.Column<decimal>(type: "money", nullable: true, defaultValueSql: "((0))"),
                    units_in_stock = table.Column<short>(type: "smallint", nullable: true, defaultValueSql: "((0))"),
                    units_on_order = table.Column<short>(type: "smallint", nullable: true, defaultValueSql: "((0))"),
                    reorder_level = table.Column<short>(type: "smallint", nullable: true, defaultValueSql: "((0))"),
                    discontinued = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.product_id);
                    table.ForeignKey(
                        name: "FK_products_categories",
                        column: x => x.category_id,
                        principalTable: "categories",
                        principalColumn: "category_id");
                });

            migrationBuilder.CreateIndex(
                name: "category_name",
                table: "categories",
                column: "category_name");

            migrationBuilder.CreateIndex(
                name: "categories_products",
                table: "products",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "category_id",
                table: "products",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "product_name",
                table: "products",
                column: "product_name");

            migrationBuilder.CreateIndex(
                name: "supplier_id",
                table: "products",
                column: "supplier_id");

            migrationBuilder.CreateIndex(
                name: "suppliers_products",
                table: "products",
                column: "supplier_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "categories");
        }
    }
}
