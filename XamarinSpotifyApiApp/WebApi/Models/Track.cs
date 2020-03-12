using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinSpotifyApiApp.WebApi
{
    class Track : object
    {
        public Album_simplified Album { get; set; }
        public List<Artist_simplified> Artists { get; set; }
        public List<string> Available_markets { get; set; }
        public int Disc_number { get; set; }
        public int Duration_ms { get; set; }
        public bool Explicit { get; set; }
        public External_ID External_ids { get; set; }
        public External_URL External_urls { get; set; }
        public string Href { get; set; }
        public string Id { get; set; }
        public bool Is_local { get; set; }
        public string Name { get; set; }
        public int Popularity { get; set; }
        public string Preview_url { get; set; }
        public int Track_number { get; set; }
        public string Type { get; set; }
        public string Uri { get; set; }


    }
}
