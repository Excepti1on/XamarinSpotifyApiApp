using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinSpotifyApiApp.WebApi.Models
{
    public class Client
    {
        private static readonly string clientID = "6ddff9420ebb4871893a10f3e4952ac8";

        public static string GetClientID()
        {
            return clientID;
        }

        private static readonly string clientSecret = "0d4c198041c14cb1b1a21725c5c14bd5";
        
        public static string GetClientSecret()
        {
            return clientSecret;
        }
    }
}
