using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Management;
using System.Threading;

namespace OnlineBookStore
{
    public class MusicCD : Product
    {
       
        private string m_singer;
        private string m_type;
        public MusicCD()
        {
        }
        public MusicCD(int ID, string name, double price, string singer, string type)
            : base(ID, name, price)
        {
            m_singer = singer;
            m_type = type;
        }
        public override void Dispose()
        {
            base.Dispose();
        }
        public override string[] printProperties(int ProductPlace)
        {
            MusicCD myMusicCD = new MusicCD();
            StreamReader readcustomerfile;
            readcustomerfile = File.OpenText(@"Products.txt");
            string[] properties = new string[5];
            string info = "";
            string line;
            int control = 0;
            int first, last;
            while ((line = readcustomerfile.ReadLine()) != null)
            {
                control = line.IndexOf("ID : " + (300 + ProductPlace));
                if (control != -1)
                {
                    //Set ID To MusicCD With Read Text File
                    first = line.IndexOf("ID : ") + "ID : ".Length;
                    last = line.IndexOf(",Name");
                    info = line.Substring(first, last - first);
                    myMusicCD.setID(Convert.ToInt32(info));
                    properties[0] = myMusicCD.getID().ToString();

                    //Set Name To MusicCD With Read Text File
                    first = line.IndexOf("Name : ") + "Name : ".Length;
                    last = line.IndexOf(",Price");
                    info = line.Substring(first, last - first);
                    myMusicCD.setName(info);
                    properties[1] = myMusicCD.getName();


                    //Set Price To MusicCD With Read Text File
                    first = line.IndexOf("Price : ") + "Price : ".Length;
                    last = line.IndexOf(",Singer");
                    info = line.Substring(first, last - first);
                    myMusicCD.setPrice(Convert.ToInt32(info));
                    properties[2] = myMusicCD.getPrice().ToString();


                    //Set Issue To MusicCD With Read Text File
                    first = line.IndexOf("Singer : ") + "Singer : ".Length;
                    last = line.IndexOf(",Type");
                    info = line.Substring(first, last - first);
                    myMusicCD.setsinger(info);
                    properties[3] = myMusicCD.getsinger();


                    //Set Type To MusicCD With Read Text File
                    first = line.IndexOf("Type : ") + "Type : ".Length;
                    last = line.Length;
                    info = line.Substring(first, last - first);
                    myMusicCD.setType(info);
                    properties[4] = myMusicCD.getType();
                }
            }
            return properties;

        }
        public string getsinger()
        {
            return m_singer;
        }
        public void setsinger(string singer)
        {
            m_singer = singer;
        }
        public string getType()
        {
            return m_type;
        }
        public void setType(string type)
        {
            m_type = type;
        }



    }
}
