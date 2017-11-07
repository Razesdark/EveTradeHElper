using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;

namespace EveTradingHelper.Data
{
    class DataSet
    {
        public DataSet(string fileLocation)
        {
            String filename = fileLocation.Split('/').Last().Replace("Corporation Orders-", "").Replace(".txt", "");

            DateTime savedTime = DateTime.ParseExact(filename, "yyyy.MM.dd HHmm", CultureInfo.InvariantCulture);
            

            FileStream file = File.OpenRead(fileLocation);
            StreamReader sr = new StreamReader(file);

            // orderID,typeID,charID,charName,regionID,regionName,stationID,stationName,range,bid,price,volEntered,volRemaining,issueDate,orderState,minVolume,accountID,duration,isCorp,solarSystemID,solarSystemName,escrow,keyID

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
        }

    }
}
