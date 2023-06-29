using CsvHelper;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Craw_MyInstants
{
    class Program
    {
        //static void Main(string[] args)
        //{
        //    EdgeOptions options = new EdgeOptions();
        //    options.AddArgument("--headless");
        //    IWebDriver driver = new EdgeDriver(options);
        //    int i = 1;

        //    while (i < 51)
        //    {
        //        var buttons = new List<ButtonInfo>();
        //        try
        //        {
        //            driver.Navigate().GoToUrl("https://www.myinstants.com/en/index/vn/?page=" + i);
        //            var instantDivs = driver.FindElements(By.CssSelector("div.instant"));
        //            foreach (var div in instantDivs)
        //            {
        //                // Lấy thông tin của button
        //                var button = div.FindElement(By.CssSelector("button[type='button']"));
        //                var onclick = button.GetAttribute("onclick");
        //                var props = onclick.Substring(6, onclick.Length - 5 - 2).Split(',');
        //                var title = '\''+button.GetAttribute("title")+'\'';
        //                var url = "\'"+props[0].Trim();
        //                var loaderId = props[1].Trim();
        //                var slug = props[2].Trim();

        //                // Thêm thông tin của button vào danh sách
        //                buttons.Add(new ButtonInfo
        //                {
        //                    Url = url,
        //                    LoaderId = loaderId,
        //                    Slug = slug,
        //                    Title = title
        //                });
        //            }
        //            if (buttons.Count > 0)
        //            {
        //                Console.WriteLine("---------total=" + buttons.Count);
        //                using (var writer = new StreamWriter("F:/buttons.csv", true))
        //                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
        //                {
        //                    csv.WriteRecords(buttons);
        //                }
        //            }
        //            i++;
        //            Console.WriteLine("---------page=" + i);
        //        }
        //        catch
        //        {

        //        }


        //    }

        //    driver.Quit();
        //}
        static void Main(string[] args)
        {

            // Mở tệp SQLite
            using (var connection = new SQLiteConnection("Data Source=D:/appcuaanhlam.sqlite;Version=3;"))
            {
                connection.Open();
                // Truy vấn thuộc tính URL của bảng dữ liệu
                using (var command = new SQLiteCommand("SELECT url FROM data", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            while (true)
                            {
                                try
                                {
                                    var url = (string)reader["url"];

                                    // Download file từ đường dẫn https://www.myinstants.com/<url> và lưu vào D:/data/
                                    var filename = Path.GetFileName(url);
                                    var downloadUrl = $"https://www.myinstants.com{url}";
                                    var savePath = Path.Combine("D:/data", filename);

                                    using (var client = new WebClient())
                                    {
                                        client.DownloadFile(downloadUrl, savePath);
                                        Console.WriteLine(savePath);
                                    }
                                    break;
                                }
                                catch
                                {

                                }
                            }
                           
                        }
                    }
                }
            }
        }
    }
}
