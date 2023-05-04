//using Newtonsoft.Json.Linq;
using System.Data;
using System.Reflection;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
 


namespace MYPAY.Models
{
    public class utility
    {
        public static List<string> InvalidJsonElements;
        //public static IList<T> DeserializeToList<T>(string jsonString)
        //{
        //    InvalidJsonElements = null;
        //    var array = JArray.Parse(jsonString);
        //    IList<T> objectsList = new List<T>();

        //    foreach (var item in array)
        //    {
        //        try
        //        {
        //            // CorrectElements  
        //            objectsList.Add(item.ToObject<T>());
        //        }
        //        catch (Exception)
        //        {
        //            InvalidJsonElements = InvalidJsonElements ?? new List<string>();
        //            InvalidJsonElements.Add(item.ToString());
        //        }
        //    }

        //    return objectsList;
        //}

        public static string ErrorLog(string FolderName, string Excption)
       {
            string logDirectory = Path.Combine(Directory.GetCurrentDirectory(), "ErrorLogFolder\\"+FolderName);
            Directory.CreateDirectory(logDirectory);

            string logFilePath = Path.Combine(logDirectory, "errorlog.txt");
            using (StreamWriter writer = new StreamWriter(logFilePath, true))            {
                writer.WriteLine("Error Message: " + Excption);
               // writer.WriteLine("Stack Trace: " + ex.StackTrace);
                writer.WriteLine("Date/Time: " + DateTime.Now.ToString());
                writer.WriteLine();
            }
            return null;

        }
        public static DataTable JsonStringToDataTable(string jsonString)
        {
            DataTable dt = new DataTable();
            string[] jsonStringArray = Regex.Split(jsonString.Replace("[", "").Replace("]", ""), "},{");
            List<string> ColumnsName = new List<string>();
            foreach (string jSA in jsonStringArray)
            {
                string[] jsonStringData = Regex.Split(jSA.Replace("{", "").Replace("}", ""), ",");
                foreach (string ColumnsNameData in jsonStringData)
                {
                    try
                    {
                        int idx = ColumnsNameData.IndexOf(":");
                        string ColumnsNameString = ColumnsNameData.Substring(0, idx - 1).Replace("\"", "");
                        if (!ColumnsName.Contains(ColumnsNameString))
                        {
                            ColumnsName.Add(ColumnsNameString);
                        }
                    }
                    catch (Exception)
                    {
                        throw new Exception(string.Format("Error Parsing Column Name : {0}", ColumnsNameData));
                    }
                }
                break;
            }
            foreach (string AddColumnName in ColumnsName)
            {
                dt.Columns.Add(AddColumnName);
            }
            foreach (string jSA in jsonStringArray)
            {
                string[] RowData = Regex.Split(jSA.Replace("{", "").Replace("}", ""), ",");
                DataRow nr = dt.NewRow();
                foreach (string rowData in RowData)
                {
                    try
                    {
                        int idx = rowData.IndexOf(":");
                        string RowColumns = rowData.Substring(0, idx - 1).Replace("\"", "");
                        string RowDataString = rowData.Substring(idx + 1).Replace("\"", "");
                        nr[RowColumns] = RowDataString;
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
                dt.Rows.Add(nr);
            }
            return dt;
        }

        public static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }

        public static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName].ToString(), null);
                    else
                        continue;
                }
            }
            return obj;
        }

        public static string GetStan()
        {
            string lstrString = "";
            string lstrNumber = "";
            int lintCnt = 0;

            lstrString = Guid.NewGuid().ToString();
            for (lintCnt = 1; lintCnt <= lstrString.Length - 1; lintCnt++)
            {
                if (IsNumeric(lstrString.Substring(lintCnt, 1)) == true)
                {
                    lstrNumber = lstrNumber + lstrString.Substring(lintCnt, 1);
                    if (lstrNumber.Length > 5)
                    {
                        break;
                    }
                }
            }
            return lstrNumber.PadRight(6, '0').ToString();
        }
        public static string GenPassword()
        {
            int length = 12;
            const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()_-+=[{]};:,<.>/?";
            var rng = new RNGCryptoServiceProvider();
            var bytes = new byte[length];
            rng.GetBytes(bytes);
            var chars = new char[length];
            var count = validChars.Length;

            for (var i = 0; i < length; i++)
            {
                chars[i] = validChars[bytes[i] % count];
            }

            return new string(chars);
        }
        public static string GetAESKEY()
        {
            string lstrString = "";
            string lstrNumber = "";
            int lintCnt = 0;

            lstrString = Guid.NewGuid().ToString().Replace("-", "");
            for (lintCnt = 1; lintCnt <= lstrString.Length - 1; lintCnt++)
            {
                lstrNumber = lstrNumber + lstrString.Substring(lintCnt, 1);
                if (lstrNumber.Length > 15)
                {
                    break;
                }
            }
            return lstrNumber.PadRight(16, '0').ToString();
        }

        public static string GetConsumerSecret()
        {
            string lstrString = "";
            lstrString = Guid.NewGuid().ToString().Replace("-", "");
            return lstrString;
        }
        public static string GetAlphaChar()
        {
            string lstrString = "";
            string lstrNumber = "";
            int lintCnt = 0;

            lstrString = Guid.NewGuid().ToString().Replace("-", "");
            for (lintCnt = 1; lintCnt <= lstrString.Length - 1; lintCnt++)
            {
                if (IsNumeric(lstrString.Substring(lintCnt, 1)) != true)
                {
                    lstrNumber = lstrNumber + lstrString.Substring(lintCnt, 1);
                    if (lstrNumber.Length > 5)
                    {
                        break;
                    }
                }
            }
            return lstrNumber.PadRight(6, 's').ToString();
        }

        public static bool IsNumeric(string input)
        {
            int test;
            return int.TryParse(input, out test);
        }

        public static string ConvertToJulian()
        {
            DateTime firstJan = new DateTime(DateTime.Now.Year, 1, 1);
            string daysSinceFirstJan = Convert.ToString((DateTime.Now - firstJan).Days + 1);
            return DateTime.Now.Year.ToString().Substring(3, 1) + daysSinceFirstJan.PadLeft(3, '0') + DateTime.Now.ToString("HH");
        }

        public static string GetTransID()
        {
            return ConvertToJulian() + GetStan() + DateTime.Now.ToString("ssffff");
        }

        public static string GetCurrentMilliSec()
        {
            return DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();
        }

        public static byte[] HashHMAC256(string message, string key)
        {
            var hash = new HMACSHA256(System.Text.Encoding.UTF8.GetBytes(key));
            return hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(message));
        }

        public static byte[] HashHMAC512(string message, string key)
        {
            var hash = new HMACSHA512(System.Text.Encoding.UTF8.GetBytes(key));
            return hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(message));
        }

      


    }
}