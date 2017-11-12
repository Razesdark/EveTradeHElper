using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace EveTradingHelper.Data
{
    
    /// <summary>Represents a Order in Eve Online.</summary>
    public class Order
    {

        /* orderID,typeID,charID,charName,
         * regionID,regionName,
         * stationID,stationName,range,bid,price,volEntered,volRemaining,issueDate,orderState,minVolume,accountID,duration,isCorp,solarSystemID,solarSystemName,escrow,keyID
         * 
         */

        SortedDictionary<DateTime, string[]> entries;
        private bool filledIn = false;

        /// <summary>
        ///  orderID
        /// </summary>
        public long ID { get; }

        /// <summary>
        /// typeID
        /// </summary>
        public long TypeID { get; }

        /// <summary>
        /// typeName
        /// </summary>
        public string Type {  get { return EVE.ItemType.GetById(this.TypeID); } }

        /// <summary>
        /// CharacterID
        /// </summary>
        public long CharID { get; }

        /// <summary>
        /// Character Name
        /// </summary>
        public string Character { get { return EVE.Character.GetById(this.CharID); } }

        /// <summary>
        /// RegionID
        /// </summary>
        public long RegionID { get; }

        /// <summary>
        /// RegionName
        /// </summary>
        public string Region { get { return EVE.Region.GetById(this.RegionID); } }

        /// <summary>
        /// StationID
        /// </summary>
        public long StationID { get; }

        /// <summary>
        /// Station Name
        /// </summary>
        public string Station {  get { return EVE.Station.GetById(this.StationID); } }

        /// <summary>
        /// Latest price, returns 0 if no price is found
        /// </summary>
        public double Price {
            get {
                return this.entries.Count == 0 ? 0.0 : double.Parse(this.entries[this.LastSeen][1].Replace(".", ","));
            }
        }

        /// <summary>
        /// Initial order size
        /// </summary>
        public int InitialOrderSize { get; }

        /// <summary>
        /// Returns true if this is a buyOrder, opposite of isSellOrder
        /// </summary>
        public bool IsBuyOrder { get; }

        /// <summary>
        /// Returns true if this is a sellOrder, opposite of isBuyOrder
        /// </summary>
        public bool IsSellOrder { get { return !this.IsBuyOrder; } }


        public DateTime OrderInstanciated { get; }
        public DateTime OrderExpiry { get; }
        
        public TimeSpan TimeLeftOnOrder {
            get
            {
                return OrderExpiry - DateTime.Now;
            }
        }

        /// <summary>
        ///  Generates a new order
        /// </summary>
        /// <param name="probe">Comma separated line from a MarketLog</param>
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

            // this.Price = double.Parse(probe[10].Replace(".", ","));
            this.InitialOrderSize = int.Parse(probe[11]);
            this.IsBuyOrder = probe[9].Equals("True");
            this.OrderInstanciated = DateTime.Parse(probe[13], CultureInfo.InvariantCulture); ;
            this.OrderExpiry = this.OrderInstanciated.AddDays(int.Parse(probe[17]));
            
            

            this.filledIn = true;
        }

        /// <summary>
        /// Checks OrderSize at this current time
        /// </summary>
        /// <returns>Order size at current time</returns>
        public int OrderSizeAt()
        {
            return this.OrderSizeAt(this.LastSeen);
        }

        /// <summary>
        /// Checks OrderSize at the closest particular known point in time
        /// </summary>
        /// <param name="time">Time to look at</param>
        /// <returns>Order size at closest relevant time</returns>
        public int OrderSizeAt(DateTime time)
        {
            if (time < this.FirstSeen)
                return 0;
            DateTime best = BestTimeAt(time);
            return best <= this.LastSeen ? (int)double.Parse(this.entries[best][2].Replace(".", ",")) : 0;
        }

        /// <summary>
        /// Adds a new snapshot of the order
        /// </summary>
        /// <param name="when">Time when snapshot was taken</param>
        /// <param name="probe">Comma separated string from a marketlog</param>
        public void AddProbe(DateTime when, string[] probe)
        {
            List<string> tmp = new List<string>();
            var x = probe[13];
            tmp.Add(probe[8]); // range
            tmp.Add(probe[10]); // price
            tmp.Add(probe[12]); // Item remaining
            
            this.entries.Add(when, tmp.ToArray());
        }

        /// <summary>
        /// When the order was first observed in logs
        /// </summary>
        public DateTime FirstSeen
        {
            get { return this.entries.Keys.First(); }
        }

        /// <summary>
        /// When the order was last observed in logs
        /// </summary>
        public DateTime LastSeen
        {
            get { return this.entries.Keys.Last(); }
        }

        /// <summary>
        /// Returns true if the order is currently active
        /// </summary>
        public bool IsActive
        {
            get { return this.LastSeen == LastTime(); }
        }

        /// <summary>
        /// Returns true if you have more than one active order of the same item in the same station
        /// </summary>
        public bool HasMultipleOrdersInStation
        {
            get
            {
                return Data.Order.Orders
                    .Where(o => o.Value.StationID == this.StationID)
                    .Where(o => o.Value.TypeID == this.TypeID)
                    .Where(o => o.Value.IsActive)
                    .ToArray().Length > 1;
            }
        }


        /// <summary>
        /// Checks if a order was active at a point in time
        /// </summary>
        /// <param name="at">Specified time to check</param>
        /// <returns>True if active at specified time</returns>
        public bool WasActive(DateTime at)
        {
            return this.FirstSeen < at && at < this.LastSeen;
        }

        /// <summary>
        /// 0.0 to 1.0 percentage of remaining items
        /// </summary>
        public double OrderPercentRemaining
        {
            get
            {
                if (!this.IsActive)
                    return 0.0;
                return ((100.0 / this.InitialOrderSize) * this.OrderSizeAt(this.LastSeen)) / 100.0;
            }
        }

        /// <summary>
        /// Returns the daily average ISK the order is paying out.
        /// </summary>
        public double DailyAverage
        {
            get
            {
                double priceEarned = (this.Price * (this.InitialOrderSize - this.OrderSizeAt(this.LastSeen)));
                return  priceEarned / (this.LastSeen - this.FirstSeen).TotalDays;
            }
        }


        /// <summary>
        /// Generates headers
        /// </summary>
        /// <returns>All the headers!</returns>
        public static ColumnHeader[] GenerateMiniatureOutputHeaders()
        {
            List<ColumnHeader> columns = new List<ColumnHeader>();
            foreach(string s in new[]
            {
                "ID",
                "Type",
                "Item Name",
                "Station",
                "Price",
                "Region",
                "Size",
                "Max",
                "Percentage",
                "Time Left",
                "Stack Cost",
                "Full Stack Cost",
                "Avg Daily Income"
            })
            {
                ColumnHeader c = new ColumnHeader();
                c.Text = s;
                columns.Add(c);
            }

            return columns.ToArray();
        }


        /// <summary>
        /// Gets images!!
        /// </summary>
        public Image ItemImage
        {
            get
            {
                return Data.EVE.ItemImage.Instance.GetImageForItem(this.TypeID);
            }
        }

        /// <summary>
        /// Generates MiniatureOutput compliant stuff
        /// </summary>
        /// <returns>STUFF!!</returns>
        public ListViewItem GenerateMiniatureOutput()
        {
            return new ListViewItem(new[] {
                this.ID.ToString(),
                this.IsBuyOrder ? "BUY" : "SELL",
                this.Type,
                this.Station,
                this.Price.ToString("N0"),
                this.Region,
                this.OrderSizeAt(this.LastSeen).ToString(),
                this.InitialOrderSize.ToString(),
                (100.0 * this.OrderPercentRemaining).ToString("N0"),
                string.Format("{0:%d}d {0:%h}h {0:%m}m {0:%s}s", this.TimeLeftOnOrder),
                (this.OrderSizeAt(this.LastSeen) * this.Price).ToString("N0"),
                (this.Price * this.InitialOrderSize).ToString("N0"),
                this.DailyAverage.ToString("N0")
            });
        }

        /// <summary>
        /// Returns a ListView Friendly Array of all the 
        /// snapshots available for this order.
        /// 
        /// Format is Time, Items Remaining, Price
        /// </summary>
        /// <returns>List of ListViewItems that can be used in a ListView</returns>
        public ListViewItem[] EntityViewItems()
        {
            List<ListViewItem> all = this.entries.Select(o => new ListViewItem(new[] { o.Key.ToString(), o.Value[2], o.Value[1] })).ToList();
            if(all.Count > 0 && this.entries.Last().Key < Order.LastTime())
            {
                all.Add(new ListViewItem(new[] {
                    Order.DateTimes[Order.DateTimes.IndexOf(this.entries.Last().Key) +1 ].ToString(),
                    "---",
                    "---"
                }));
            }
            return all.ToArray();
        }
 
        
        /// <summary>
        /// Initiates a new Import of files from the standard eve marketlogs location
        /// </summary>
        public static void Import() { Import(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//EVE//logs//Marketlogs//"); }

        /// <summary>
        /// Initiates a new Import of files from a specified marketlogs location
        /// </summary>
        /// <param name="folder">Folder with marketlogs</param>
        public static void Import(String folder)
        {
            Orders.Clear();

            Directory.GetFiles(folder, "Corporation*.txt").Select(path => ParseFile(path)).ToList();
        }

        /// <summary>
        /// Parses a single file and adds all orders to the order list
        /// </summary>
        /// <param name="fileLocation">File Locations</param>
        /// <returns>False, always</returns>
        private static bool ParseFile(string fileLocation)
        {
            String filename = fileLocation.Split('/').Last().Replace("Corporation Orders-", "").Replace(".txt", "");
            DateTime savedTime = DateTime.ParseExact(filename, "yyyy.MM.dd HHmm", CultureInfo.InvariantCulture);
            StreamReader sr = new StreamReader(File.OpenRead(fileLocation));

            // 0-5      orderID,typeID,charID,charName,regionID,regionName,
            // 6-10     stationID,stationName,range,bid,price,
            // 11-15    volEntered,volRemaining,issueDate,orderState,minVolume,
            // 16-20    accountID,duration,isCorp,solarSystemID,solarSystemName,
            // 20-22    escrow,keyID

           
            Order.AddTime(savedTime);
            sr.ReadLine();
            while (!sr.EndOfStream)
            {
                string[] t = sr.ReadLine().Split(',');
                long orderId = long.Parse(t[0]);

                if (!Order.Orders.Keys.Contains(orderId))
                    Order.Orders.Add(orderId, new Order(t));

                Order.Orders[orderId].AddProbe(savedTime, t);
            }
            return false;
        }

        /// <summary>
        /// list of orders
        /// </summary>
        private static Dictionary<long, Order> orders;

        /// <summary>
        /// Singleton getter for Orders
        /// </summary>
        public static Dictionary<long, Order> Orders
        {
            get
            {
                if (orders == null)
                    orders = new Dictionary<long, Order>();
                return orders;
            }
        }

        /// <summary>
        /// List of all timestamps that has come up so far
        /// </summary>
        private static List<DateTime> checkedTimes;

        /// <summary>
        /// Signifies if the CheckedTimes variable needs to be sorted
        /// </summary>
        public static bool checkedTimeNeedsShuffle = true;

        /// <summary>
        /// Singleton getter for checkedTimes
        /// </summary>
        private static List<DateTime> CheckedTimes
        {
            get
            {
                if (checkedTimes == null)
                    checkedTimes = new List<DateTime>();
                return checkedTimes;
            }
        }

        /// <summary>
        /// Sorts the checkedTimes list if needed
        /// </summary>
        private static void sortList()
        {
            if(checkedTimeNeedsShuffle)
            {
                checkedTimeNeedsShuffle = false;
                checkedTimes.Sort();
            }
        }

        /// <summary>
        /// Adds a new time to checkedTimes, triggers a new Sort on next usage
        /// </summary>
        /// <param name="d">Time that shall be added</param>
        public static void AddTime(DateTime d)
        {
            var x = CheckedTimes;
            if (!x.Contains(d))
                x.Add(d);
            checkedTimeNeedsShuffle = true;
            checkedTimes = x;

        }

        /// <summary>
        /// Singleton getter for all DateTimes that has been added to the List
        /// </summary>
        public static List<DateTime> DateTimes
        {
            get
            {
                return CheckedTimes;
            }
        }

        /// <summary>
        /// The earliest time in memory so far
        /// </summary>
        /// <returns>Earliest time</returns>
        public static DateTime FirstTime()
        {
            sortList();
            return checkedTimes.First();
        }

        /// <summary>
        /// The latest time in memory so far
        /// </summary>
        /// <returns>Latest Time</returns>
        public static DateTime LastTime()
        {
            sortList();
            return checkedTimes.Last();
        }

        /// <summary>
        /// Returns the checked time that is before the specified time
        /// </summary>
        /// <param name="time">Time to check</param>
        /// <returns>Best snapshot time available</returns>
        public static DateTime BestTimeAt(DateTime time)
        {
            sortList();
            for(int i = 0; i < checkedTimes.Count-1; i++)
            {
                if (checkedTimes[i] <= time && checkedTimes[i + 1] > time)
                    return checkedTimes[i];
            }

            return checkedTimes.Last();
        }
    }
}
