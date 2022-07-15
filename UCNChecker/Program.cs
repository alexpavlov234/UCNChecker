namespace UCNChecker
{
    internal class Program
    {
        static Dictionary<string, string> RegionsDictionary = ReadFromFile(AppDomain.CurrentDomain.BaseDirectory + "/Regions.txt");
        static void Main(string[] args)
        {
                    
        }
        static bool UCNCheck(string UCN)
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
                            return LastNumChecker(UCN);
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
                            return LastNumChecker(UCN);
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
                                return LastNumChecker(UCN);
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
        static string FormatDate(string str)
        {
            if (DateTime.TryParseExact(str, "yyyyMMdd", null, System.Globalization.DateTimeStyles.AllowWhiteSpaces | System.Globalization.DateTimeStyles.AdjustToUniversal, out DateTime result))
                return result.ToString("dd-MM-yyyy");
            else
                return null;
        }

        static string WholeDateNormalizer(string date, int number)
        {
            if (number == 0)
            {
                date = "19" + date.Substring(0, 2) + string.Format("{0:00}", Int32.Parse(date.Substring(2, 2)) - number) + date.Substring(4);
            }
            else if (number == 20)
            {
                date = "18" + date.Substring(0, 2) + string.Format("{0:00}", Int32.Parse(date.Substring(2, 2)) - number) + date.Substring(4);
            }
            else if (number == 40)
            {
                date = "20" + date.Substring(0, 2) + string.Format("{0:00}", Int32.Parse(date.Substring(2, 2)) - number) + date.Substring(4);
            }



            return date;
        }

        static Dictionary<string, string> ReadFromFile(string file)
        {
            return File.ReadLines(file).ToDictionary(x => x.Split(',')[0].Trim(), x => x.Split(',')[1].Trim());
        }

        static bool CheckCityRegion(string UCN)
        {
            return RegionsDictionary.ContainsKey(UCN.Substring(6, 3));
        }

        static bool LastNumChecker(string UCN)
        {
            int sum = 0;
            int divisionWithRemainder;
            int lastNumberOfUCN;

            char[] numbersCharArray = UCN.ToCharArray();

            int[] numbersIntegerArray = Array.ConvertAll(numbersCharArray, c => (int)Char.GetNumericValue(c));

            int[] numbersWeight = { 2, 4, 8, 5, 10, 9, 7, 3, 6 };


            for (int i = 0; i < numbersWeight.Length; i++)
            {
                sum += numbersIntegerArray[i] * numbersWeight[i];
            }

            divisionWithRemainder = sum / 11;

            lastNumberOfUCN = sum - 11 * divisionWithRemainder;

            if (lastNumberOfUCN < 10)
            {

                if (UCN.Substring(UCN.Length - 1).Equals(lastNumberOfUCN.ToString()))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                lastNumberOfUCN = 0;
                if (UCN.Substring(UCN.Length - 1).Equals(lastNumberOfUCN.ToString()))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

    }

}