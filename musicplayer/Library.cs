using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace musicplayer
{
    //Library object with properties for lists of artists within the music directory
    public class Library
    {
        public static List<Artist> Artists = new List<Artist>();
        public bool isInitialized;
        //Will change once user is directed to choose a default directory, my directory is included for testing purposes
        public static string MyMusic = @"C:\Users\Pavilion\Music";

        public Library()
        {
            //Default library constructor
            isInitialized = true;
        }

        public static void InitializeLibrary()
        {
            string[] ArtistFolders = Directory.GetDirectories(MyMusic);
            foreach (string ArtistFolder in ArtistFolders)
            {
                Artists.Add(new Artist(ArtistFolder));
            }
        }
    }

    //Artist object with properties for artist name and list of albums within the artist directory
    public class Artist : object
    {
        public string ArtistName { get; set; }
        public List<Album> Albums = new List<Album>();

        public static string ArtistDir { get; set; }

        //Artist constructor
        public Artist(string artistDir)
        {
            ArtistDir = artistDir;
            ArtistName = Path.GetFileNameWithoutExtension(ArtistDir);
            foreach (string album in Directory.GetDirectories(ArtistDir))
            {

                Albums.Add(new Album(album, Path.GetFileNameWithoutExtension(album)));

            }
        }

        override public string ToString()
        {
            return ArtistName;
        }

    }

    //Album object with properties for album name, album directory, and list of songs within the album directory
    public class Album : object
    {
        public string AlbumName { get; set; }
        public string AlbumDir { get; set; }
        public List<Song> Songs = new List<Song>();

        //Album constructor
        public Album(string albumDir, string albumName)
        {
            AlbumDir = albumDir;
            AlbumName = albumName;
            foreach (string song in Directory.GetFiles(AlbumDir))
            {
                Songs.Add(new Song(song, Path.GetFileNameWithoutExtension(song)));
            }
        }
    }

    //Song object with properties for artist name, album name, song name, song directory, and filename
    public class Song : object
    {
        //public string[] fileName { get; set; }
        public string artistName { get; set; }
        public string albumName { get; set; }
        public string songName { get; set; }
        public uint songNum { get; set; }
        public string SongDir { get; set; }
        public int songLength { get; set; } //In seconds

        //Song constructor
        public Song(string songDir, string song)
        {
            SongDir = songDir;
            //   OBSOLETE
            //Parses the song name(string) to only include the song title
            //fileName = song.Split('-');
            //artistName = fileName[0];
            //songName = fileName[1];

            TagLib.File tagFile = TagLib.File.Create(SongDir);
            artistName = tagFile.Tag.FirstAlbumArtist;
            albumName = tagFile.Tag.Album;
            songName = tagFile.Tag.Title;
            songNum = tagFile.Tag.Track;
        }
    }
}
