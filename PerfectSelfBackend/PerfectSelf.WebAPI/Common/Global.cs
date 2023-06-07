using CsvHelper;
using CsvHelper.Configuration;
using PerfectSelf.WebAPI.Models;
using System.Collections;
using System.Globalization;
using System.Text;

namespace PerfectSelf.WebAPI.Common
{
    public class Global
    {
        public static Hashtable onlineAllUsers = new Hashtable();
        public static Hashtable countryMap = new Hashtable();
        public static Hashtable stateMap = new Hashtable();
        public static Hashtable cityMap = new Hashtable();
        public static Hashtable verifyCodeMap = new Hashtable();
        public static Queue<LogInfo> logQueue = new Queue<LogInfo>();
        public static ManualResetEventSlim _canExecute = new ManualResetEventSlim(true);
        public static object LockMe = new object();
        public static String GenToken()
        {
            string token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            return token;
        }

        public static void ReadAllAddress(String path)
        {
            String fileName = path + "countries.csv";
            var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Encoding = Encoding.UTF8, // Our file uses UTF-8 encoding.
                Delimiter = "," // The delimiter is a comma.
            };

            using (var fs = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (var textReader = new StreamReader(fs, Encoding.UTF8))
                using (var csv = new CsvReader(textReader, configuration))
                {
                    var countries = csv.GetRecords<Country>();

                    foreach (var country in countries)
                    {
                        countryMap[country.name] = country.iso2;
                    }
                }
            }

            using (var fs = File.Open(path + "states.csv", FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (var textReader = new StreamReader(fs, Encoding.UTF8))
                using (var csv = new CsvReader(textReader, configuration))
                {
                    var states = csv.GetRecords<State>();

                    foreach (var state in states)
                    {
                        if (stateMap[state.country_code] == null) stateMap[state.country_code] = new Hashtable();
                        ((Hashtable)stateMap[state.country_code])[state.name] = state.id;
                    }
                }
            }

            using (var fs = File.Open(path + "cities.csv", FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (var textReader = new StreamReader(fs, Encoding.UTF8))
                using (var csv = new CsvReader(textReader, configuration))
                {
                    var cities = csv.GetRecords<City>();

                    foreach (var city in cities)
                    {
                        if (cityMap[city.state_id.ToString()] == null) cityMap[city.state_id.ToString()] = new Hashtable();
                        ((Hashtable)cityMap[city.state_id.ToString()])[city.name] = city.id.ToString();
                    }
                }
            }
        }

        public static void LogMessageThread()
        {
            while (true)
            {
                _canExecute.Wait();
                while (Global.logQueue.Count > 0)
                {
                    var logMap = Global.logQueue.Dequeue();
                    try
                    {   
                        String logFile = Directory.GetCurrentDirectory() + $"\\Log\\{logMap.uid}.log";
                        String logContent = $"[{DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss")}] {logMap.log}\n";
                        System.IO.File.AppendAllText(logFile, logContent);
                    }
                    catch {
                        String logFile = Directory.GetCurrentDirectory() + $"\\Log\\{logMap.uid}.log";
                        String logContent = $"[{DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss")}] log error!\n";
                        System.IO.File.AppendAllText(logFile, logContent);
                    }
                }
                _canExecute.Reset();
            }
        }
    }
}
