namespace UCNChecker
{
    internal class Program
    {
        static Dictionary<string, string> _dictionary = ReadFromFile(AppDomain.CurrentDomain.BaseDirectory + "/Cities.txt");
        static void Main(string[] args)
        {


            UcnCheck("9931136319");
        }
        static bool UcnCheck(string UCN)
        {
            try
            {
                if (UCN.Length != 10 || !IsDigitsOnly(UCN))
                {
                    //Console.WriteLine("dd");
                    return false;
                }
                else
                {
                    string wholeDate = UCN.Substring(0, 6);
                    int monthDate = Int32.Parse(UCN.Substring(2, 2));
                   

                    if (monthDate <= 12 && IsValidDate(wholeDate))
                    {

                        return true;
                    }
                    else if (monthDate > 12 && monthDate <= 32 && IsValidDate(WholeDateNormalizer(wholeDate, 20)))
                    {

                        return true;
                    }
                    else if (monthDate > 40 && monthDate <= 52 && IsValidDate(WholeDateNormalizer(wholeDate, 40)))
                    {

                        return true;
                    }
                    else
                    {

                        return false;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        static bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }
        static bool IsValidDate(string str)
        {
            if (DateTime.TryParseExact(str, "yyMMdd", null, System.Globalization.DateTimeStyles.AllowWhiteSpaces |
                               System.Globalization.DateTimeStyles.AdjustToUniversal, out DateTime result))
                return false;
            else
                return true;

        }

        static string WholeDateNormalizer(string date, int number)
        {
            date = date.Substring(0, 2) + string.Format("{0:00}", Int32.Parse(date.Substring(2, 2)) - number) + date.Substring(2 + 2);
            return date;
        }

        static Dictionary<string, string> ReadFromFile(string file)
        {
            return File.ReadLines(file).ToDictionary(x => x.Split(',')[0].Trim(), x => x.Split(',')[1].Trim());
        }
        //       public bool IsValidDate(int year, int month, int day)
        //{
        //    return year >= 1 && year <= 9999
        //            && month >= 1 && month <= 12
        //            && day >= 1 && day <= DateTime.DaysInMonth(year, month);
        //}
    }
}