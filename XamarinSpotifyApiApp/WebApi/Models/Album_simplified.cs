using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinSpotifyApiApp.WebApi
{
    public class Album_simplified
    {
        public string Album_type { get; set; }

        public List<Artist_simplified> Artists { get; set; }

        public List<string> Available_markets { get; set; }
        public External_URL External_urls { get; set; }
        public string Href { get; set; }
        public string Id { get; set; }
        public List<Image> Images { get; set; }
        public string Name { get; set; }
        public string Release_date { get; set; }
        public string Release_date_precision { get; set; }
        public int Total_tracks { get; set; }
        public string Type { get; set; }
        public string Uri { get; set; }

    }
}
