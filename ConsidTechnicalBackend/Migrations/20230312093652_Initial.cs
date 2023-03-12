using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ConsidTechnicalBackend.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsCEO = table.Column<bool>(type: "bit", nullable: false),
                    IsManager = table.Column<bool>(type: "bit", nullable: false),
                    ManagerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LibraryItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pages = table.Column<int>(type: "int", nullable: true),
                    RunTimeMinutes = table.Column<int>(type: "int", nullable: true),
                    IsBorrowable = table.Column<bool>(type: "bit", nullable: false),
                    Borrower = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BorrowDate = table.Column<DateTime>(type: "date", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LibraryItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LibraryItem_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "CategoryName" },
                values: new object[,]
                {
                    { 1, "Action" },
                    { 2, "Comedy" },
                    { 3, "Sci-Fi" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "FirstName", "IsCEO", "IsManager", "LastName", "ManagerId", "Salary" },
                values: new object[,]
                {
                    { 1, "John", true, false, "Doe", null, 50m },
                    { 2, "Alex", false, true, "Fernandez", 1, 25m },
                    { 3, "Jane", false, true, "Doe", 2, 20m },
                    { 4, "Harry", false, false, "Andersson", 2, 12m },
                    { 5, "Lucas", false, false, "Swan", 2, 15m }
                });

            migrationBuilder.InsertData(
                table: "LibraryItem",
                columns: new[] { "Id", "Author", "BorrowDate", "Borrower", "CategoryId", "IsBorrowable", "Pages", "RunTimeMinutes", "Title", "Type" },
                values: new object[,]
                {
                    { 1, "John Doe", null, null, 1, true, 50, null, "The Short Man", "Book" },
                    { 2, null, null, null, 1, true, null, 120, "The Idiot", "DVD" },
                    { 3, null, null, null, 2, true, null, 100, "The Shark", "Audio Book" },
                    { 4, "Jane Doe", null, null, 3, false, 200, null, "The cooking book", "Reference Book" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Category_CategoryName",
                table: "Category",
                column: "CategoryName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_IsCEO",
                table: "Employees",
                column: "IsCEO",
                unique: true,
                filter: "[IsCEO] = 1");

            migrationBuilder.CreateIndex(
                name: "IX_LibraryItem_CategoryId",
                table: "LibraryItem",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "LibraryItem");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
