using MediaPlaylist.Core;
using MediaPlaylistStore;

namespace MediaPlaylist.ViewModels.PlaylistViewModels
{
    public class PlaylistViewModel : ViewModelBase
    {
        private Playlist _playlist;
        public Playlist Playlist => _playlist;

        public int Id => _playlist.PlaylistId;
        public string Title => _playlist.Title;
        public string? Description => _playlist.Description;
        public string CreationDate => _playlist.CreationDate.ToString("g");
        public string? LastModifiedDate => _playlist.LastModifiedDate?.ToString("g");
        public List<Media> Medias => _playlist.Medias;

        public PlaylistViewModel(Playlist playlist)
        {
            _playlist = playlist;
        }


        // Refresh data manually when needed.
        public void RefreshData(Playlist updatedPlaylist)
        {
            _playlist = updatedPlaylist;

            // Notify all relevant properties
            OnPropertyChanged(nameof(Playlist));
            OnPropertyChanged(nameof(Id));
            OnPropertyChanged(nameof(Title));
            OnPropertyChanged(nameof(Description));
            OnPropertyChanged(nameof(CreationDate));
            OnPropertyChanged(nameof(LastModifiedDate));
            OnPropertyChanged(nameof(Medias));
        }
    }
}
