using Microsoft.EntityFrameworkCore.Migrations;

namespace TechnicalRadiation.WebApi.Migrations
{
    public partial class TableNameFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NewsItemAuthors_Authors_AuthorId",
                table: "NewsItemAuthors");

            migrationBuilder.DropForeignKey(
                name: "FK_NewsItemAuthors_NewsItems_NewsItemId",
                table: "NewsItemAuthors");

            migrationBuilder.DropForeignKey(
                name: "FK_NewsItemCategories_Categories_CategoryId",
                table: "NewsItemCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_NewsItemCategories_NewsItems_NewsItemId",
                table: "NewsItemCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NewsItemCategories",
                table: "NewsItemCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NewsItemAuthors",
                table: "NewsItemAuthors");

            migrationBuilder.RenameTable(
                name: "NewsItemCategories",
                newName: "CategoryNewsItem");

            migrationBuilder.RenameTable(
                name: "NewsItemAuthors",
                newName: "AuthorNewsItem");

            migrationBuilder.RenameIndex(
                name: "IX_NewsItemCategories_NewsItemId",
                table: "CategoryNewsItem",
                newName: "IX_CategoryNewsItem_NewsItemId");

            migrationBuilder.RenameIndex(
                name: "IX_NewsItemAuthors_NewsItemId",
                table: "AuthorNewsItem",
                newName: "IX_AuthorNewsItem_NewsItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryNewsItem",
                table: "CategoryNewsItem",
                columns: new[] { "CategoryId", "NewsItemId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AuthorNewsItem",
                table: "AuthorNewsItem",
                columns: new[] { "AuthorId", "NewsItemId" });

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorNewsItem_Authors_AuthorId",
                table: "AuthorNewsItem",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorNewsItem_NewsItems_NewsItemId",
                table: "AuthorNewsItem",
                column: "NewsItemId",
                principalTable: "NewsItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryNewsItem_Categories_CategoryId",
                table: "CategoryNewsItem",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryNewsItem_NewsItems_NewsItemId",
                table: "CategoryNewsItem",
                column: "NewsItemId",
                principalTable: "NewsItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthorNewsItem_Authors_AuthorId",
                table: "AuthorNewsItem");

            migrationBuilder.DropForeignKey(
                name: "FK_AuthorNewsItem_NewsItems_NewsItemId",
                table: "AuthorNewsItem");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoryNewsItem_Categories_CategoryId",
                table: "CategoryNewsItem");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoryNewsItem_NewsItems_NewsItemId",
                table: "CategoryNewsItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryNewsItem",
                table: "CategoryNewsItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AuthorNewsItem",
                table: "AuthorNewsItem");

            migrationBuilder.RenameTable(
                name: "CategoryNewsItem",
                newName: "NewsItemCategories");

            migrationBuilder.RenameTable(
                name: "AuthorNewsItem",
                newName: "NewsItemAuthors");

            migrationBuilder.RenameIndex(
                name: "IX_CategoryNewsItem_NewsItemId",
                table: "NewsItemCategories",
                newName: "IX_NewsItemCategories_NewsItemId");

            migrationBuilder.RenameIndex(
                name: "IX_AuthorNewsItem_NewsItemId",
                table: "NewsItemAuthors",
                newName: "IX_NewsItemAuthors_NewsItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NewsItemCategories",
                table: "NewsItemCategories",
                columns: new[] { "CategoryId", "NewsItemId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_NewsItemAuthors",
                table: "NewsItemAuthors",
                columns: new[] { "AuthorId", "NewsItemId" });

            migrationBuilder.AddForeignKey(
                name: "FK_NewsItemAuthors_Authors_AuthorId",
                table: "NewsItemAuthors",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NewsItemAuthors_NewsItems_NewsItemId",
                table: "NewsItemAuthors",
                column: "NewsItemId",
                principalTable: "NewsItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NewsItemCategories_Categories_CategoryId",
                table: "NewsItemCategories",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NewsItemCategories_NewsItems_NewsItemId",
                table: "NewsItemCategories",
                column: "NewsItemId",
                principalTable: "NewsItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
