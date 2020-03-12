using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinSpotifyApiApp.WebApi
{
    class Followers
    {
        private string href;

        public string GetHref()
        {
            return href;
        }

        public void SetHref(string value)
        {
            if (value == null)
            {
                href = "";
            }
            else
            {
                href = value;
            }

        }

        public int Total { get; set; }

    }
}
