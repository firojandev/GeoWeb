using System;
using System.Configuration;
using GeoService.models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace GeoService
{
	public class DbHelper
	{
    
        private string _connectionString;

        public DbHelper(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
      

        public async Task<List<UserModel>> GetUsersList()
        {
            List<UserModel> list = new List<UserModel>();

            try {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();

                    MySqlCommand cmd = new MySqlCommand("select * from user", conn);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            UserModel user = new UserModel();
                            user.id = Convert.ToInt32(reader["Id"]);
                            user.fullname = reader["fullname"].ToString()!;
                            user.device_id = reader["device_id"].ToString()!;

                            list.Add(user);

                        }
                        reader.Close();
                    }
                    conn.Close();
                }
            }
            catch (Exception e) {
            }
            return list;
        }

        public async Task<List<UserLocation>> GetUserLocationsList()
        {
            List<UserLocation> list = new List<UserLocation>();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();

                    MySqlCommand cmd = new MySqlCommand("select * from user_locations", conn);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            UserLocation userLocation = new UserLocation();
                            userLocation.id = Convert.ToInt32(reader["id"]);
                            userLocation.UserId = Convert.ToInt32(reader["user_id"]);
                            userLocation.lat = Convert.ToDouble(reader["lat"]);
                            userLocation.lng = Convert.ToDouble(reader["lng"]);
                            userLocation.SiteId = Convert.ToInt32(reader["site_id"]);
                            userLocation.SiteName = reader["site_name"].ToString()!;
                            userLocation.ZoneId = Convert.ToInt32(reader["zone_id"]);
                            userLocation.ZoneName = reader["zone_name"].ToString()!;

                            list.Add(userLocation);

                        }
                        reader.Close();
                    }
                    conn.Close();
                }
            }
            catch (Exception e)
            {
            }
            return list;
        }

     

    }
}

