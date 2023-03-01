using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MicroserviceMail.Migrations
{
    /// <inheritdoc />
    public partial class CreateDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Mail",
                columns: table => new
                {
                    MailId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Owner = table.Column<string>(type: "Varchar(200)", nullable: false),
                    From = table.Column<string>(type: "Varchar(200)", nullable: false),
                    To = table.Column<string>(type: "Varchar(2000)", nullable: false),
                    Cc = table.Column<string>(type: "Varchar(2000)", nullable: false),
                    Subject = table.Column<string>(type: "Varchar(200)", nullable: false),
                    Body = table.Column<string>(type: "Varchar(2000)", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Sent = table.Column<bool>(type: "boolean", nullable: false),
                    Status = table.Column<string>(type: "Varchar(1000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mail", x => x.MailId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mail");
        }
    }
}
