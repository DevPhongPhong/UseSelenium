using System;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {
        static async Task Main(string[] args)
        {

            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://semamediadata-image-ocr-v1.p.rapidapi.com/"),
                Headers =
                {
                    { "X-RapidAPI-Key", "SIGN-UP-FOR-KEY" },
                    { "X-RapidAPI-Host", "semamediadata-image-ocr-v1.p.rapidapi.com" },
                },
                            Content = new MultipartFormDataContent
                {
                    new StringContent("json")
                    {
                        Headers =
                        {
                            ContentDisposition = new ContentDispositionHeaderValue("form-data")
                            {
                                Name = "outform",
                            }
                        }
                    },
                    new StringContent("")
                    {
                        Headers =
                        {
                            ContentType = new MediaTypeHeaderValue("application/octet-stream"),
                            ContentDisposition = new ContentDispositionHeaderValue("form-data")
                            {
                                Name = "file",
                                FileName = "a.jpg",
                            }
                        }
                    },
                    new StringContent("en")
                    {
                        Headers =
                        {
                            ContentDisposition = new ContentDispositionHeaderValue("form-data")
                            {
                                Name = "lang",
                            }
                        }
                    },
                    new StringContent("True")
                    {
                        Headers =
                        {
                            ContentDisposition = new ContentDispositionHeaderValue("form-data")
                            {
                                Name = "noempty",
                            }
                        }
                    },
                    new StringContent("True")
                    {
                        Headers =
                        {
                            ContentDisposition = new ContentDispositionHeaderValue("form-data")
                            {
                                Name = "uc",
                            }
                        }
                    },
                },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                Console.WriteLine(body);
            }
        }
    }
}
