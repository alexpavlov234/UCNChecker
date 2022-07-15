namespace UCNChecker
{
    internal class Program
    {
        //Статично поле, което представлява Dictionary, съдържащ кодовете на регионите.
        static Dictionary<string, string> RegionsDictionary = ReadFromFile(AppDomain.CurrentDomain.BaseDirectory + "/Regions.txt");
        static void Main(string[] args)
        {
            Console.WriteLine("------------------------- ЕГН-та с невалиден формат -------------------------");
            UCNCheck("008258732");
            UCNCheck("0а12294004");
            Console.WriteLine("\n------------------------- ЕГН-та с невалидна дата на раждане -------------------------");
            UCNCheck("0198054703");
            UCNCheck("0500106961");
            UCNCheck("0102309521");
            UCNCheck("0001326269");
            Console.WriteLine("\n------------------------- ЕГН-та с невалидна контрлна цифра -------------------------");
            UCNCheck("1401120473");
            UCNCheck("2611250074");
            UCNCheck("3005117845");
            UCNCheck("4506064169");
            Console.WriteLine("\n------------------------- Валидни ЕГН-та -------------------------");
            UCNCheck("5303196540");
            UCNCheck("6208104252");
            UCNCheck("8701086755");
            UCNCheck("8810092292");
            UCNCheck("8831187000");
            UCNCheck("1941153292");
            UCNCheck("1642290508");
        }
        //Метод за проверка на ЕГН.
        static bool UCNCheck(string UCN)
        {
            //Try-catch в случай че възникне непредвидена грешка.
            try
            {
                //Проверка на формата на ЕГН, за да се установи дали е с правилната дължина и дали съдържа букви. 
                if (UCN.Length != 10 || !IsDigitsOnly(UCN))
                {
                    Console.WriteLine("============== ЕГН: " + UCN + " ==============" + "\nГрешка! Невалиден формат!");
                    return false;
                }
                else
                {
                    //Извличане на цялата дата като низ и преобразуване на стойността на месеца в число. 
                    string wholeDate = UCN.Substring(0, 6);
                    int monthDate = Int32.Parse(UCN.Substring(2, 2));
                    //Установява се, с колко е увеличена стойността на месеца в ЕГН-то и се прави проверка дали датата е валидна.
                    //Ако това условие е вярно, месеца не е увеличен, т.е. лицето е родено през 1900 г.
                    if (monthDate <= 12 && IsValidDate(WholeDateNormalizer(wholeDate, 0)))
                    {
                        //Проверка на контролната цифра, ако тя е валидна, програмата отпечатва информация за ЕГН-то и методът връща стойност true.
                        if (LastNumChecker(UCN))
                        {
                            PrintDataAboutUCN(UCN, 0);
                            return true;
                        }
                        //В случай на невалидна контролна цифра методът връща стойност false и се извежда съобщение.
                        else
                        {
                            Console.WriteLine("============== ЕГН: " + UCN + " ==============" + "\nНевалидно ЕГН!");
                            return false;
                        }
                    }
                    //Ако това условие е вярно, месеца не е увеличен, т.е. лицето е родено през 1800 г.
                    else if (monthDate > 12 && monthDate <= 32 && IsValidDate(WholeDateNormalizer(wholeDate, 20)))
                    {
                        //Проверка на контролната цифра, ако тя е валидна, програмата отпечатва информация за ЕГН-то и методът връща стойност true.
                        if (LastNumChecker(UCN))
                        {
                            PrintDataAboutUCN(UCN, 20);
                            return true;
                        }
                        else
                        //В случай на невалидна контролна цифра методът връща стойност false и се извежда съобщение.
                        {
                            Console.WriteLine("============== ЕГН: " + UCN + " ==============" + "\nНевалидно ЕГН!");
                            return false;
                        }
                    }
                    //Ако това условие е вярно, месеца не е увеличен, т.е. лицето е родено през 2000 г.
                    else if (monthDate > 40 && monthDate <= 52 && IsValidDate(WholeDateNormalizer(wholeDate, 40)))
                    {
                        //Проверка на контролната цифра, ако тя е валидна, програмата отпечатва информация за ЕГН-то и методът връща стойност true.
                        if (LastNumChecker(UCN))
                        {
                            PrintDataAboutUCN(UCN, 40);
                            return true;
                        }
                        //В случай на невалидна контролна цифра методът връща стойност false и се извежда съобщение.
                        else
                        {
                            Console.WriteLine("============== ЕГН: " + UCN + " ==============" + "\nНевалидно ЕГН!");
                            return false;
                        }
                    }
                    //В случай на невалидна дата на раждане методът връща стойност false и се извежда съобщение.
                    else
                    {
                        Console.WriteLine("============== ЕГН: " + UCN + " ==============" + "\nГрешка! Невалидна дата на раждане!");
                        return false;
                    }
                }
            }
            //В случай на непредвидена грешка методът връща стойност false и се извежда съобщение.
            catch
            {
                Console.WriteLine("Грешка!");
                return false;
            }
        }
        //Метод за проверка дали ЕГН-то съдържа само цифри, чрез for-each, като всеки символ се проверява дали има стойност, която съответства на цифра.
        static bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }
        //Метод за проверка на датата в ЕГН-то, като се използва структурата DateTime. 
        static bool IsValidDate(string str)
        {
            //Чрез метода TryParseExact(), подаден низ се проверява дали е валидна дата.
            //Методът изисква да се подаде още и форматът на датата,  обект, който предоставя информация за форматирането на нашия низ (в нашия случай null),
            //стил на датата и часа (в нашия случай - координирано универсално време (UTC)) и променлива която да бъде форматирана дата (в нашия случай не я използваме).
            if (DateTime.TryParseExact(str, "yyyyMMdd", null, System.Globalization.DateTimeStyles.AdjustToUniversal, out DateTime result))
                return true;
            else
                return false;
        }
        //Метод за форматиране на датата на ЕГН-то, като се използва структурата DateTime. Методът връща низ, който се използва в метода за печат.
        static string FormatDate(string str)
        {
            //Чрез метода TryParseExact(), подаден низ се проверява дали е валидна дата.
            //Методът изисква да се подаде още и форматът на датата,  обект, който предоставя информация за форматирането на нашия низ (в нашия случай null),
            //стил на датата и часа (в нашия случай - координирано универсално време (UTC)) и променлива която да бъде форматирана дата (в нашия случай връщаме този резултат).
            if (DateTime.TryParseExact(str, "yyyyMMdd", null, System.Globalization.DateTimeStyles.AdjustToUniversal, out DateTime result))
                return result.ToString("dd-MM-yyyy");
            else
                return null;
        }
        //Метод за форматиране на датата на ЕГН-то, методът премахва добавената стойност в месеците на ЕНГ-то.
        //Единият параметър е самата дата, а другият е числото, с което датата е увеличена. 
        static string WholeDateNormalizer(string date, int number)
        {
            //В зависимост от числото датата се раздробява и се конкатенира отново в правилен формат. 
            //Методът Format() форматира и месеците, които имат една цифра - X в формат - 0X (Пример 1 -> 01).
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
        //Метод за четене на Dictionary от файл. Методът, чете файла ред по ред, като използва за разделител - "," и премахва начални и крайни интервали.
        static Dictionary<string, string> ReadFromFile(string file)
        {
            return File.ReadLines(file).ToDictionary(x => x.Split(',')[0].Trim(), x => x.Split(',')[1].Trim());
        }
        //Метод за проверка на контролната цифра.
        static bool LastNumChecker(string UCN)
        {
            //Променлива за сума на произведенията от всички цифри.
            int sum = 0;
            //Променлива за съхраняване на резултата от целочислено деление. 
            int integerDivision;
            //Променлива съхраняваща контролната цифра. 
            int lastNumberOfUCN;

            //Преобразуваме низа на ЕГН-то в масив от символи. 
            char[] numbersCharArray = UCN.ToCharArray();
            //Преобразуваме символния низ в числов.
            int[] numbersIntegerArray = Array.ConvertAll(numbersCharArray, c => (int)Char.GetNumericValue(c));
            //Масив, съхраняващ теглото на съответното число в ЕГН-то. 
            int[] numbersWeight = { 2, 4, 8, 5, 10, 9, 7, 3, 6 };

            //Чрез for-цикъл умножаваме всяка цифра от ЕГН-то със съответното ѝ тегло и добавяме произведението към сума.
            for (int i = 0; i < numbersWeight.Length; i++)
            {
                sum += numbersIntegerArray[i] * numbersWeight[i];
            }
            //Делим целочислено сумата на 11 и запазваме резултатa.
            integerDivision = sum / 11;
            //Намиране на контролната цифра на ЕГН-то.
            lastNumberOfUCN = sum - 11 * integerDivision;
            //Ако контролната цифра е по-малка от 10, то тя си остава същата и се проверява дали съответства на тази в ЕГН-то. 
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
            //Ако контролната цифра по-голяма или равна на 10, то тя става равна на 0 и се проверява дали съответства на тази в ЕГН-то. 
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
        //Метод за разпечатка на данни за ЕГН. 
        static void PrintDataAboutUCN(string UCN, int number)
        {
            Console.WriteLine("============== ЕГН: " + UCN + " ==============");
            Console.WriteLine("Дата на раждане: " + FormatDate(WholeDateNormalizer(UCN.Substring(0, 6), number)));
            Console.WriteLine("Регион: " + RegionsDictionary[UCN.Substring(6, 3)]);
            Console.WriteLine("Пол: " + (Int32.Parse(UCN.Substring(UCN.Length - 2, 1)) % 2 == 0 ? "мъжки" : "женски"));

        }

    }

}