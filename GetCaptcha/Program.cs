using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using SkiaSharp;
using SkiaSharp.Extended.Svg;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SKSvg = SkiaSharp.Extended.Svg.SKSvg;

namespace GetCaptcha
{
    class Program
    {
        static void Main(string[] args)
        {
            // Khởi tạo HttpClient
            HttpClient client = new HttpClient();

            // Lặp 1000 lần
            for (int i = 0; i < 1000; i++)
            {
                // Gọi API và lấy nội dung trả về
                HttpResponseMessage response = client.GetAsync("https://hoadondientu.gdt.gov.vn:30000/captcha").Result;
                string json = response.Content.ReadAsStringAsync().Result;

                // Tạo json obj 
                JObject obj = JObject.Parse(json);
                // Lấy value content và Thay thế chuỗi \" thành "
                string content = obj.SelectToken("content").ToString().Replace("\\\"", "\"");
                // tạo svg obj
                SKSvg svg = new SKSvg();
                svg.Load(new MemoryStream(Encoding.UTF8.GetBytes(content)));

                // Tạo đối tượng SKBitmap và vẽ đối tượng SKSvg lên đó
                SKBitmap skBitmap = new SKBitmap((int)svg.CanvasSize.Width, (int)svg.CanvasSize.Height);
                SKCanvas canvas = new SKCanvas(skBitmap);
                canvas.DrawPicture(svg.Picture);
                using (MemoryStream stream = new MemoryStream())
                {
                    skBitmap.Encode(stream, SKEncodedImageFormat.Png, 100);
                    Bitmap bitmap = new Bitmap(stream);
                    // Tạo ảnh mới với background màu trắng
                    Bitmap newBitmap = new Bitmap(bitmap.Width, bitmap.Height, PixelFormat.Format32bppArgb);
                    using (Graphics graphics = Graphics.FromImage(newBitmap))
                    {
                        graphics.Clear(Color.White);
                        graphics.DrawImage(bitmap, 0, 0);
                    }

                    // Lưu ảnh mới
                    newBitmap.Save("newimg/img" + i + ".png", ImageFormat.Png);
                }
            }
        }
    }
}
