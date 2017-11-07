using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EveTradingHelper.Data
{
    public class Order
    {

        /* orderID,typeID,charID,charName,
         * regionID,regionName,
         * stationID,stationName,range,bid,price,volEntered,volRemaining,issueDate,orderState,minVolume,accountID,duration,isCorp,solarSystemID,solarSystemName,escrow,keyID
         * 
         */

        SortedDictionary<DateTime, string[]> entries;

        private bool filledIn = false;
        public long ID { get; }
        public long TypeID { get; }
        public string Type {  get { return EVE.ItemType.GetById(this.TypeID); } }
        public long CharID { get; }
        public string Character { get { return EVE.Character.GetById(this.CharID); } }
        public long RegionID { get; }
        public string Region { get { return EVE.Region.GetById(this.RegionID); } }
        public long StationID { get; }
        public string Station {  get { return EVE.Station.GetById(this.StationID); } }

        public double Price { get; }
        public int InitialOrderSize { get; }
        public bool IsBuyOrder { get; }
        public bool IsSellOrder { get { return !this.IsBuyOrder; } }

        public Order(string[] probe)
        {
            
            this.entries = new SortedDictionary<DateTime, string[]>();

            if(this.filledIn)
            {
                return;
            }

            this.ID = long.Parse(probe[0]);
            this.TypeID = long.Parse(probe[1]);
            this.CharID = EVE.Character.Add(probe[2], probe[3]);
            this.RegionID = EVE.Region.Add(probe[4], probe[5]);
            this.StationID = EVE.Station.Add(probe[6], probe[7]);

            this.Price = double.Parse(probe[10].Replace(".", ","));
            this.InitialOrderSize = int.Parse(probe[11]);
            this.IsBuyOrder = probe[9].Equals("True");


            
            this.filledIn = true;

            var x = this.Type;

        }

        public int OrderSizeAt()
        {
            return this.OrderSizeAt(this.LastSeen);
        }
        public int OrderSizeAt(DateTime time)
        {
            if (time < this.FirstSeen)
                return 0;
            DateTime best = BestTimeAt(time);
            return best <= this.LastSeen ? (int)double.Parse(this.entries[best][2].Replace(".", ",")) : 0;
        }

        public void AddProbe(DateTime when, string[] probe)
        {
           

            List<string> tmp = new List<string>();
            tmp.Add(probe[8]); // range
            tmp.Add(probe[10]); // price
            tmp.Add(probe[12]); // Item remaining
            this.entries.Add(when, tmp.ToArray());
        }
        public DateTime FirstSeen
        {
            get { return this.entries.Keys.First(); }
        }
        public DateTime LastSeen
        {
            get { return this.entries.Keys.Last(); }
        }
        public bool IsActive
        {
            get { return this.LastSeen == LastTime(); }
        }
        public bool WasActive(DateTime at)
        {
            return at < this.LastSeen;
        }
        public ListViewItem[] EntityViewItems()
        {
            return this.entries.Select(o => new ListViewItem(new[] { o.Key.ToString(), o.Value[2], o.Value[1] })).ToArray();
        }



        /**
         * 
         * 
         * 
         *  Static methods
         * 
         * 
         * 
         */ 
        private static Dictionary<long, Order> orders;
        public static Dictionary<long, Order> Orders
        {
            get
            {
                if (orders == null)
                    orders = new Dictionary<long, Order>();
                return orders;
            }
        }
        private static List<DateTime> checkedTimes;
        public static bool checkedTimeNeedsShuffle = true;
        private static List<DateTime> CheckedTimes
        {
            get
            {
                if (checkedTimes == null)
                    checkedTimes = new List<DateTime>();
                return checkedTimes;
            }
        }
        private static void sortList()
        {
            if(checkedTimeNeedsShuffle)
            {
                checkedTimeNeedsShuffle = false;
                checkedTimes.Sort();
            }
        }
        public static void AddTime(DateTime d)
        {
            var x = CheckedTimes;
            if (!x.Contains(d))
                x.Add(d);
            checkedTimeNeedsShuffle = true;
            checkedTimes = x;

        }
        public static List<DateTime> DateTimes
        {
            get
            {
                return CheckedTimes;
            }
        }
        public static DateTime FirstTime()
        {
            sortList();
            return checkedTimes.First();
        }
        public static DateTime LastTime()
        {
            sortList();
            return checkedTimes.Last();
        }
        public static DateTime BestTimeAt(DateTime time)
        {
            sortList();
            for(int i = 0; i < checkedTimes.Count-1; i++)
            {
                if (checkedTimes[i] <= time && checkedTimes[i + 1] >= time)
                    return checkedTimes[i];
            }

            return checkedTimes.Last();
        }
    }
}
