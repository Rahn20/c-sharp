using MediaPlaylist.ViewModels.PlaylistViewModels;
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

namespace MediaPlaylist.Views.PlaylistViews
{
    /// <summary>
    /// Interaction logic for PlaylistDetailsView.xaml
    /// </summary>
    public partial class PlaylistDetailsView : UserControl
    {
        public PlaylistDetailsView()
        {
            InitializeComponent();
        }

        private void Slider_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is PlaylistDetailsViewModel viewModel)
            {
                viewModel.SliderStartDragging();
            }
        }

        private void Slider_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is PlaylistDetailsViewModel viewModel)
            {
                viewModel.SliderStopDragging();
            }
        }
    }
}
