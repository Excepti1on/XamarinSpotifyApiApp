﻿using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinSpotifyApiApp.WebApi
{
    class Playlist
    {
        public bool Collaborative { get; set; }
        public string Description { get; set; }
        public External_URL External_urls { get; set; }
        public Followers Followers { get; set; }
        public string Href { get; set; }
        public string Id { get; set; }
        public List<Image> Images { get; set; }
        public string Name { get; set; }
        public UserPublic Owner { get; set; }
        public bool Public { get; set; }
        public string Snapshot_id { get; set; }
        public PagingTracks Tracks { get; set; }
        public string Type { get; set; }
        public string Uri { get; set; }

    }
}
