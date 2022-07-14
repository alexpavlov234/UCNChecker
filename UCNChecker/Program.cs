namespace UCNChecker
{
    internal class Program
    {
        static Dictionary<string, string> Regions = ReadFromFile(AppDomain.CurrentDomain.BaseDirectory + "/Regions.txt");
        static void Main(string[] args)
        {

        }
        static bool UcnCheck(string UCN)
        {
            try
            {
                if (UCN.Length != 10 || !IsDigitsOnly(UCN))
                {
                    return false;
                }
                else
                {
                    string wholeDate = UCN.Substring(0, 6);
                    int monthDate = Int32.Parse(UCN.Substring(2, 2));



                    if (monthDate <= 12 && IsValidDate(WholeDateNormalizer(wholeDate, 0)))
                    {

                        if (CheckCityRegion(UCN))
                        {

                        }
                        else
                        {
                            return false;
                        }
                        return true;
                    }
                    else if (monthDate > 12 && monthDate <= 32 && IsValidDate(WholeDateNormalizer(wholeDate, 20)))
                    {

                        if (CheckCityRegion(UCN))
                        {

                        }
                        else
                        {
                            return false;
                        }
                        return true;
                    }
                    else if (monthDate > 40 && monthDate <= 52 && IsValidDate(WholeDateNormalizer(wholeDate, 40)))
                    {

                        if (CheckCityRegion(UCN))
                        {

                        }
                        else
                        {
                            return false;
                        }
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


            if (DateTime.TryParseExact(str, "yyyyMMdd", null, System.Globalization.DateTimeStyles.AllowWhiteSpaces | System.Globalization.DateTimeStyles.AdjustToUniversal, out DateTime result))
                return true;
            else
                return false;


        }

        static string WholeDateNormalizer(string date, int number)
        {
            if (number == 0)
            {
                date = "19" + date.Substring(0, 2) + string.Format("{0:00}", Int32.Parse(date.Substring(2, 2)) - number) + date.Substring(2 + 2);
            }
            else if (number == 20)
            {
                date = "18" + date.Substring(0, 2) + string.Format("{0:00}", Int32.Parse(date.Substring(2, 2)) - number) + date.Substring(2 + 2);
            }
            else if (number == 40)
            {
                date = "20" + date.Substring(0, 2) + string.Format("{0:00}", Int32.Parse(date.Substring(2, 2)) - number) + date.Substring(2 + 2);
            }



            return date;
        }

        static Dictionary<string, string> ReadFromFile(string file)
        {
            return File.ReadLines(file).ToDictionary(x => x.Split(',')[0].Trim(), x => x.Split(',')[1].Trim());
        }

        static bool CheckCityRegion(string UCN)
        {
            return Regions.ContainsKey(UCN.Substring(6, 3));
        }

    }
}