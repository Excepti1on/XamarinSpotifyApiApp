using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamarinSpotifyApiApp.WebApi;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using OnlyMusic;

namespace XamarinSpotifyApiApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShortTermPage : ContentPage
    {

        public IList<Track> Tracks { get; set; }
        public ShortTermPage(PagingTracks pagingTracks)
        {


            Tracks = new List<Track>(pagingTracks.Items);
            BindingContext = this;

            InitializeComponent();

            var play = new Button()
            {
                Text = "Play/Pause",
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                Command = new Command(() =>
                {
                    DependencyService.Get<IAudio>().Play_Pause("http://www.montemagno.com/sample.mp3");
                })
            };
            var stop = new Button()
            {
                Text = " Stop ",
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                Command = new Command(() =>
                {
                    DependencyService.Get<IAudio>().Stop(true);
                })
            };

        }
    }
}
