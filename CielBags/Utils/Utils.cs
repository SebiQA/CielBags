using AventStack.ExtentReports;
using Microsoft.VisualBasic.FileIO;
using NPOI.XSSF.UserModel;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace CielBags
{
    public class Utils
    {

        public static IWebElement WaitForElement(IWebDriver driver, int seconds, By locator)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));
            return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator));
        }

        public static IWebElement WaitForElementClickable(IWebDriver driver, int seconds, By locator)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));
            return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(locator));
        }

        public static IWebElement WaitForFluentElement(IWebDriver driver, int seconds, By locator)
        {
            DefaultWait<IWebDriver> fluentWait = new DefaultWait<IWebDriver>(driver)
            {
                Timeout = TimeSpan.FromSeconds(seconds),
                PollingInterval = TimeSpan.FromMilliseconds(100),
                Message = "Sorry, the element in the page cannot be found"
            };
            fluentWait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            return fluentWait.Until(x => x.FindElement(locator));
        }
        public static void PrintCookies(ICookieJar cookies)
        {

            foreach (Cookie c in cookies.AllCookies)
            {
                Console.WriteLine("Cookie name {0} - cookie value {1}", c.Name, c.Value);
            }
        }

        /// <summary>
        /// The method creates screenshot based on the current date and saves it into a folder defined by the tester
        /// </summary>
        /// <param name="driver">The WebDriver instance/ browser from which the screenshot will be taken</param>
        /// <param name="path">The path where the screenshot will be saved</param>
        /// <param name="fileName">The base file name that will have appended the date to have unique files</param>
        /// <param name="format">specify the image file format, example: jpg</param>
        public static void TakeScreenshotWithDate(IWebDriver driver, string path, string fileName, ScreenshotImageFormat format)
        {
            DirectoryInfo validation = new DirectoryInfo(path);
            if (!validation.Exists)
            {
                validation.Create();
            }
            string currentDate = DateTime.Now.ToString();
            StringBuilder sb = new StringBuilder(currentDate);
            sb.Replace(":", "_");
            sb.Replace(".", "_");
            sb.Replace(" ", "_");
            string finalFilePath = String.Format("{0}\\{1}_{2}.{3}", path, fileName, sb.ToString(), format);
            ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile(finalFilePath, format);
        }

        public static void ExecuteJsScript(IWebDriver driver, string script)
        {
            var jsExecutor = (IJavaScriptExecutor)driver;
            var result = jsExecutor.ExecuteScript(script);
            if (result != null)
            {
                Console.WriteLine(result.ToString());
            }

        }
        /// <summary>
        /// Converts a config file that has lines like key=value into a Dictionary with key and value
        /// </summary>
        /// <param name="configFilePath">the path of the config file</param>
        /// <returns> A dictionary with a key value pair of type string and string representing the lines in the config file</returns>
        public static Dictionary<string, string> ReadConfig(string configFilePath) 
        { 
            var configData = new Dictionary<string, string>(); 
            foreach (var line in File.ReadAllLines(configFilePath)) 
            { 
                string[] values = line.Split('='); configData.Add(values[0].Trim(), values[1].Trim()); 
            } 
            return configData; 
        }

        public static string[][] GetGenericData(string path)
        {
            var lines = File.ReadAllLines(path).Select(a => a.Split(',')).Skip(1);
            return lines.ToArray();
        }

        public static DataTable GetDataTableFromCsv(string csv)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using(TextFieldParser csvReader = new TextFieldParser(csv))
                {
                    csvReader.SetDelimiters(new string[] { "," });
                    csvReader.HasFieldsEnclosedInQuotes = true;
                    string[] columnNames = csvReader.ReadFields();
                    foreach(string column in columnNames)
                    {
                        DataColumn dataColumn = new DataColumn();
                        dataColumn.AllowDBNull = true;
                        dataTable.Columns.Add(dataColumn);
                    }
                    while (!csvReader.EndOfData)
                    {
                        string[] rowValues = csvReader.ReadFields();

                        //this for loop can be skipped. It is used for sanitisation puposes
                        for (int i=0; i<rowValues.Length; i++)
                        {
                            if(rowValues[i] == "")
                            {
                                rowValues[i] = null;
                            }
                        }

                        dataTable.Rows.Add(rowValues);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(String.Format("Could not read from csv file {0}", csv));
            }
            return dataTable;
        }

        public static DataTable GetDataTableFromExcel(string excelPath)
        {
            DataTable dataTable = new DataTable();
            XSSFWorkbook wb;
            XSSFSheet sh;
            string sheetName;
            using (var fs = new FileStream(excelPath, FileMode.Open, FileAccess.Read))
            {
                wb = new XSSFWorkbook(fs);
                sheetName = wb.GetSheetAt(0).SheetName;
            }

            dataTable.Columns.Clear();
            dataTable.Rows.Clear();

            sh = (XSSFSheet)wb.GetSheet(sheetName);
            int i = 0;
            while (sh.GetRow(i) != null)
            {
                if (dataTable.Columns.Count < sh.GetRow(i).Cells.Count)
                {
                    for (int j = 0; j < sh.GetRow(i).Cells.Count; j++)
                    {
                        dataTable.Columns.Add("", typeof(string));
                    }
                }
                dataTable.Rows.Add();
                for (int j = 0; j < sh.GetRow(i).Cells.Count; j++)
                {
                    var cell = sh.GetRow(i).GetCell(j);
                    if (cell != null)
                    {
                        switch (cell.CellType)
                        {
                            case NPOI.SS.UserModel.CellType.Numeric:
                                {
                                    dataTable.Rows[i][j] = sh.GetRow(i).GetCell(j).NumericCellValue;
                                    break;
                                }
                            case NPOI.SS.UserModel.CellType.String:
                                {
                                    dataTable.Rows[i][j] = sh.GetRow(i).GetCell(j).StringCellValue;
                                    break;
                                }
                            default:
                                {
                                    dataTable.Rows[i][j] = "";
                                    break;
                                }
                        }
                    }
                }


            }
            return dataTable;
        }

        public static T JsonRead<T>(string jsonFile)
        {
            string text = File.ReadAllText(jsonFile);
            return JsonSerializer.Deserialize<T>(text);
        }

        public static List<string> GetAllFilesInFolderExt(string path, string extension)
        {
            List<string> files = new List<string>();
            DirectoryInfo di = new DirectoryInfo(path);
            foreach (FileInfo fi in di.GetFiles(extension, System.IO.SearchOption.TopDirectoryOnly))
            {
                files.Add(fi.FullName);
            }
            return files;
        }


        public static string Encrypt(string source, string key)
        {
            using (TripleDESCryptoServiceProvider tripleDESCryptoService = new TripleDESCryptoServiceProvider())
            {
                using (MD5CryptoServiceProvider hashMD5Provider = new MD5CryptoServiceProvider())
                {
                    byte[] byteHash = hashMD5Provider.ComputeHash(Encoding.UTF8.GetBytes(key));
                    tripleDESCryptoService.Key = byteHash;
                    tripleDESCryptoService.Mode = CipherMode.ECB;
                    byte[] data = Encoding.UTF8.GetBytes(source);
                    return Convert.ToBase64String(tripleDESCryptoService.CreateEncryptor().TransformFinalBlock(data, 0, data.Length));
                }
            }
        }

        public static string Decrypt(string encrypt, string key)
        {
            using (TripleDESCryptoServiceProvider tripleDESCryptoService = new TripleDESCryptoServiceProvider())
            {
                using (MD5CryptoServiceProvider hashMD5Provider = new MD5CryptoServiceProvider())
                {
                    byte[] byteHash = hashMD5Provider.ComputeHash(Encoding.UTF8.GetBytes(key));
                    tripleDESCryptoService.Key = byteHash;
                    tripleDESCryptoService.Mode = CipherMode.ECB;
                    byte[] data = Convert.FromBase64String(encrypt);
                    return Encoding.UTF8.GetString(tripleDESCryptoService.CreateDecryptor().TransformFinalBlock(data, 0, data.Length));
                }
            }
        }

        //metoda generare screenshot
        public static MediaEntityModelProvider CaptureScreenShot(IWebDriver driver, string name)
        {
            var screenShot = ((ITakesScreenshot)driver).GetScreenshot().AsBase64EncodedString;
            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenShot, name).Build();
        }

        public static string ConvertDictionaryToQuery(Dictionary<string, string> queryParams)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string key in queryParams.Keys)
            {
                sb.Append(String.Format("&{0}={1}", key, queryParams[key]));
            }
            return sb.ToString();

        }

        public static List<Dictionary<string, string>> ConvertCsvToDictionary(string filePath)
        {
            var lines = File.ReadAllLines(filePath).Select(a => a.Split(','));
            List<Dictionary<string, string>> dictionaryList = new List<Dictionary<string, string>>();
            string[] header = lines.ElementAt(0).ToArray();
            for (int i = 1; i < lines.Count(); i++)
            {
                var currentValues = lines.ElementAt(i).ToArray();
                Dictionary<string, string> queryParams = new Dictionary<string, string>();
                for (int j = 0; j < currentValues.Count(); j++)
                {
                    queryParams.Add(header[j], currentValues[j]);
                }
                dictionaryList.Add(queryParams);
            }
            return dictionaryList;
        }

        public static string GenerateRandomStringCount(int count)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz1234567890";
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < count; i++)
            {
                Random r = new Random();
                sb.Append(chars[r.Next(chars.Length)]);
            }
            return sb.ToString();
        }




    }

}
