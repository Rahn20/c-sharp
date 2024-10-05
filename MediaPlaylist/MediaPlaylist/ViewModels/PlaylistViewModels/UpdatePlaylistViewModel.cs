using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using MediaPlaylist.Core;
using MediaPlaylist.Services;
using MediaPlaylistBL;
using MediaPlaylistStore;

namespace MediaPlaylist.ViewModels.PlaylistViewModels
{
    public class UpdatePlaylistViewModel : ViewModelBase, INavigatable
    {
        private readonly ApplicationManager _appManager;
        private PlaylistViewModel _playlist;


        #region Properties
        public ObservableCollection<Media> Medias { get; private set; }
        public string HeaderTxt { get; private set; } = string.Empty;

        public string _statusMessage = string.Empty;
        public string StatusMessage
        {
            get => _statusMessage;
            private set
            {
                if (_statusMessage != value)
                {
                    _statusMessage = value;
                    OnPropertyChanged(nameof(StatusMessage));
                }
            }
        }

        private string _title = string.Empty;
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        private string _description = string.Empty;
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        #endregion

        public AsyncCommandBase UpdatePlaylistCommand { get; }
        public AsyncCommandBase RemoveMediaCommand { get; }
        public ICommand BackToPlaylistCommand { get; }

        public UpdatePlaylistViewModel(INavigationService navigationService, ApplicationManager manager)
        {
            _appManager = manager;
            Medias = new ObservableCollection<Media>();

            BackToPlaylistCommand = new CommandBase(_ => navigationService.NavigateTo<PlaylistDetailsViewModel>(_playlist));
            UpdatePlaylistCommand = new AsyncCommandBase(async _ => await UpdatePlaylist());
            RemoveMediaCommand = new AsyncCommandBase(async (parameter) => await RemoveMedia(parameter));
        }


        public void Initialize(object parameter)
        {
            StatusMessage = string.Empty;

            if (parameter is PlaylistViewModel playlistItem)
            {
                _playlist = playlistItem;
                HeaderTxt = $"Update {playlistItem.Title}";
                Title = playlistItem.Title;
                Description = playlistItem.Description;
                Medias.Clear();
                _playlist.Medias.ForEach(m => Medias.Add(m));
            }
        }

        private async Task RemoveMedia(object parameter)
        {
            try
            {
                if (parameter is Media media)
                {
                    await _appManager.RemoveMedia(media);
                    Medias.Remove(media);
                }
            }
            catch (Exception ex)
            {
                //Debug.WriteLine(ex);
                MessageBox.Show($"Error removing media from playlist: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task UpdatePlaylist()
        {
            try
            {
                if (string.IsNullOrEmpty(Title)) throw new Exception("Title cannot be empty");

                await _appManager.UpdatePlaylist(_playlist.Id, Title, Description);
                Playlist getPlaylist = await _appManager.GetPlaylistById(_playlist.Id);
                _playlist.RefreshData(getPlaylist);

                StatusMessage = "Playlist has successfully been uppdated";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating playlist: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
