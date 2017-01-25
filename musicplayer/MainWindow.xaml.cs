using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace musicplayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public string selectedAlbum;
        public string selectedArtist;

        public MainWindow()
        {
            InitializeComponent();
            Library.InitializeLibrary();
            foreach (Artist artist in Library.Artists)
            {
                artistListBox.Items.Add(artist.ToString());
                foreach (Album album in artist.Albums)
                {
                    albumListBox.Items.Add(album.AlbumName);
                    foreach (Song song in album.Songs)
                        songListBox.Items.Add(song.songName);
                }
            }
        }

        private void artistListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            albumListBox.Items.Clear();
            songListBox.Items.Clear();
            selectedArtist = artistListBox.SelectedItem.ToString();
            foreach (Artist artist in Library.Artists)
            {
                if (artist.ArtistName == selectedArtist)
                {
                    foreach (Album album in artist.Albums)
                    {
                        albumListBox.Items.Add(album.AlbumName);
                        foreach (Song song in album.Songs)
                            songListBox.Items.Add(song.songName);
                    }
                }
            }
        }

        private void albumListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            songListBox.Items.Clear();
            if (albumListBox.SelectedItem != null)
                selectedAlbum = albumListBox.SelectedItem.ToString();
            foreach (Artist artist in Library.Artists)
            {
                foreach (Album album in artist.Albums)
                {
                    if (album.AlbumName == selectedAlbum)
                    {
                        foreach (Song song in album.Songs)
                            songListBox.Items.Add(song.songName);
                    }
                }
            }
        }

        private void playbackButton_Click(object sender, RoutedEventArgs e)
        {
            PlaybackController playbackController = new PlaybackController();
            playbackController.playSong();
        }
    }
}
