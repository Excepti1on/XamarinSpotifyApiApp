using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamarinSpotifyApiApp.WebApi;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinSpotifyApiApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewTracksPage : TabbedPage
    {
        public ViewTracksPage(IList<PagingTracks> passOn)
        {
            Children.Add(new ShortTermPage(passOn.First()));
            passOn.Remove(passOn.First());
            Children.Add(new MediumTermPage(passOn.First()));
            passOn.Remove(passOn.First());
            Children.Add(new LongTermPage(passOn.First()));

        }
    }
}