using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieApp.Migrations
{
    //created with PMC
    public partial class InitialDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Character",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Alias = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PictureUrl = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Character", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Franchise",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Franchise", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Movie",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Genre = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    ReleaseYear = table.Column<int>(type: "int", nullable: false),
                    Director = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PictureUrl = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Trailer = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    FranchiseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movie", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Movie_Franchise_FranchiseId",
                        column: x => x.FranchiseId,
                        principalTable: "Franchise",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CharacterMovie",
                columns: table => new
                {
                    CharactersId = table.Column<int>(type: "int", nullable: false),
                    MoviesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterMovie", x => new { x.CharactersId, x.MoviesId });
                    table.ForeignKey(
                        name: "FK_CharacterMovie_Character_CharactersId",
                        column: x => x.CharactersId,
                        principalTable: "Character",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterMovie_Movie_MoviesId",
                        column: x => x.MoviesId,
                        principalTable: "Movie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Character",
                columns: new[] { "Id", "Alias", "FullName", "Gender", "PictureUrl" },
                values: new object[,]
                {
                    { 1, "Human", "Iron Man", "Male", "https://vignette.wikia.nocookie.net/marvelcinematicuniverse/images/e/e9/Iron_Man_AIW_Profile.jpg/revision/latest?cb=20180518212029" },
                    { 2, "Human", "Captain America", "Male", "https://vignette.wikia.nocookie.net/marvelcinematicuniverse/images/6/66/Captain_America_AIW_Profile.jpg/revision/latest?cb=20180518211704" },
                    { 3, "Human", "Hulk", "Male", "https://vignette.wikia.nocookie.net/marvelcinematicuniverse/images/c/c3/Hulk_AIW_Profile.jpg/revision/latest?cb=20180518211829" },
                    { 4, "Asgardian", "Thor", "Male", "https://vignette.wikia.nocookie.net/marvelcinematicuniverse/images/4/45/Thor_AIW_Profile.jpg/revision/latest?cb=20180518212120" },
                    { 5, "Human", "Black Widow", "Female", "https://vignette.wikia.nocookie.net/marvelcinematicuniverse/images/5/50/Black_Widow_AIW_Profile.jpg/revision/latest?cb=20180518212205" },
                    { 6, "Human", "Hawkeye", "Maale", "https://vignette.wikia.nocookie.net/marvelcinematicuniverse/images/6/6f/CW_Textless_Shield_Poster_02.jpg/revision/latest?cb=20180417151836" }
                });

            migrationBuilder.InsertData(
                table: "Franchise",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, null, "Marvel Cinematic Universe" },
                    { 2, null, "Lord of the Rings" }
                });

            migrationBuilder.InsertData(
                table: "Movie",
                columns: new[] { "Id", "Director", "FranchiseId", "Genre", "PictureUrl", "ReleaseYear", "Title", "Trailer" },
                values: new object[,]
                {
                    { 1, "", 1, "Action", "", 2015, "Iron Man", "" },
                    { 22, "Peter Jackson", 2, "Fantasy", "", 2003, "The Two Towers", "" },
                    { 21, "Peter Jackson", 2, "Fantasy", "", 2001, "The Fellowship of the Ring", "" },
                    { 20, "", 1, "Science Fiction", "", 2018, "Ant-Man and the Wasp", "" },
                    { 19, "", 1, "Science Fiction", "", 2018, "Avengers: Infinity War", "" },
                    { 18, "", 1, "Science Fiction", "", 2018, "Black Panther", "" },
                    { 17, "", 1, "Science Fiction", "", 2017, "Thor: Ragnarok", "" },
                    { 16, "", 1, "Science Fiction", "", 2017, "Spider-Man: Homecoming", "" },
                    { 15, "", 1, "Science Fiction", "", 2017, "Guardians of the Galaxy Vol. 2", "" },
                    { 14, "", 1, "Science Fiction", "", 2016, "Doctor Strange", "" },
                    { 13, "", 1, "Science Fiction", "", 2016, "Captain America: Civil War", "" },
                    { 12, "", 1, "Science Fiction", "", 2015, "Ant-Man", "" },
                    { 11, "", 1, "Science Fiction", "", 2015, "Avengers: Age of Ultron", "" },
                    { 10, "", 1, "Science Fiction", "", 2014, "Guardians of the Galaxy", "" },
                    { 9, "", 1, "Science Fiction", "", 2014, "Captain America: The Winter Soldier", "" },
                    { 8, "", 1, "Science Fiction", "", 2013, "Thor: The Dark World", "" },
                    { 7, "", 1, "Action", "", 2015, "Iron Man 3", "" },
                    { 6, "", 1, "Science Fiction", "", 2012, "Avengers, The", "" },
                    { 5, "", 1, "Science Fiction", "", 2011, "Captain America", "" },
                    { 4, "", 1, "Action", "", 2011, "Thor", "" },
                    { 3, "", 1, "Action", "", 2015, "Iron Man 2", "" },
                    { 2, "", 1, "Action", "", 2008, "The Incredible Hulk", "" },
                    { 23, "Peter Jackson", 2, "Fantasy", "", 2005, "The Return of the King", "" },
                    { 24, "Peter Jackson", 2, "Fantasy", "", 2010, "Hobbit", "" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CharacterMovie_MoviesId",
                table: "CharacterMovie",
                column: "MoviesId");

            migrationBuilder.CreateIndex(
                name: "IX_Movie_FranchiseId",
                table: "Movie",
                column: "FranchiseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharacterMovie");

            migrationBuilder.DropTable(
                name: "Character");

            migrationBuilder.DropTable(
                name: "Movie");

            migrationBuilder.DropTable(
                name: "Franchise");
        }
    }
}
