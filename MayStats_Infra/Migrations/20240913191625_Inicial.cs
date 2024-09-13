using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MayStats_Infra.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Stats",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Weigth = table.Column<float>(type: "float", nullable: false),
                    IMC = table.Column<float>(type: "float", nullable: false),
                    FatPercentage = table.Column<float>(type: "float", nullable: false),
                    FatWeight = table.Column<float>(type: "float", nullable: false),
                    SkeletalMuscleMassPercentage = table.Column<float>(type: "float", nullable: false),
                    WeightSkeletalMuscleMassPercentage = table.Column<float>(type: "float", nullable: false),
                    MuscleMassRecordPercentage = table.Column<float>(type: "float", nullable: false),
                    WaterPercentage = table.Column<float>(type: "float", nullable: false),
                    WaterWeight = table.Column<float>(type: "float", nullable: false),
                    VisceralFat = table.Column<float>(type: "float", nullable: false),
                    Bones = table.Column<float>(type: "float", nullable: false),
                    Metabolism = table.Column<float>(type: "float", nullable: false),
                    ProteinPercentage = table.Column<float>(type: "float", nullable: false),
                    ObesityPercentage = table.Column<float>(type: "float", nullable: false),
                    MetabolicAge = table.Column<float>(type: "float", nullable: false),
                    LBM = table.Column<float>(type: "float", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stats", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stats");
        }
    }
}
