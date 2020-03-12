using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinSpotifyApiApp.WebApi
{
    class PagingPlaylists
    {
        public string Href { get; set; }
        public int Limit { get; set; }
        public string Next { get; set; }
        public int Offset { get; set; }
        public string Previous { get; set; }
        public int Total { get; set; }
        public List<Playlist_simplified> Items { get; set; }

    }
}
