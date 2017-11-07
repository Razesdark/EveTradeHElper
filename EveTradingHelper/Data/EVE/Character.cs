using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveTradingHelper.Data.EVE
{
    class Character
    {
        private static Dictionary<long, string> regions;
        private static Dictionary<long, string> Regions
        {
            get
            {
                if (regions == null)
                    regions = new Dictionary<long, string>();
                return regions;
            }
        }

        public static long Add(string id, string name)
        {
            return Add(long.Parse(id), name);
        }
        public static long Add(long id, string name)
        {
            if (!Regions.Keys.Contains(id))
                Regions.Add(id, name);
            return id;
        }
        public static string GetById(long id)
        {
            return Regions[id];
        }
    }
}
