using Microsoft.EntityFrameworkCore.Migrations;

namespace TechnicalRadiation.WebApi.Migrations
{
    public partial class EntityItemNamesFixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<int>(
                name: "NewsItemId",
                table: "CategoryNewsItem",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "CategoryNewsItem",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<int>(
                name: "CategoriesId",
                table: "CategoryNewsItem",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NewsItemsId",
                table: "CategoryNewsItem",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "NewsItemId",
                table: "AuthorNewsItem",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "AuthorId",
                table: "AuthorNewsItem",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<int>(
                name: "AuthorsId",
                table: "AuthorNewsItem",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NewsItemsId",
                table: "AuthorNewsItem",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryNewsItem",
                table: "CategoryNewsItem",
                columns: new[] { "CategoriesId", "NewsItemsId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AuthorNewsItem",
                table: "AuthorNewsItem",
                columns: new[] { "AuthorsId", "NewsItemsId" });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryNewsItem_CategoryId",
                table: "CategoryNewsItem",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_AuthorNewsItem_AuthorId",
                table: "AuthorNewsItem",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorNewsItem_Authors_AuthorId",
                table: "AuthorNewsItem",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorNewsItem_NewsItems_NewsItemId",
                table: "AuthorNewsItem",
                column: "NewsItemId",
                principalTable: "NewsItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryNewsItem_Categories_CategoryId",
                table: "CategoryNewsItem",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryNewsItem_NewsItems_NewsItemId",
                table: "CategoryNewsItem",
                column: "NewsItemId",
                principalTable: "NewsItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
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

            migrationBuilder.DropIndex(
                name: "IX_CategoryNewsItem_CategoryId",
                table: "CategoryNewsItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AuthorNewsItem",
                table: "AuthorNewsItem");

            migrationBuilder.DropIndex(
                name: "IX_AuthorNewsItem_AuthorId",
                table: "AuthorNewsItem");

            migrationBuilder.DropColumn(
                name: "CategoriesId",
                table: "CategoryNewsItem");

            migrationBuilder.DropColumn(
                name: "NewsItemsId",
                table: "CategoryNewsItem");

            migrationBuilder.DropColumn(
                name: "AuthorsId",
                table: "AuthorNewsItem");

            migrationBuilder.DropColumn(
                name: "NewsItemsId",
                table: "AuthorNewsItem");

            migrationBuilder.AlterColumn<int>(
                name: "NewsItemId",
                table: "CategoryNewsItem",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "CategoryNewsItem",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NewsItemId",
                table: "AuthorNewsItem",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AuthorId",
                table: "AuthorNewsItem",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

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
    }
}
