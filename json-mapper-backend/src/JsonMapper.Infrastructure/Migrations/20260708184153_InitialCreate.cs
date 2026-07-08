using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JsonMapper.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "json_mappings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_json_mappings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "json_field_mappings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    JsonMappingId = table.Column<Guid>(type: "uuid", nullable: false),
                    SourceField = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    TargetField = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_json_field_mappings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_json_field_mappings_json_mappings_JsonMappingId",
                        column: x => x.JsonMappingId,
                        principalTable: "json_mappings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_json_field_mappings_JsonMappingId",
                table: "json_field_mappings",
                column: "JsonMappingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "json_field_mappings");

            migrationBuilder.DropTable(
                name: "json_mappings");
        }
    }
}
