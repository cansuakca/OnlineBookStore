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
    public class Magazine : Product
    {
        private string m_issue;
        private string m_type;
        public Magazine()
        {
        }
        public Magazine(int ID, string name, double price, string issue, string type)
            : base(ID, name, price)
        {
            m_issue = issue;
            m_type = type;
        }
        public override void Dispose()
        {
            base.Dispose();
        }
        public override string[] printProperties(int ProductPlace)
        {
            Magazine myMagazine = new Magazine();
            StreamReader readcustomerfile;
            readcustomerfile = File.OpenText(@"Products.txt");
            string[] properties = new string[5];
            string info = "";
            string line;
            int control = 0;
            int first, last;
            while ((line = readcustomerfile.ReadLine()) != null)
            {
                control = line.IndexOf("ID : " + (200 + ProductPlace));
                if (control != -1)
                {
                    //Set ID To Magazine With Read Text File
                    first = line.IndexOf("ID : ") + "ID : ".Length;
                    last = line.IndexOf(",Name");
                    info = line.Substring(first, last - first);
                    myMagazine.setID(Convert.ToInt32(info));
                    properties[0] = myMagazine.getID().ToString();

                    //Set Name To Magazine With Read Text File
                    first = line.IndexOf("Name : ") + "Name : ".Length;
                    last = line.IndexOf(",Price");
                    info = line.Substring(first, last - first);
                    myMagazine.setName(info);
                    properties[1] = myMagazine.getName();


                    //Set Price To Magazine With Read Text File
                    first = line.IndexOf("Price : ") + "Price : ".Length;
                    last = line.IndexOf(",Issue");
                    info = line.Substring(first, last - first);
                    myMagazine.setPrice(Convert.ToInt32(info));
                    properties[2] = myMagazine.getPrice().ToString();


                    //Set Issue To Magazine With Read Text File
                    first = line.IndexOf("Issue : ") + "Issue : ".Length;
                    last = line.IndexOf(",Type");
                    info = line.Substring(first, last - first);
                    myMagazine.setissue(info);
                    properties[3] = myMagazine.getissue();


                    //Set Type To Magazine With Read Text File
                    first = line.IndexOf("Type : ") + "Type : ".Length;
                    last = line.Length;
                    info = line.Substring(first, last - first);
                    myMagazine.setType(info);
                    properties[4] = myMagazine.getType();
                }
            }
            return properties;

        }

        public string getissue()
        {
            return m_issue;
        }
        public void setissue(string issue)
        {
            m_issue = issue;
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
