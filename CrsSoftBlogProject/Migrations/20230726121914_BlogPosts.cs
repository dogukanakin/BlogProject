using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrsSoftBlogProject.Migrations
{
    /// <inheritdoc />
    public partial class BlogPosts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPublished",
                table: "BlogPosts");

            migrationBuilder.RenameColumn(
                name: "FirsName",
                table: "ContactForm",
                newName: "FirstName");

            migrationBuilder.AddColumn<string>(
                name: "SelectedTag",
                table: "BlogPosts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SelectedTag",
                table: "BlogPosts");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "ContactForm",
                newName: "FirsName");

            migrationBuilder.AddColumn<bool>(
                name: "IsPublished",
                table: "BlogPosts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
