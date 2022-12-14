using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Xml.Linq;
using GeoService.models;
using GeoService.services;
using Google.Protobuf.WellKnownTypes;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Utilities.Collections;

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

                    MySqlCommand cmd = new MySqlCommand("select * from user ORDER BY id desc", conn);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            UserModel user = new UserModel();
                            user.id = Convert.ToInt32(reader["Id"]);
                            user.fullname = reader["fullname"].ToString()!;
                            user.device_id = reader["device_id"].ToString()!;
                            user.email = reader["email"].ToString()!;
                            user.phone = reader["phone_number"].ToString()!;
                            user.valid_till = reader["valid_till"].ToString()!;
                            user.created_at = reader["created_at"].ToString()!;
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

        public async Task<UserModel> GetUser(int id)
        {
            UserModel user = new UserModel();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();

                    MySqlCommand cmd = new MySqlCommand("select * from user where id = " + id, conn);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            user.id = Convert.ToInt32(reader["Id"]);
                            user.fullname = reader["fullname"].ToString()!;
                            user.device_id = reader["device_id"].ToString()!;
                            user.email = reader["email"].ToString()!;
                            user.phone = reader["phone_number"].ToString()!;
                            user.valid_till = reader["valid_till"].ToString()!;
                            user.created_at = reader["created_at"].ToString()!;
                      
                        }
                        reader.Close();
                    }
                    conn.Close();
                }
            }
            catch (Exception e)
            {
            }
            return user;
        }

        public async Task<Boolean> UpdateUser(int id, string datetime)
        {
            Boolean isSuccess = false;
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    MySqlCommand copmmand = new MySqlCommand("UPDATE user SET valid_till=@validtill WHERE id=@id", conn);
                  
                    copmmand.Parameters.AddWithValue("@id", id);
                    copmmand.Parameters.AddWithValue("@validtill", datetime);
                    copmmand.ExecuteNonQuery();

                    conn.Close();

                    isSuccess = true;
                }
            }
            catch (Exception e)
            {
                isSuccess = false;
            }
            return isSuccess;
        }

        public async Task<List<GeoService.models.UserLocation>> GetUserLocationsList()
        {
            List<GeoService.models.UserLocation> list = new List<GeoService.models.UserLocation>();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();

                    MySqlCommand cmd = new MySqlCommand("select * from user_locations ORDER BY id desc", conn);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            GeoService.models.UserLocation userLocation = new GeoService.models.UserLocation();
                            userLocation.id = Convert.ToInt32(reader["id"]);
                            userLocation.user_id = Convert.ToInt32(reader["user_id"]);
                            userLocation.user_fullname = reader["user_fullname"].ToString();
                            userLocation.lat = Convert.ToDouble(reader["lat"]);
                            userLocation.lng = Convert.ToDouble(reader["lng"]);
                            userLocation.site_id = Convert.ToInt32(reader["site_id"]);
                            userLocation.site_name = reader["site_name"].ToString()!;
                            userLocation.zone_id = Convert.ToInt32(reader["zone_id"]);
                            userLocation.zone_name = reader["zone_name"].ToString()!;
                            userLocation.created_at = reader["created_at"].ToString()!;

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

        public async Task<List<AnnouceMessageModel>> GetMessagesList()
        {
            List<AnnouceMessageModel> list = new List<AnnouceMessageModel>();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();

                    MySqlCommand cmd = new MySqlCommand("select * from announce_messages ORDER BY id desc", conn);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            AnnouceMessageModel aModel = new AnnouceMessageModel();
                            aModel.id = Convert.ToInt64(reader["id"]);
                            aModel.user_id = Convert.ToInt64(reader["user_id"]);
                            aModel.user_fullname = reader["user_fullname"].ToString();
                            aModel.message = reader["message"].ToString()!;
                            aModel.created_at = reader["created_at"].ToString()!;

                            list.Add(aModel);

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

        public async Task<AnnouceMessageModel> SaveMessage(AnnouceMessageModel annouce)
        {
            AnnouceMessageModel announceModel = new AnnouceMessageModel();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();

                    MySqlCommand comm = conn.CreateCommand();
                    comm.CommandText = "INSERT INTO announce_messages(user_id,user_fullname,message) VALUES(@user_id, @user_fullname, @message)";
                    comm.Parameters.AddWithValue("@user_id", annouce.user_id);
                    comm.Parameters.AddWithValue("@user_fullname", annouce.user_fullname);
                    comm.Parameters.AddWithValue("@message", annouce.message);
                    comm.ExecuteNonQuery();

                    long lastInsertedId = comm.LastInsertedId;

                    MySqlCommand cmdGet = new MySqlCommand("select * from announce_messages where id = " + lastInsertedId, conn);

                    using (var reader = cmdGet.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            announceModel.id = Convert.ToInt64(reader["id"]);
                            announceModel.user_id = Convert.ToInt64(reader["user_id"]);
                            announceModel.user_fullname = reader["user_fullname"].ToString();
                            announceModel.message = reader["message"].ToString()!;
                            announceModel.created_at = reader["created_at"].ToString()!;
                        }
                        reader.Close();
                    }

                    conn.Close();
                }
            }
            catch (Exception e)
            {
            }
            return announceModel;
        }

        public async Task<Boolean> SaveLocation(models.UserLocation location)
        {
            Boolean isSuccess = false;

            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();

                    MySqlCommand comm = conn.CreateCommand();
                    comm.CommandText = "INSERT INTO user_locations(user_id,user_fullname,lat,lng,site_id,site_name,zone_id,zone_name) VALUES(@user_id, @user_fullname, @lat, @lng, @site_id, @site_name, @zone_id, @zone_name)";
                    comm.Parameters.AddWithValue("@user_id", location.site_id);
                    comm.Parameters.AddWithValue("@user_fullname", location.user_fullname);
                    comm.Parameters.AddWithValue("@lat", location.lat);
                    comm.Parameters.AddWithValue("@lng", location.lng);
                    comm.Parameters.AddWithValue("@site_id", location.site_id);
                    comm.Parameters.AddWithValue("@site_name", location.site_name);
                    comm.Parameters.AddWithValue("@zone_id", location.zone_id);
                    comm.Parameters.AddWithValue("@zone_name", location.zone_name);
                    comm.ExecuteNonQuery();

                    isSuccess = true;

                    conn.Close();
                }
            }
            catch (Exception e)
            {
                isSuccess = false;
            }

            return isSuccess;
        }

        public async Task<Boolean> SaveImage(models.UserImage userImage)
        {
            Boolean isSuccess = false;

            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();

                    MySqlCommand comm = conn.CreateCommand();
                    comm.CommandText = "INSERT INTO user_images(user_id,user_fullname,image,site_id,site_name,zone_id,zone_name) VALUES(@user_id, @user_fullname, @image, @site_id, @site_name, @zone_id, @zone_name)";
                    comm.Parameters.AddWithValue("@user_id", userImage.site_id);
                    comm.Parameters.AddWithValue("@user_fullname", userImage.user_fullname);
                    comm.Parameters.AddWithValue("@image", userImage.image);
                    comm.Parameters.AddWithValue("@site_id", userImage.site_id);
                    comm.Parameters.AddWithValue("@site_name", userImage.site_name);
                    comm.Parameters.AddWithValue("@zone_id", userImage.zone_id);
                    comm.Parameters.AddWithValue("@zone_name", userImage.zone_name);
                    comm.ExecuteNonQuery();

                    isSuccess = true;

                    conn.Close();
                }
            }
            catch (Exception e)
            {
                isSuccess = false;
            }

            return isSuccess;
        }

        public async Task<List<GeoService.models.UserImage>> GetUserImagesList()
        {
            List<GeoService.models.UserImage> list = new List<GeoService.models.UserImage>();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();

                    MySqlCommand cmd = new MySqlCommand("select * from user_images ORDER BY id desc ", conn);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            GeoService.models.UserImage userImage = new GeoService.models.UserImage();
                            userImage.id = Convert.ToInt32(reader["id"]);
                            userImage.user_id = Convert.ToInt32(reader["user_id"]);
                            userImage.user_fullname = reader["user_fullname"].ToString();
                            userImage.image = reader["image"].ToString()!;
                            userImage.site_id = Convert.ToInt32(reader["site_id"]);
                            userImage.site_name = reader["site_name"].ToString()!;
                            userImage.zone_id = Convert.ToInt32(reader["zone_id"]);
                            userImage.zone_name = reader["zone_name"].ToString()!;
                            userImage.created_at = reader["created_at"].ToString()!;

                            list.Add(userImage);

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

