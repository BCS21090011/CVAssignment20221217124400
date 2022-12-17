using System.Drawing;
using System.IO;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using CVAssignment20221217124400.Models;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Drawing.Imaging;

namespace UsingAPI
{

    public class TargetAPI
    {
        public string PredictionKey { get; set; }
        public string PredictionURL { get; set; }
        public HttpClient Client { get; set; }
        public TargetAPI(string predictionKey, string predictionURL)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Prediction-Key", predictionKey);
            this.PredictionKey = predictionKey;
            this.PredictionURL = predictionURL;
            this.Client = client;
        }

        public ByteArrayContent GetImgContent(string imgFileName)
        {
            Stream file = File.OpenRead(imgFileName);
            byte[] imgBytes = FileReader.ReadFully(file);
            ByteArrayContent content = new ByteArrayContent(imgBytes);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

            return content;
        }

        public ByteArrayContent GetImgContent(Bitmap img)
        {
            byte[] imgBytes = FileReader.ReadFully(img);
            ByteArrayContent content = new ByteArrayContent(imgBytes);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

            return content;
        }

        public async Task<MyPredictionModel> GetPredictionsAsync(ByteArrayContent content)
        {

            HttpResponseMessage resMessg = await this.Client.PostAsync(this.PredictionURL, content);
            string str = await resMessg.Content.ReadAsStringAsync();
            MyPredictionModel predModel = JsonConvert.DeserializeObject<MyPredictionModel>(str);

            return predModel;

        }

        public Task<MyPredictionModel> GetPredictionsAsync(string imgFileName)
        {
            ByteArrayContent content = GetImgContent(imgFileName);
            return GetPredictionsAsync(content);
        }

        public Task<MyPredictionModel> GetPredictionsAsync(Bitmap img)
        {
            ByteArrayContent content = GetImgContent(img);
            return GetPredictionsAsync(content);
        }

        public class FileReader
        {
            public static byte[] ReadFully(Stream input)
            {
                byte[] buffer = new byte[16 * 1024];
                using (MemoryStream ms = new MemoryStream())
                {
                    int read;
                    while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        ms.Write(buffer, 0, read);
                    }
                    return ms.ToArray();
                }
            }

            public static byte[] ReadFully(Bitmap input)
            {
                byte[] buffer = new byte[16 * 1024];
                using (MemoryStream ms = new MemoryStream())
                {
                    // Save the image to the memory stream in JPEG format
                    input.Save(ms, ImageFormat.Jpeg);

                    // Get the byte array from the memory stream
                    buffer = ms.ToArray();
                }
                return buffer;
            }
        }

    }

}