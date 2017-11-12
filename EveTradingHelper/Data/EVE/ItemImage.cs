using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EveTradingHelper.Data.EVE
{
    class ItemImage
    {


        private static ItemImage instance;
        private ItemImage()
        {
            if(!Directory.Exists("./images"))
            {
                Directory.CreateDirectory("images");
            }

        }

        public Image GetImageForItem(long id)
        {
            if(!Directory.Exists("images/type_" + id.ToString() + "_64.png"))
            {
                try
                {
                    string url = "https://image.eveonline.com/Type/" + id.ToString() + "_64.png";
                    string path = "images/type_" + id.ToString() + "_64.png";
                    (new WebClient()).DownloadFile(url, path);
                } catch (WebException e)
                {
                    // MessageBox.Show(e.Message + ":" + e.Status);
                    return null;
                }
            }
            return Image.FromFile("images/type_" + id.ToString() + "_64.png");
        }


        public static ItemImage Instance
        {
            get {
                if (instance == null)
                    instance = new ItemImage();
                return instance;
            }
        }
    }
}
