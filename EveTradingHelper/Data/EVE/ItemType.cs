using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;

namespace EveTradingHelper.Data.EVE
{
    class ItemType
    {
        private static Dictionary<long, string> types;
        private static Dictionary<long, string> GetList()
        {
            if (types == null)
            {


                if (!Directory.Exists("typeID.csv"))
                {
                    try
                    {
                        (new WebClient()).DownloadFile("https://www.fuzzwork.co.uk/resources/typeids.csv", "typeID.csv");
                    }
                    catch
                    {
                        (new WebClient()).DownloadFile("https://raw.githubusercontent.com/Razesdark/EveTradeHelper/a476637fac8ab516c87e0661781f662051780eca/EveTradingHelper/bin/Debug/typeID.csv", "typeID.csv");
                    }
                }

                StreamReader sr = new StreamReader(File.Open("typeID.csv", FileMode.Open));

                types = new Dictionary<long, string>();
                while (sr.EndOfStream == false)
                {
                    string[] s = sr.ReadLine().Split(',');
                    types.Add(long.Parse(s[0].Trim('\"')), s[1].Trim('\"'));
                }
            }

            return types;
        }

        public static String GetById(long id)
        {
            return GetList()[id];
        }
        public static string[] GetAllNames()
        {
            return GetList().Values.ToArray<string>();
        }
    }
}
