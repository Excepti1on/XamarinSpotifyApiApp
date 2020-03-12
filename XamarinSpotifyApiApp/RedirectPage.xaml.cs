using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinSpotifyApiApp.WebApi;
using XamarinSpotifyApiApp.WebApi.Models;

namespace XamarinSpotifyApiApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RedirectPage : ContentPage
    {
        static string redirectUri;
        static Token _token = new Token();
        public RedirectPage(string uri)
        {
            redirectUri = uri;
            InitializeComponent();
            //redirectLabel.Text = redirectUri;
            //token = await Methods.GetToken(Client.GetClientID(), Client.GetClientSecret(), redirectUri);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            //redirectLabel.Text = redirectUri;
            Token token = await Methods.GetToken(Client.GetClientID(), Client.GetClientSecret(), redirectUri);
            //redirectLabel.Text = token.Access_token;

            _token = token;

            IList<PagingTracks> passOn = new List<PagingTracks>();
            string[] input = { "short_term", "medium_term", "long_term" };
            foreach (string item in input)
            {
                await Methods.MakePlaylist(item, _token.Access_token);
                passOn.Add(await Methods.GetTopTracks(_token.Access_token, item));

            }
            App.Current.MainPage = new ViewTracksPage(passOn);
        }


    }
}