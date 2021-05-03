using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Module4_Test.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Artist",
                columns: table => new
                {
                    ArtistId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "date", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstagramUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artist", x => x.ArtistId);
                });

            migrationBuilder.CreateTable(
                name: "Genre",
                columns: table => new
                {
                    GenreId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genre", x => x.GenreId);
                });

            migrationBuilder.CreateTable(
                name: "Song",
                columns: table => new
                {
                    SongId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Duration = table.Column<TimeSpan>(type: "time", nullable: false),
                    ReleasedDate = table.Column<DateTime>(type: "date", nullable: false),
                    GenreId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Song", x => x.SongId);
                    table.ForeignKey(
                        name: "FK_Song_Genre_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genre",
                        principalColumn: "GenreId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ArtistSong",
                columns: table => new
                {
                    ArtistId = table.Column<int>(type: "int", nullable: false),
                    SongId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtistSong", x => new { x.ArtistId, x.SongId });
                    table.ForeignKey(
                        name: "FK_ArtistSong_Artist_ArtistId",
                        column: x => x.ArtistId,
                        principalTable: "Artist",
                        principalColumn: "ArtistId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArtistSong_Song_SongId",
                        column: x => x.SongId,
                        principalTable: "Song",
                        principalColumn: "SongId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArtistSong_SongId",
                table: "ArtistSong",
                column: "SongId");

            migrationBuilder.CreateIndex(
                name: "IX_Song_GenreId",
                table: "Song",
                column: "GenreId");

            migrationBuilder.Sql(@"
                INSERT INTO Genre(Title) VALUES('Country')
                INSERT INTO Genre(Title) VALUES('Pop')
                INSERT INTO Genre(Title) VALUES('Jazz')
                INSERT INTO Genre(Title) VALUES('Rock')
                INSERT INTO Genre(Title) VALUES('Disco')

                INSERT INTO Artist(Name, DateOfBirth, Phone, Email) VALUES('Elton John', '1946-01-18', '1234567895', 'a@b')
                INSERT INTO Artist(Name, DateOfBirth, InstagramUrl) VALUES('Texas', '1980-02-04', 'https://www.instagram.com/aaaaaaaa/?hl=en')
                INSERT INTO Artist(Name, DateOfBirth, InstagramUrl) VALUES('Boney M', '1970-08-09', 'https://www.instagram.com/bbbbbbbbb/?hl=en')
                INSERT INTO Artist(Name, DateOfBirth, Phone) VALUES('ABBA', '1969-11-16', '123123123')
                INSERT INTO Artist(Name, DateOfBirth, InstagramUrl) VALUES('Radiohead', '1995-07-02', 'https://www.instagram.com/ttttttttt/?hl=en')

                INSERT INTO Song(Title, Duration, ReleasedDate, GenreId) VALUES('Goodbye Yellow Brick Road', '00:03:08', '1973-08-07', (select GenreId from Genre where Title = 'Pop'))
                INSERT INTO Song(Title, Duration, ReleasedDate, GenreId) VALUES('Summer Son', '00:02:55', '1999-10-03', (select GenreId from Genre where Title = 'Rock'))
                INSERT INTO Song(Title, Duration, ReleasedDate, GenreId) VALUES('Daddy cool', '00:02:52', '1985-12-11', (select GenreId from Genre where Title = 'Disco'))
                INSERT INTO Song(Title, Duration, ReleasedDate, GenreId) VALUES('Voulez-Vous', '00:03:12', '1982-06-09', (select GenreId from Genre where Title = 'Disco'))
                INSERT INTO Song(Title, Duration, ReleasedDate, GenreId) VALUES('Creep', '00:04:59', '2002-06-09', (select GenreId from Genre where Title = 'Rock'))

                INSERT INTO ArtistSong(ArtistId, SongId) VALUES((select ArtistId from Artist where Name = 'Elton John'), (select SongId from Song where Title = 'Goodbye Yellow Brick Road'))
                INSERT INTO ArtistSong(ArtistId, SongId) VALUES((select ArtistId from Artist where Name = 'Texas'), (select SongId from Song where Title = 'Summer Son'))
                INSERT INTO ArtistSong(ArtistId, SongId) VALUES((select ArtistId from Artist where Name = 'Boney M'), (select SongId from Song where Title = 'Daddy cool'))
                INSERT INTO ArtistSong(ArtistId, SongId) VALUES((select ArtistId from Artist where Name = 'ABBA'), (select SongId from Song where Title = 'Voulez-Vous'))
                INSERT INTO ArtistSong(ArtistId, SongId) VALUES((select ArtistId from Artist where Name = 'Radiohead'), (select SongId from Song where Title = 'Creep'))
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArtistSong");

            migrationBuilder.DropTable(
                name: "Artist");

            migrationBuilder.DropTable(
                name: "Song");

            migrationBuilder.DropTable(
                name: "Genre");
        }
    }
}
