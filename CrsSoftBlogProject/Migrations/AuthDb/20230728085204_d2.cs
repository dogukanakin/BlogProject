using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrsSoftBlogProject.Migrations.AuthDb
{
    /// <inheritdoc />
    public partial class d2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "953a266f-233b-483f-9923-b042c86360ea",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6654fd13-6cc3-4053-aa4b-b95c2746231a", "AQAAAAIAAYagAAAAEMKkicg/DNsk4U1p2KODrg/w45b9gv/ITsrRPptLxYx9qhkqpFtVveUHGMb0aO5l2Q==", "1ac0c00c-5a91-4bc7-9022-a1739ec49218" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "953a266f-233b-483f-9923-b042c86360ea",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5adfff62-1055-484d-9265-ab01abf85863", "AQAAAAIAAYagAAAAEBkAKKH9o54ICGeAKgb7LZiXpW8BBj8H8P/o7LIWlXw6dPXtnO/QD9gvtjFVDtRx0A==", "56dd3670-3d81-4e58-90b2-1b311925d18f" });
        }
    }
}
