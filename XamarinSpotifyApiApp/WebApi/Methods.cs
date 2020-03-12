using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.IO;
using System.Runtime.InteropServices;
using Xamarin.Essentials;

namespace XamarinSpotifyApiApp.WebApi
{
    class Methods
    {
        //static string code;
        //static string playlistID;
        //static string userID;
        static bool playlistExists;
        //static int g = 1;

        private static readonly string clientID = "6ddff9420ebb4871893a10f3e4952ac8";
        private static readonly string clientSecret = "0d4c198041c14cb1b1a21725c5c14bd5";




        //static Token token = new Token();
        //static PagingTracks pagingObject = new PagingTracks();
        //static PagingPlaylists PagingPlaylists = new PagingPlaylists();


        //static readonly string pathTopTracks = @"F:\repos\SpotifyAPIApp\TopTracks.txt";
        //static readonly string pathUserPlaylists = @"F:\repos\SpotifyAPIApp\UserPlaylists.txt";


        //static List<string> myTopTracks = new List<string>();
        //static List<string> userPlaylists = new List<string>();



        static async Task Main(string[] args)
        {
            /*SpotifyRedirect(clientID);
            string code = GetCode();
            //Console.WriteLine(code);
            Token token = await GetToken(clientID, clientSecret, code);
            string type = "short term";
            //Console.WriteLine(token.Access_token);
            PagingTracks pagingTracks = await GetTopTracks(token.Access_token, type);

            foreach (Track item in pagingTracks.Items)
            {
                myTopTracks.Add(item.Uri);
                //Console.WriteLine(item.Popularity);
            }

            PagingPlaylists pagingPlaylists = await GetUserPlaylists(token.Access_token);
            //Console.WriteLine("");

            foreach (Playlist_simplified item in pagingPlaylists.Items)
            {
                userPlaylists.Add(item.Id);
                //Console.WriteLine(item.Owner.Display_name);
            }

            //userID = await GetUserId();
            string playlistID = await FindPlaylist(token.Access_token, pagingPlaylists, type);
            //Console.WriteLine(playlistID);
            //Console.WriteLine(playlistID);

            //Console.WriteLine(userID);

            if (playlistExists == true)
            {
                await ReplaceTracks(playlistID, myTopTracks, token.Access_token);
            }
            else
            {
                await AddTracks(playlistID, myTopTracks, token.Access_token);
            }

            Console.WriteLine("Your playlist has been created");
            */
            string[] input = { "short_term", "medium_term", "long_term" };
            string token = await Authentification(clientID, clientSecret);
            Console.WriteLine(token);
            //List<string> types = new List<string>({"short term", "medium term", "long term"});
            //types = ["short term", "medium term", "long term"];
            foreach (string item in input)
            {
                Console.WriteLine(item);
                await MakePlaylist(item, token);
            }
        }

        static async Task<string> Authentification(string clientId, string clientSecret)
        {
            SpotifyRedirect(clientId);

            Token token = await GetToken(clientId, clientSecret, GetCode());
            return token.Access_token;

        }

        public static async Task MakePlaylist(string type, string token)
        {
            PagingTracks tracks = await GetTopTracks(token, type);
            string playlistId = await FindPlaylist(token, await GetUserPlaylists(token), type);
            List<string> myTopTracks = new List<string>();
            List<string> myPlaylists = new List<string>();
            foreach (Track item in tracks.Items)
            {
                myTopTracks.Add(item.Uri);
            }

            if (playlistExists == true)
            {
                await ReplaceTracks(playlistId, myTopTracks, token);
            }
            else
            {
                await AddTracks(playlistId, myTopTracks, token);
            }

        }
        public static void SpotifyRedirect(string clientId)   //This Method opens up a browser window with the Spotify Authentification site.
        {
            //Currently Windows only
            string url = $"https://accounts.spotify.com/authorize?client_id={clientId}&response_type=code&redirect_uri=XamarinApiApp://callback&scope=playlist-modify-public%20playlist-modify-private%20playlist-read-private%20user-top-read%20user-read-private&state=34fFs29kd09";

            Browser.OpenAsync(url);
        }
        static string GetCode()     //retrieving the code given from the Spotify Authentification
        {

            //Creating a HttpListener which listens for the reqeusts made to the redirect uri
            HttpListener responseListener = new HttpListener();
            responseListener.Prefixes.Add("http://localhost:5050/");
            responseListener.Start();
            Console.WriteLine("Listening.");    //Useless, but shows the user, that its working

            //Deriving a HttpListenerContext from our HttpListener to give Responses and make Requests.
            HttpListenerContext context = responseListener.GetContext();
            HttpListenerRequest request = context.Request;
            HttpListenerResponse response = context.Response;


            //creating the response which will be displayed to the user when he is redirected
            string responseString = "<html><title>Please close this Page!</title><body>Please close this page!<body></html>";
            byte[] buffer = Encoding.UTF8.GetBytes(responseString);
            response.ContentLength64 = buffer.Length;
            Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            output.Close();

            //Getting the strings we want to retrieve from the Redirect Uri
            string code = request.QueryString["code"];
            string state = request.QueryString["state"];

            //Console.WriteLine(code);    //Useless, but shows that the code was retrieved

            responseListener.Stop();    // Closing the Context Object

            return code;

        }

        public static async Task<Token> GetToken(string clientId, string clientSecret, string code)
        {
            //A Method for recieving an Access Token from the Api using the code from GetCode()

            //Creating a HttpClient whit whom the Request will be handled
            HttpClient tokenClient = new HttpClient();

            //The string that will be added to the Request header
            string tokenPostHeader = $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes(clientId + ":" + clientSecret))}";

            //The List which will be added to the Request body
            List<KeyValuePair<string, string>> tokenPostBody = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("grant_type", "authorization_code"),
                new KeyValuePair<string, string>("code", code),
                new KeyValuePair<string, string>("redirect_uri", "XamarinApiApp://callback")
            };

            //Adding the Request Header to the Client and the Request Body to the HttpContent
            tokenClient.DefaultRequestHeaders.Add("Authorization", tokenPostHeader);
            HttpContent tokenContent = new FormUrlEncodedContent(tokenPostBody);

            //Making a Post Request with our HttpClient to the Spotify API that contains the created HttpContent. Recieving the Json as a string.
            HttpResponseMessage httpResponse = await tokenClient.PostAsync("https://accounts.spotify.com/api/token", tokenContent);
            string tokenReponse = await httpResponse.Content.ReadAsStringAsync();

            //Converting the Json to a Token Object and returning it.

            tokenClient.Dispose();
            return JsonConvert.DeserializeObject<Token>(tokenReponse);
        }

        public static async Task<PagingTracks> GetTopTracks(string token, string type)
        {
            //A Method to recieve the Users Top Tracks
            //string timeRange = "";
            /*switch (type)
            {
                case "short term":
                    timeRange = "short-term";
                    break;
                case "medium term":
                    timeRange = "medium-term";
                    break;
                case "long term":
                    timeRange = "long-term";
                    break;
                default:
                    break;
            }
            */
            //Declaring the strings needed for the Method
            string topTracksHeader = $"Bearer {/*Convert.ToBase64String(Encoding.UTF8.GetBytes(*/token/*))*/}";
            string apiUrl = $"https://api.spotify.com/v1/me/top/tracks?time_range={type}&limit=50&offset=0";

            //Creating a HttpClient with a Header, that contains the access token
            HttpClient spotifyClient = new HttpClient();
            spotifyClient.DefaultRequestHeaders.Add("Authorization", topTracksHeader);

            //Recieving the Response as a string from the specified Url and Deserializing it into a Paging Object
            string responseBody = await spotifyClient.GetStringAsync(apiUrl);
            PagingTracks getTopTracks = JsonConvert.DeserializeObject<PagingTracks>(responseBody);

            /*
            //Creating a .txt file that contains the json Response
            if (!File.Exists(pathTopTracks))
            {
                using StreamWriter streamWriter = File.CreateText(pathTopTracks);
                streamWriter.WriteLine(responseBody);
            }
            */
            //Dispoal of the HttpClient
            spotifyClient.Dispose();

            //Returning the Paging Obejct that contains the useres Top Tracks
            Console.WriteLine("Got Top Tracks");
            return getTopTracks;

        }

        public static async Task<PagingPlaylists> GetUserPlaylists(string token)
        {
            //A Method to recieve the Users Playlists

            //Creating the strings for the Header and Url for the Request
            string userPlaylistHeader = $"Bearer {token}";
            string apiUrl = "https://api.spotify.com/v1/me/playlists?limit=50&offset=0";

            //Creating a HttpClient to handle the Get Request and adding the Request Header
            HttpClient spotifyClient = new HttpClient();
            spotifyClient.DefaultRequestHeaders.Add("Authorization", userPlaylistHeader);

            //Making a Get Request to the apiUrl and recieving a Json Object as a string
            string responseBody = await spotifyClient.GetStringAsync(apiUrl);

            //Converting the Json into a Paging Object, which contains the Users Playlists
            PagingPlaylists getUserPlaylists = JsonConvert.DeserializeObject<PagingPlaylists>(responseBody);


            /*//Useless, creates a .txt file in which the requested json will be displayed
            if (!File.Exists(pathUserPlaylists))
            {
                using StreamWriter streamWriter = File.CreateText(pathUserPlaylists);
                streamWriter.WriteLine(responseBody);
            }
            */
            //Disposing of the HttpClient
            spotifyClient.Dispose();

            //returning the Paging Object, that contains the Playlists
            Console.WriteLine("Got User Playlists");
            return getUserPlaylists;
        }

        public static async Task<string> FindPlaylist(string token, PagingPlaylists playlists, string type)
        {
            //A Method that checks, if the Playlist that this app creates already exists, and if so, which id it has
            string name = "";
            string description = "";

            switch (type)
            {
                case "short_term":
                    name = "Short Term Top Tracks";
                    description = "Your Short Term(4 weeks) Top Tracks";
                    break;
                case "medium_term":
                    name = "Medium Term Top Tracks";
                    description = "Your Medium Term(6 months) Top Tracks";
                    break;
                case "long_term":
                    name = "Long Term Top Tracks";
                    description = "Your Long Term(all time) Top Tracks";
                    break;
            }
            //Creating a Playlist Object whose ID we want to get
            Playlist_simplified fgAnalytics = new Playlist_simplified();

            //Searching the Playlists return from the GetUserPlaylists() Method for the Name and Description that this App creates.
            fgAnalytics = playlists.Items.Find(x => x.Name.Contains(name) && x.Description == description);

            //If a Playlist exists, that matches our Specifications, return its ID, otherwise return, that it is non existent.
            if (fgAnalytics != null)
            {
                playlistExists = true;
                return fgAnalytics.Id;
            }
            else
            {
                playlistExists = false;
                string id = await CreatePlaylist(token, await GetUserId(token), name, description);
                //string id = "x";
                return id;
            }
        }

        public static async Task<string> CreatePlaylist(string token, string userId, string name, string description)
        {
            //A Method to Create a Playlist

            //Setting the Url and the Header for the Request
            string apiUrl = $"https://api.spotify.com/v1/users/{userId}/playlists";
            string playlistHeaderAuth = $"Bearer {token}";
            string playlistHeaderContentType = "application/json";

            //Creating the Json for the Request Body
            string playlistBody = $"{{\"name\":\"{name}\",\"description\":\"{description}\",\"public\":false}}";

            //Creating a HttpClient for the post-Request
            HttpClient spotifyClient = new HttpClient();

            //Adding the Headers to the HttpClient
            spotifyClient.DefaultRequestHeaders.Add("Authorization", playlistHeaderAuth);
            //spotifyClient.DefaultRequestHeaders.Add("Content-Type", playlistHeaderContentType);

            //Adding the Body to a HttpContent to use for the HttpClient
            HttpContent spotifyContent = new StringContent(playlistBody);
            spotifyContent.Headers.ContentType.MediaType = playlistHeaderContentType;

            //Making the POST Request, get the Playlist Object as a Json String as the Response
            HttpResponseMessage httpResponse = await spotifyClient.PostAsync(apiUrl, spotifyContent);
            string responseMessage = await httpResponse.Content.ReadAsStringAsync();
            //Console.WriteLine(responseMessage);


            //Converting the response String into a Playlist Object
            Playlist newPlaylist = new Playlist();

            newPlaylist = JsonConvert.DeserializeObject<Playlist>(responseMessage);

            spotifyClient.Dispose();

            //returning the Id of the new Playlist
            string id;
            id = newPlaylist.Id;

            return id;
        }

        public static async Task<string> GetUserId(string token)
        {
            //A Method that gets the user ID of the current User

            //Creating the Header for the Get Request
            string userHeader = $"Bearer {token}";

            //Creating the HttpClient for the Request and adding the previously defined Header
            HttpClient spotifyResponse = new HttpClient();
            spotifyResponse.DefaultRequestHeaders.Add("Authorization", userHeader);

            //Get Reqeust via HttpClient to get the user info
            string responseBody = await spotifyResponse.GetStringAsync("https://api.spotify.com/v1/me");

            //Convert the response Json into an User Object
            User currentUser = JsonConvert.DeserializeObject<User>(responseBody);

            //Disposing of the HttpClient
            spotifyResponse.Dispose();

            //Returning the Id Variable of the User Object
            return currentUser.Id;

        }

        public static async Task ReplaceTracks(string playlistId, List<string> Tracks, string token)
        {
            //A Method for Replacing the Tracks in an already existing Playlist

            //Creating the Url and the Headers
            string apiUrl = $"https://api.spotify.com/v1/playlists/{playlistId}/tracks";
            string replaceHeaderAuth = $"Bearer {token}";
            string replaceHeaderContentType = "application/json";

            //Creating the Json Body for the http Request
            string replaceBody = "{\"uris\":" + JsonConvert.SerializeObject(Tracks) + "}";
            //Console.WriteLine("Json");
            //Console.WriteLine(replaceBody);

            //Creating the HttpClient
            HttpClient spotifyClient = new HttpClient();

            //Adding the Headers
            spotifyClient.DefaultRequestHeaders.Add("Authorization", replaceHeaderAuth);
            //spotifyClient.DefaultRequestHeaders.Add("Content-Type", replaceHeaderContentType);

            //Creating the HttpContent for the Body
            HttpContent spotifyContent = new StringContent(replaceBody);
            spotifyContent.Headers.ContentType.MediaType = replaceHeaderContentType;


            //Making the PUT Request
            HttpResponseMessage httpResponse = await spotifyClient.PutAsync(apiUrl, spotifyContent);
            string spotifyResponse = await httpResponse.Content.ReadAsStringAsync();

            //Console.WriteLine("Replace" + spotifyResponse);

            //Parsing the Response String


            spotifyClient.Dispose();
        }

        public static async Task AddTracks(string playlistId, List<string> Tracks, string token)
        {
            //A Method for Adding Tracks to the new Playlist

            //Creating the Url and the Headers
            string apiUrl = $"https://api.spotify.com/v1/playlists/{playlistId}/tracks";
            string addHeaderAuth = $"Bearer {token}";
            string addHeaderContentType = "application/json";

            //Creating the Json Body for the http Request
            string addBody = "{\"uris\":" + JsonConvert.SerializeObject(Tracks) + "}";

            //Creating the HttpClient
            HttpClient spotifyClient = new HttpClient();

            //Adding the Headers
            spotifyClient.DefaultRequestHeaders.Add("Authorization", addHeaderAuth);
            //spotifyClient.DefaultRequestHeaders.Add("Content-Type", addHeaderContentType);

            //Creating the HttpContent for the Body
            HttpContent spotifyContent = new StringContent(addBody);
            spotifyContent.Headers.ContentType.MediaType = addHeaderContentType;

            //Making the POST Request
            HttpResponseMessage httpResponse = await spotifyClient.PostAsync(apiUrl, spotifyContent);
            string spotifyResponse = await httpResponse.Content.ReadAsStringAsync();

            //Console.WriteLine("Add" + spotifyResponse);

            //Parsing the Response String into a usable string
            //string snapshot_id = JsonConvert.DeserializeObject<string>(spotifyResponse);

            spotifyClient.Dispose();

            //returning the value of the string



        }




    }
}
