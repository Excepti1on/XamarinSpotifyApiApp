using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinSpotifyApiApp.WebApi
{
    public class PagingTracks
    {
        public string Href { get; set; }
        public int Limit { get; set; }
        public string Next { get; set; }
        public int Offset { get; set; }
        public string Previous { get; set; }
        public int Total { get; set; }

        public List<Track> Items { get; set; }


        /*private List<object> items = new List<object>();

        public List<object> GetItems()
        {
            return items;
        }

        public void SetItems(List<object> value)
        {
            items = value;
        }*/
    }
}
