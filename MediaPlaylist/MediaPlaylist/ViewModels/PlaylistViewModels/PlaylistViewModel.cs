using MediaPlaylist.Core;
using MediaPlaylistStore;

namespace MediaPlaylist.ViewModels.PlaylistViewModels
{
    public class PlaylistViewModel : ViewModelBase
    {
        private readonly Playlist _playlist;

        public int Id => _playlist.Id;
        public string Title => _playlist.Title;
        public string? Description => _playlist.Description;
        public string CreationDate => _playlist.CreationDate.ToString("g");
        public string? LastModifiedDate => _playlist.LastModifiedDate?.ToString("g");
        public List<Media> Medias => _playlist.Medias;

        public PlaylistViewModel(Playlist playlist)
        {
            _playlist = playlist;
        }
    }
}
