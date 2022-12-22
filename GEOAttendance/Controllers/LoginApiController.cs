using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using System.Xml.Linq;
using GEOAttendance.Services;
using GeoService.models;
using GeoService.services;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI;
using MySqlX.XDevAPI.Relational;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GEOAttendance.Controllers
{

    [Route("api/[controller]")]
    public class LoginApiController : Controller
    {
        public IUserService _iUserService;

        public LoginApiController(IUserService iUserService)
        {
            _iUserService = iUserService;
        }

        //http://localhost:5219/api/LoginApi/userData 

        //[Route("[action]")]
        //[HttpPost]
        //[Consumes("application/x-www-form-urlencoded")]
        //public async Task<JsonResult> userData([FromForm] string device_uid)

        [Route("[action]/{device_uid}/info")]
        public async Task<JsonResult> userData(string device_uid)
        {
            UserModel data = await _iUserService.GetUser(device_uid);

            if (data.id == 0 || data.fullname == null) {
                return Json(new {
                    code = 200,
                    status = "success",
                    message = "No user found",
                    data = default(Plane[])
                });
            }

            var user_id = data.id;
            var fullname = data.fullname;
            var device_id = data.device_id;
            var email = data.email;
            var phone = data.phone;
            var validtill = data.valid_till;

            var zone153 = new {
                id = 153,
                name = "Altaf Home Zone",
                site_id = 36,
                client_system_site_id = "12",
                client_id = 28,
                geo_json = new {
                    type = "Feature",
                    geometry = new {
                        type = "Polygon",
                        coordinates = new[] {
                            new[]{90.423692,23.781732},
                            new[]{90.423175,23.780805},
                            new[]{ 90.423656,23.779911},
                            new[]{ 90.424784,23.779676},
                            new[]{ 90.425948,23.779893},
                            new[]{ 90.425914,23.781692},
                            new[]{  90.423692,23.781732},
                        }

                    }

                },
                latitude = 23.781732,
                longitude= 90.423692,
                is_hazardous= 0,
                requires_ppe= 0,
                is_non_human= 0,
                unequipped_access_message = "s",
                entry_message= "Welcome to Altaf Zone",
                exit_message= "You have left Altaf's Zone",
                status= "Normal",
                is_active= 1
            };
            var zone158 = new
            {
                id = 158,
                name = "Altaf Zone 2 - Gulsan 1 Dhaka",
                site_id = 36,
                client_system_site_id = "12",
                client_id = 28,
                geo_json = new
                {
                    type = "Feature",
                    geometry = new
                    {
                        type = "Polygon",
                        coordinates = new[] {
                            new[]{90.416232,23.779896},
                            new[]{90.423175,23.780805},
                            new[]{90.416158,23.780538},
                            new[]{90.41688,23.781162},
                            new[]{90.425948,23.779893},
                            new[]{90.417937,23.78104},
                            new[]{90.41761,23.779945},
                            new[]{90.416495,23.779686},
                            new[]{90.416232,23.779896},
                        }

                    }

                },
                latitude = 23.779896,
                longitude = 90.416232,
                is_hazardous = 0,
                requires_ppe = 0,
                is_non_human = 0,
                unequipped_access_message = "d",
                entry_message = "Welcome altaf at gulshan 1",
                exit_message = "Bye bye altaf from gulshan 1",
                status = "Normal",
                is_active = 1
            };
            var zone214 = new
            {
                id = 214,
                name = "AltafOverlappedZone",
                site_id = 36,
                client_system_site_id = "12",
                client_id = 28,
                geo_json = new
                {
                    type = "Feature",
                    geometry = new
                    {
                        type = "Polygon",
                        coordinates = new[] {
                            new[]{90.416232,23.779896},
                            new[]{90.424927,23.78143},
                            new[]{90.425945,23.781224},
                            new[]{90.426023,23.780527},
                            new[]{90.426,23.780076},
                            new[]{90.425613,23.780023},
                            new[]{90.424027,23.780178},
                            new[]{90.424056,23.781211}
                        }
                    }
                },
                latitude = 23.781211,
                longitude = 90.424056,
                is_hazardous = 0,
                requires_ppe = 0,
                is_non_human = 0,
                unequipped_access_message = "d",
                entry_message = "You are entering at overlapped zone",
                exit_message = "You are leaving from overlapped zone",
                status = "Normal",
                is_active = 1
            };

            var zone387 = new
            {
                id = 387,
                name = "JNU Zone",
                site_id = 36,
                client_system_site_id = "12",
                client_id = 28,
                geo_json = new
                {
                    type = "Feature",
                    geometry = new
                    {
                        type = "Polygon",
                        coordinates = new[] {
                            new[]{90.26541112156076,23.878184300115876},
                            new[]{90.26776042822473,23.878204824558836},
                            new[]{90.26798488427568,23.876378136425544},
                            new[]{90.26601715289775,23.876364453271293},
                            new[]{90.26484998143337,23.876802313495816},
                            new[]{90.26414668580753,23.87782854259457},
                            new[]{90.26541112156076,23.878184300115876}
                        }
                    }
                },
                latitude = 23.878184300116,
                longitude = 90.265411121561,
                is_hazardous = 0,
                requires_ppe = 0,
                is_non_human = 0,
                unequipped_access_message = "d",
                entry_message = "Welcome to JNU zone",
                exit_message = "Bye bye from JNU zone",
                status = "Normal",
                is_active = 1
            };


            var myResponse = new {
                code = 200,
                status = "success",
                message = "Okay",
                data = new {
                    id = user_id,
                    name = fullname,
                    client_id = 28,
                    site_id = 36,
                    client_system_site_id = "AltafAppDev",
                    frame_interval = "3",
                    capture_type = "unfiltered",
                    capture_quality = "low",
                    login_code = new {
                        id = 442,
                        uuid = "efc9fac1-910a-4bd6-8d95-2ac0c35995d9",
                        device_id = 40,
                        device_uid = device_id,
                        local_valid_from = "2022-11-28 14:36:50",
                        valid_from = "2022-11-28 08:36:50",
                        local_valid_till = "2023-01-27 14:36:50",
                        valid_till = validtill,
                        client_id = 28,
                        user_id = user_id,
                        last_login_at = "2022-12-08 18:46:26",
                        is_active = 1
                    },
                    user = new {
                        id = user_id,
                        name = fullname,
                        email = email
                    },
                    client = new {
                        name = fullname,
                        email = email
                    },
                    site = new {
                        name = "AltafAppDevSite",
                        geo_json = new {
                            geometry = new {
                                type = "Polygon",
                                coordinates = new[] { new[] {90.42263552187688, 23.781630670810628 },new[] { 90.422608978489, 23.781278471686463 } }
                            }
                        },
                        zones = new[] { zone153, zone158, zone214, zone387 }

                    }
                    

                }
            };


            return Json(myResponse);
           
        }

    }
}

