using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveTradingHelper.Data
{
    class Import
    {
        public static void ImportFiles(String folder=null)
        {
            if (folder == null)
                folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//Eve//logs//Marketlogs/";

            Directory.GetFiles(folder, "Corporation*.txt").Select(path => new DataSet(path)).ToList();

            var y = Order.DateTimes;
            var x = Order.Orders.Where(order => order.Value.IsActive == false).ToArray();

        }
    }
}
