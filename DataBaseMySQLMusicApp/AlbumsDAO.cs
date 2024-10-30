using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseMySQLMusicApp
{
    internal class AlbumsDAO
    {
        string connectionString = "datasource=localhost;port=3306;username=root;password=root;database=music;";
        public List<Album> getAllAlbums()
        { 
            //создаем список возвращаемых альбомов
            List<Album> returnAlbums = new List<Album>();
            //соединяемся с базой данных
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlCommand command = new MySqlCommand("Select * From Albums", connection);
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Album a = new Album
                    {
                        ID = reader.GetInt32(0),
                        AlbumName = reader.GetString(1),
                        ArtistName = reader.GetString(2),
                        Year = reader.GetInt32(3),
                        ImageUrl = reader.GetString(4),
                        Description = reader.GetString(5),
                    };
                    a.Tracks = getTracksForAlbum(a.ID);
                    returnAlbums.Add(a);
                }
            }
            connection.Close(); 
            return returnAlbums;
        }

        public List<Album> searchTitles(string searchTerm)
        {
            //создаем список возвращаемых альбомов
            List<Album> returnAlbums = new List<Album>();
            //соединяемся с базой данных
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            string searchWildPhrase = "%" + searchTerm + "%";

            MySqlCommand command = new MySqlCommand();
            command.CommandText = "Select ID,ALBUM_TITLE,ARTIST,YEAR,IMAGE_NAME,DESCRIPTION FROM ALBUMS WHERE ALBUM_TITLE LIKE @search";
            command.Parameters.AddWithValue("@search", searchWildPhrase);
            command.Connection = connection;
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Album a = new Album
                    {
                        ID = reader.GetInt16(0),
                        AlbumName = reader.GetString(1),
                        ArtistName = reader.GetString(2),
                        Year = reader.GetInt32(3),
                        ImageUrl = reader.GetString(4),
                        Description = reader.GetString(5),
                    };
                    returnAlbums.Add(a);
                }
            }
            connection.Close();
            return returnAlbums;
        }

        public int addOneAlbum(Album album)
        {
           
            //соединяемся с базой данных
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlCommand command = new MySqlCommand("INSERT INTO `albums`( `ALBUM_TITLE`, `ARTIST`, `YEAR`, `IMAGE_NAME`, `DESCRIPTION`)" +
                                                    " VALUES (@albumTitle,@artist,@year,@imageUrl,@description)", connection);
          
            command.Parameters.AddWithValue("@albumTitle",album.AlbumName);
            command.Parameters.AddWithValue("@artist", album.ArtistName);
            command.Parameters.AddWithValue("@year", album.Year);
            command.Parameters.AddWithValue("@imageUrl", album.ImageUrl);
            command.Parameters.AddWithValue("@description", album.Description);

            int newRows= command.ExecuteNonQuery();
            connection.Close();
            return newRows;
        }

        public List<Track> getTracksForAlbum(int albumID)
        {
            //создаем список возвращаемых треков
            List<Track> returnThese = new List<Track>();
            //соединяемся с базой данных
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            MySqlCommand command = new MySqlCommand();
            command.CommandText = "SELECT * FROM  tracks WHERE albums_ID =@albumid";
            command.Parameters.AddWithValue("@albumid", albumID);
            command.Connection = connection;
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Track t = new Track
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Number = reader.GetInt32(2),
                        UrlVideo = reader.GetString(3),
                        Lyrics = reader.GetString(4),
                    };
                    returnThese.Add(t);
                }
            }
            connection.Close();
            return returnThese;
        }

        public List<JObject> getTracksUsingJoin(int albumID)
        {
            //создаем список возвращаемых треков
            List<JObject> returnThese = new List<JObject>();
            //соединяемся с базой данных
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            MySqlCommand command = new MySqlCommand();
            command.CommandText = "SELECT  tracks.ID as tracksID,albums.ALBUM_TITLE, `track_title`,`video_url` FROM `tracks` JOIN albums ON albums_ID=albums.ID " +
                "WHERE albums_ID = @albumid";
            command.Parameters.AddWithValue("@albumid", albumID);
            command.Connection = connection;
            using (MySqlDataReader reader = command.ExecuteReader())
            {
               
                while (reader.Read())
                {
                    JObject newTrack = new JObject();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        newTrack.Add(reader.GetName(i).ToString(), reader.GetValue(i).ToString());
                    }
                    returnThese.Add(newTrack);
                }
            }
            connection.Close();
            return returnThese;
        }
    }
}
