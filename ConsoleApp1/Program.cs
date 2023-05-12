using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UseSelenium;

namespace ConsoleApp1
{
    class Program
    {
        //const string ClassName_Element_Modal_Close_Button = "ant-modal-close";
        //const string Id_Element_MST_Nguoi_ban = "nbmst";
        //const string CssSelector_Element_Loai_Hoa_Don = "div.ant-select-selection";
        //const string Id_Element_Ky_Hieu_Hoa_Don = "khhdon";
        //const string Id_Element_So_Hoa_Don = "shdon";
        //const string Id_Element_Tong_Tien_Thue = "tgtthue";
        //const string Id_Element_Tong_Tien_Thanh_Toan = "tgtttbso";
        //const string Id_Element_Input_Captcha = "cvalue";
        //const string CssSelector_Element_Form_Tra_Cuu_Submit_Button = "button.ant-btn.ButtonAnt__Button-sc-p5q16s-0.foIGVG.ant-btn-primary";
        //const string CssSelector_Danh_Sach_Loai_Hoa_Don_li = "ul.ant-select-dropdown-menu > li";
        //const string CssSelector_Response = "section.styles__SearchContentBox-sc-1ljhobs-0";
        //static void Main(string[] args)
        //{
        //    ////Khoi tao drive khong show browser
        //    //EdgeOptions options = new EdgeOptions();
        //    //options.AddArgument("--headless"); 
        //    //IWebDriver driver = new EdgeDriver(options);

        //    //Khoi tao drive show browser
        //    IWebDriver driver = new EdgeDriver();

        //    int countLoad = 0;
        //    while (true)
        //    {
        //        // Truy cập trang đăng nhập
        //        try {
        //            driver.Navigate().GoToUrl("https://hoadondientu.gdt.gov.vn/");
        //        }
        //        catch (Exception ex)
        //        {
        //            countLoad++;
        //            if (countLoad == 2) throw new Exception("Không tải được trang");
        //        }
        //    }


        //    // Tìm element 
        //    IWebElement Modal_Close_Button = driver.FindElement(By.ClassName(ClassName_Element_Modal_Close_Button));
        //    IWebElement MST_Nguoi_ban = driver.FindElement(By.Id(Id_Element_MST_Nguoi_ban));
        //    IWebElement Loai_Hoa_Don = driver.FindElement(By.CssSelector(CssSelector_Element_Loai_Hoa_Don));
        //    IWebElement Ky_Hieu_Hoa_Don = driver.FindElement(By.Id(Id_Element_Ky_Hieu_Hoa_Don));
        //    IWebElement So_Hoa_Don = driver.FindElement(By.Id(Id_Element_So_Hoa_Don));
        //    IWebElement Tong_Tien_Thue = driver.FindElement(By.Id(Id_Element_Tong_Tien_Thue));
        //    IWebElement Tong_Tien_Thanh_Toan = driver.FindElement(By.Id(Id_Element_Tong_Tien_Thanh_Toan));
        //    IWebElement Input_Captcha = driver.FindElement(By.Id(Id_Element_Input_Captcha));
        //    IWebElement Submit_Button = driver.FindElement(By.CssSelector(CssSelector_Element_Form_Tra_Cuu_Submit_Button));
        //    ReadOnlyCollection<IWebElement> Danh_Sach_Loai_Hoa_Don_li = new ReadOnlyCollection<IWebElement>(new List<IWebElement>());

        //    // Chuan Bi Data
        //    string MST_Nguoi_ban_Data = "0100686209-999";
        //    int Loai_Hoa_Don_Data = 0;
        //    string Ky_Hieu_Hoa_Don_Data = "C23TYA";
        //    string So_Hoa_Don_Data = "13";
        //    string Tong_Tien_Thue_Data = "0";
        //    string Tong_Tien_Thanh_Toan_Data = "0";

        //    // Điền và thao tác thông tin 
        //    Modal_Close_Button.Click();
        //    MST_Nguoi_ban.SendKeys(MST_Nguoi_ban_Data);
        //    Loai_Hoa_Don.Click();
        //    int count = 0;
        //    while (true)
        //    {
        //        try
        //        {
        //            Danh_Sach_Loai_Hoa_Don_li = driver.FindElements(By.CssSelector(CssSelector_Danh_Sach_Loai_Hoa_Don_li));
        //            break;
        //        }
        //        catch
        //        {
        //            count++;
        //            if (count == 5) throw new Exception("Không tải được danh sách loại hóa đơn");
        //        }
        //    }

        //    Danh_Sach_Loai_Hoa_Don_li[Loai_Hoa_Don_Data].Click();
        //    Ky_Hieu_Hoa_Don.SendKeys(Ky_Hieu_Hoa_Don_Data);
        //    So_Hoa_Don.SendKeys(So_Hoa_Don_Data);
        //    Tong_Tien_Thue.SendKeys(Tong_Tien_Thue_Data);
        //    Tong_Tien_Thanh_Toan.SendKeys(Tong_Tien_Thanh_Toan_Data);
        //    Input_Captcha.SendKeys("BT8APH");
        //    Submit_Button.Click();

        //    // Đợi trang tải xong
        //    driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(2);

        //    // lấy kết quả trả về
        //    IWebElement response;
        //    while (count < 5)
        //    {
        //        try
        //        {
        //             response = driver.FindElement(By.CssSelector(CssSelector_Response));
        //            break;
        //        }
        //        catch(Exception e)
        //        {
        //            count++;
        //            if (count == 5) throw new Exception("Không nhận được kết quả");
        //        }
        //    }


        //    // Đóng trình duyệt
        //    driver.Quit();
        //}

        static void Main(string[] args)
        {
            Browser browser = new Browser("https://hoadondientu.gdt.gov.vn/");
            browser.Load();
            browser.PostHoaDon(new HoaDonModel
            {
                MST_Nguoi_ban_Data = "0100686209-999",
                Loai_Hoa_Don_Data = 0,
                Ky_Hieu_Hoa_Don_Data = "C23TYA",
                So_Hoa_Don_Data = "13",
                Tong_Tien_Thue_Data = "0",
                Tong_Tien_Thanh_Toan_Data = "0"
            });
            byte[] ba = browser.GetScreenShot();
            ThueResponse res = browser.GetResponse();
            
            Console.WriteLine(res.Message);


            Console.Read();
        }
    }
}
