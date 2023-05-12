using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tesseract;

namespace UseSelenium
{
    public class Browser : IDisposable
    {
        private IWebDriver Driver { get; set; }
        private EdgeOptions Options { get; set; }
        private string Url { get; }
        public Browser(string url, bool showBrowser = true)
        {
            if (!showBrowser)
            {
                Options = new EdgeOptions();
                Options.AddArgument("--headless");
                Driver = new EdgeDriver(Options);
            }
            else
            {
                Driver = new EdgeDriver();
            }
            Url = url;
        }

        public void Load()
        {
            int countLoad = 0;
            while (true)
            {
                try
                {
                    Driver.Navigate().GoToUrl(Url);
                    return;
                }
                catch (Exception ex)
                {
                    countLoad++;
                    if (countLoad == 2)
                        throw new Exception("Không tải được trang");
                }
            }
        }

        public void PostHoaDon(HoaDonModel model)
        {
            string ClassName_Element_Modal_Close_Button = "ant-modal-close";
            string Id_Element_MST_Nguoi_ban = "nbmst";
            string CssSelector_Element_Loai_Hoa_Don = "div.ant-select-selection";
            string Id_Element_Ky_Hieu_Hoa_Don = "khhdon";
            string Id_Element_So_Hoa_Don = "shdon";
            string Id_Element_Tong_Tien_Thue = "tgtthue";
            string Id_Element_Tong_Tien_Thanh_Toan = "tgtttbso";
            string Id_Element_Input_Captcha = "cvalue";
            string CssSelector_Element_Form_Tra_Cuu_Submit_Button = "button.ant-btn.ButtonAnt__Button-sc-p5q16s-0.foIGVG.ant-btn-primary";
            string CssSelector_Danh_Sach_Loai_Hoa_Don_li = "ul.ant-select-dropdown-menu > li";

            IWebElement Modal_Close_Button = Driver.FindElement(By.ClassName(ClassName_Element_Modal_Close_Button));
            IWebElement MST_Nguoi_ban = Driver.FindElement(By.Id(Id_Element_MST_Nguoi_ban));
            IWebElement Loai_Hoa_Don = Driver.FindElement(By.CssSelector(CssSelector_Element_Loai_Hoa_Don));
            IWebElement Ky_Hieu_Hoa_Don = Driver.FindElement(By.Id(Id_Element_Ky_Hieu_Hoa_Don));
            IWebElement So_Hoa_Don = Driver.FindElement(By.Id(Id_Element_So_Hoa_Don));
            IWebElement Tong_Tien_Thue = Driver.FindElement(By.Id(Id_Element_Tong_Tien_Thue));
            IWebElement Tong_Tien_Thanh_Toan = Driver.FindElement(By.Id(Id_Element_Tong_Tien_Thanh_Toan));
            IWebElement Input_Captcha = Driver.FindElement(By.Id(Id_Element_Input_Captcha));
            IWebElement Submit_Button = Driver.FindElement(By.CssSelector(CssSelector_Element_Form_Tra_Cuu_Submit_Button));
            ReadOnlyCollection<IWebElement> Danh_Sach_Loai_Hoa_Don_li;

            Modal_Close_Button.Click();
            MST_Nguoi_ban.SendKeys(model.MST_Nguoi_ban_Data);
            Loai_Hoa_Don.Click();
            int count = 0;
            while (true)
            {
                try
                {
                    Danh_Sach_Loai_Hoa_Don_li = Driver.FindElements(By.CssSelector(CssSelector_Danh_Sach_Loai_Hoa_Don_li));
                    break;
                }
                catch
                {
                    count++;
                    if (count == 5) throw new Exception("Không tải được danh sách loại hóa đơn");
                }
            }

            Danh_Sach_Loai_Hoa_Don_li[model.Loai_Hoa_Don_Data].Click();
            Ky_Hieu_Hoa_Don.SendKeys(model.Ky_Hieu_Hoa_Don_Data);
            So_Hoa_Don.SendKeys(model.So_Hoa_Don_Data);
            Tong_Tien_Thue.SendKeys(model.Tong_Tien_Thue_Data);
            Tong_Tien_Thanh_Toan.SendKeys(model.Tong_Tien_Thanh_Toan_Data);
            Input_Captcha.SendKeys("NV7989");
            Submit_Button.Click();

            Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(2);
        }

        public ThueResponse GetResponse()
        {
            string CssSelector_Response = "section.styles__SearchContentBox-sc-1ljhobs-0";
            IWebElement responseElement ;
            int count = 0;

            while (count < 5)
            {
                try
                {
                    responseElement = Driver.FindElement(By.CssSelector(CssSelector_Response));
                    ThueResponse response = new ThueResponse();
                    response.Message = responseElement.Text;
                    return response;
                }
                catch (Exception e)
                {
                    count++;
                    if (count == 5) throw new Exception("Không nhận được kết quả");
                }
            }
            return null;
        }

        public byte[] GetScreenShot()
        {
            ITakesScreenshot screenshotDriver = Driver as ITakesScreenshot;
            Screenshot screenshot = screenshotDriver.GetScreenshot();
            byte[] screenshotBytes = screenshot.AsByteArray;
            return screenshotBytes;
        }

        private string GetCaptcha()
        {
            string CssSelector_Captcha = "div.Captcha__ImageWrapper-sc-1up1k1e-0.img";
            int count = 0;
            string response;
            while (true)
            {
                try
                {
                    IWebElement img = Driver.FindElement(By.CssSelector(CssSelector_Captcha));
                    string base64string = img.GetAttribute("src");

                    response = base64string;
                    return response;
                }
                catch (Exception e)
                {
                    count++;
                    if (count == 5)
                    {
                        throw new Exception("Không thể lấy captcha ");
                    }
                }
            }
        }

        public void Dispose()
        {
            Driver.Quit();
            Driver = null;
            Options = null;
        }
    }
}
