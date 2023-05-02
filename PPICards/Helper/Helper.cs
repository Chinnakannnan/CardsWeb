using System.ComponentModel;

namespace PPICards.Helper
{
    public static class Helper
    {
        public static string ConvertCardNumber(this string cardnumber, int type)
        {
            string returnvalue = cardnumber;

            if (type == 2 && !string.IsNullOrEmpty(returnvalue) && returnvalue.Length == 16)
            {
                returnvalue = returnvalue.Substring(0, 4) + " " + returnvalue.Substring(5, 4) + " " + returnvalue.Substring(8, 4) + " " + returnvalue.Substring(12, 4);
            }
            else
            {
                returnvalue = "xxxx xxxx xxxx xxxx";

            }

            return returnvalue;
        }

        public static string ConvertExpiryDate(this string ExpiryDate, int type)
        {
            string returnvalue = ExpiryDate;

            if (type == 2 && !string.IsNullOrEmpty(returnvalue) && returnvalue.Length == 4)
            {
                returnvalue = returnvalue.Substring(0, 2) + "/" + returnvalue.Substring(2, 2);
            }
            else
            {
                returnvalue = "xx/xx";

            }

            return returnvalue;
        }

        public static string ConvertCVV(this string cvv, int type)
        {
            string returnvalue = cvv;

            if (type == 2 && !string.IsNullOrEmpty(returnvalue))
            {
                returnvalue = cvv;
            }
            else
            {
                returnvalue = "xxx";

            }

            return returnvalue;
        }

        public static bool IsCheckValidFromTodate(this string Fromdate, string Todate, out string errormessage, out DateTime fromdate, out DateTime todate)
        {
            bool retunvalue = true;
            errormessage = string.Empty;
            DateTime.TryParse(Fromdate, out fromdate);
            DateTime.TryParse(Todate, out todate);

            if (fromdate == DateTime.MinValue)
            {
                errormessage = "Invalid from date.<br>";
            }
            if (fromdate.Date > DateTime.Today || todate.Date > DateTime.Today)
            {
                errormessage = "Future date can't be allowed<br>";
            }
            if (todate == DateTime.MinValue)
            {
                errormessage = errormessage + "Invalid to date.<br>";
            }
            if (fromdate > todate && fromdate != DateTime.MinValue && todate != DateTime.MinValue)
            {
                errormessage = errormessage + "From date can't be extended to the to date.<br>";
            }
            if (((todate - fromdate).TotalDays > 365) && fromdate != DateTime.MinValue && todate != DateTime.MinValue)
            {
                errormessage = errormessage + "Maximum date range selection extended,select maximum 365 days<br>";
            }
            if (string.IsNullOrEmpty(errormessage))
            {
                retunvalue = false;
            }
            return retunvalue;

        }

        public static DateTime DateTimeConverter(this string value)
        {
            DateTime dateTime = DateTime.MinValue;
            int ts = 0;
            value = value.Length > 10 ? value.Substring(0, 10) : value;
            var validint = int.TryParse(value, out ts);
            // = value;//1451174400;
            dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(ts).ToLocalTime();
            return dateTime;
        }
    }
}
