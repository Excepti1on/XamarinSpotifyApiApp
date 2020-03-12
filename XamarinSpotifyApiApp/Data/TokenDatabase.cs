using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using XamarinSpotifyApiApp.WebApi;
using System.Threading.Tasks;

namespace XamarinSpotifyApiApp.Data
{
    public class TokenDatabase
    {
        readonly SQLiteAsyncConnection _database;

        public TokenDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Token>().Wait();
        }

        public Task<Token> GetTokenAsync(string type)
        {
            return _database.Table<Token>()
                            .Where(i => i.Token_type == "Bearer")
                            .FirstOrDefaultAsync();
        }

         
    }
}
