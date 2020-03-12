using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamarinSpotifyApiApp.WebApi;
using XamarinSpotifyApiApp.ViewModels;
using XamarinSpotifyApiApp.Services;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinSpotifyApiApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MediumTermPage : ContentPage
    {
        public AudioPlayerViewModel MyAudioPlayerViewModel { get; set; }

        public IList<Track> Tracks { get; set; }
        public MediumTermPage(PagingTracks pagingTracks)
        {


            Tracks = new List<Track>(pagingTracks.Items);
            BindingContext = this;

            InitializeComponent();
        }
    }
}