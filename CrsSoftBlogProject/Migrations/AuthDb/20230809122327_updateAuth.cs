using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrsSoftBlogProject.Migrations.AuthDb
{
    /// <inheritdoc />
    public partial class updateAuth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "953a266f-233b-483f-9923-b042c86360ea",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "52f6a956-fdf1-430e-8512-a8e0930701d5", "AQAAAAIAAYagAAAAEGu4bTrouLl4vNe/DXFuMHW7CG0pwSCrLwKxOkaN3Yy9sfySFY8NcSRR1ld/Mhr/NA==", "f274fcd6-bf1e-4e82-b111-2cc523cb6add" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "953a266f-233b-483f-9923-b042c86360ea",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b1a3768c-2d04-489a-a5eb-9083634fb636", "AQAAAAIAAYagAAAAECHQlPwUH0J8HTG7zyaJZ7cZobcq2ndl+y50qVq6dHbiWN+Ldvy1G7uAG3yySTjIcQ==", "645a3d5e-f03d-468f-be41-78bc4000ac35" });
        }
    }
}
