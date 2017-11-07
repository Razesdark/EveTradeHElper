using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveTradingHelper.Data.EVE
{
    class Station
    {
        private static Dictionary<long, string> characters;
        private static Dictionary<long, string> Characters
        {
            get
            {
                if (characters == null)
                    characters = new Dictionary<long, string>();
                return characters;
            }
        }

        public static long Add(string id, string name)
        {
            return Add(long.Parse(id), name);
        }
        public static long Add(long id, string name)
        {
            if (!Characters.Keys.Contains(id))
                Characters.Add(id, name);
            return id;
        }
        public static string GetById(long id)
        {
            return characters[id];
        }
    }
}
