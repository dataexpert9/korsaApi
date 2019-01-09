using Component;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Utility
{
    public static class ImageHelper
    {
        public enum ImageSavingEnum
        {
            UploadedSuccessfully = 0,
            InvalidExtension = 1,
            MaxSizeExceeded = 2,
        }
        public class ImageModel
        {
            public string Name { get; set; }
            public string ImageString { get; set; }

        }

        public static ImageSavingEnum SaveImage(string CurrentDirectory, string FolderName, IFormFile ImageFile, out string returnPath)
        {
            try
            {
                IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".gif", ".png" };
                string ext = Path.GetExtension(ImageFile.FileName).ToLower();
                string ImageUrl = Guid.NewGuid().ToString() + ext;
                FolderName = "FileDirectory\\" + FolderName + "\\";
                returnPath = FolderName + ImageUrl;
                FolderName = Path.Combine(CurrentDirectory, FolderName);
                if (!AllowedFileExtensions.Contains(ext))
                    return ImageSavingEnum.InvalidExtension;

                else if (ImageFile.Length > Global.ImageMaxSize)
                    return ImageSavingEnum.MaxSizeExceeded;

                if (!Directory.Exists(FolderName))
                    Directory.CreateDirectory(FolderName);

                using (var stream = File.OpenWrite(returnPath))
                {
                    stream.Position = 0;
                    ImageFile.CopyTo(stream);
                }
                return ImageSavingEnum.UploadedSuccessfully;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }




        public static ImageSavingEnum SaveImageForWebPanel(string CurrentDirectory, string FolderName, ImageModel model, out string returnPath)
        {
            try
            {
                IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".gif", ".png" };
                byte[] bytes = Convert.FromBase64String(model.ImageString);
                string ext = Path.GetExtension(model.Name).ToLower();
                string ImageUrl = Guid.NewGuid().ToString() + ext;
                FolderName = "FileDirectory\\" + FolderName + "\\";
                returnPath = FolderName + ImageUrl;
                FolderName = Path.Combine(CurrentDirectory, FolderName);
                if (!AllowedFileExtensions.Contains(ext))
                    return ImageSavingEnum.InvalidExtension;

                if (!Directory.Exists(FolderName))
                    Directory.CreateDirectory(FolderName);

                File.WriteAllBytes(returnPath, bytes);
                return ImageSavingEnum.UploadedSuccessfully;
            }
            catch (Exception ex)
            {

                throw ex;
            }
       
        }

        public static string SaveFileFromGoogleMap(string CurrentDirectory, string FolderName, string OriginLocationLongitude, string OriginLocationLatitude, string DestinationLocationLongitude, string DestinationLocationLatitude)
        {
            try
            {
                string gMapDirectionUrl = @"http://maps.googleapis.com/maps/api/directions/json?origin=" + OriginLocationLatitude + "," + OriginLocationLongitude + "&destination=" + DestinationLocationLatitude + "," + DestinationLocationLongitude + "&sensor=false";
                WebRequest directionReq = WebRequest.Create(gMapDirectionUrl);
                WebResponse directionResponse = directionReq.GetResponse();
                Stream data = directionResponse.GetResponseStream();
                StreamReader reader = new StreamReader(data);
                string json = reader.ReadToEnd();
                directionResponse.Close();
                JObject jObject = JObject.Parse(json);
                string overview_polyline = (string)jObject.SelectToken("routes[0].overview_polyline.points");

                string ImageUrl = Guid.NewGuid().ToString() + ".png";
                FolderName = "FileDirectory\\" + FolderName + "\\";
                string returnPath = FolderName + ImageUrl;
                FolderName = Path.Combine(CurrentDirectory, FolderName);
                if (!Directory.Exists(FolderName))
                    Directory.CreateDirectory(FolderName);
                ImageUrl = FolderName + ImageUrl;
                string gStaticMapUrl = "http://maps.googleapis.com/maps/api/staticmap?size=400x150&path=weight:3|color:blue|enc:" + overview_polyline + "&markers=color:blue|label:S|" + OriginLocationLatitude + "," + OriginLocationLongitude + "&markers=color:blue|label:D|" + DestinationLocationLatitude + "," + DestinationLocationLongitude + "&sensor=false";
                using (var client = new WebClient())
                {
                    client.DownloadFile(gStaticMapUrl, ImageUrl);
                }
                return returnPath;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async static Task<string> SaveImageUrlFromGoogleMap(string CurrentDirectory, string FolderName, string OriginLocationLongitude, string OriginLocationLatitude, string DestinationLocationLongitude, string DestinationLocationLatitude, string GoogleMapsAPIKey)
        {
            try
            {
                string overviewPath = "";
                string webImageUrl = "";
                var _uri = new Uri("https://maps.googleapis.com/maps/api/directions/json?origin=" + OriginLocationLatitude + "," + OriginLocationLongitude + "&destination=" + DestinationLocationLatitude + "," + DestinationLocationLongitude + "&key=" + GoogleMapsAPIKey);
                using (HttpClient _client = new HttpClient())
                {
                    HttpResponseMessage response = await _client.GetAsync(_uri).ConfigureAwait(false);

                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        response = await _client.GetAsync(_uri).ConfigureAwait(false);
                    }
                    if (response.IsSuccessStatusCode)
                    {
                        var directions = await response.Content.ReadAsStringAsync();

                        var dirObject = JObject.Parse(directions); // Newtonsoft.Json.JsonConvert.DeserializeObject(directions);
                        if (dirObject["routes"] != null)
                        {
                            overviewPath = dirObject["routes"][0]["overview_polyline"]["points"].ToString();
                        }
                    }
                }
                string ImageUrl = Guid.NewGuid().ToString() + ".png";
                FolderName = "FileDirectory\\" + FolderName + "\\";
                string returnPath = FolderName + ImageUrl;
                FolderName = Path.Combine(CurrentDirectory, FolderName);
                if (!Directory.Exists(FolderName))
                    Directory.CreateDirectory(FolderName);
                ImageUrl = FolderName + ImageUrl;

                webImageUrl = "http://maps.googleapis.com/maps/api/staticmap?size=400x150&path=weight:3|color:blue|enc:" + overviewPath + "&markers=color:blue|label:S|" + OriginLocationLatitude + "," + OriginLocationLongitude + "&markers=color:blue|label:D|" + DestinationLocationLatitude + "," + DestinationLocationLongitude + "&sensor=false&key=" + GoogleMapsAPIKey;
                WebClient webClient = new WebClient();
                webClient.Headers.Add("User-Agent: Other");
                webClient.DownloadFile(webImageUrl, ImageUrl);
                return returnPath;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


    }
}
