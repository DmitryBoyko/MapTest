using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MapTest.Migrations
{
    public partial class Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            if (ActiveProvider == "Microsoft.EntityFrameworkCore.SqlServer")
            {
                // do something SQL Server - specific
            }
            if (ActiveProvider == "Microsoft.EntityFrameworkCore.Sqlite")
            {
                // do something SqLite - specific
            }

           migrationBuilder.CreateTable(
               name: "FederalDistricts",
               columns: table => new
               {
                   ID = table.Column<int>(nullable: false)
                       .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                   Name = table.Column<string>(nullable: true),
                   ShortName = table.Column<string>(nullable: true)
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_FederalDistricts", x => x.ID);
               });

            migrationBuilder.CreateTable(
               name: "FederalSubjects",
               columns: table => new
               {
                   ID = table.Column<int>(nullable: false)
                       .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                   Name = table.Column<string>(nullable: true),
                   FederalDistrictID = table.Column<int>(nullable: true),
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_FederalSubjects", x => x.ID);
                   
               });

            migrationBuilder.AddForeignKey(name: "FK_Projects_Users_UserId",
                    table: "FederalSubjects",
                    column: "FederalDistrictID",
                    principalTable: "FederalDistricts",
                    principalColumn: "ID",
                    onDelete: ReferentialAction.Cascade);
          }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
