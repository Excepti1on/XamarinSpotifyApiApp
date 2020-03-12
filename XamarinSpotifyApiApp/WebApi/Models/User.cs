using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinSpotifyApiApp.WebApi
{
    class User
    {
        public string Display_name { get; set; }
        public External_URL External_urls { get; set; }
        public Followers Followers { get; set; }
        public string Href { get; set; }
        public string Id { get; set; }
        public List<Image> Images { get; set; }
        public string Type { get; set; }
        public string Uri { get; set; }

    }
}
