using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinSpotifyApiApp.WebApi
{
    public class Image
    {
        private int height;

        public int GetHeight()
        {
            return height;
        }

        public void SetHeight(int? value)
        {
            height = Convert.ToInt32(value == null ? 0 : value);
        }

        public string Url { get; set; }

        private int width;

        public int GetWidth()
        {
            return width;
        }

        public void SetWidth(int? value)
        {
            width = Convert.ToInt32(value != null ? value : 0);
        }
    }
}
