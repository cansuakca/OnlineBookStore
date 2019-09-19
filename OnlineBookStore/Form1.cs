using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Management;
using System.Threading;
using System.Collections;

namespace OnlineBookStore
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        ShoppingCart myShoppingCart = ShoppingCart.getInstance();


        private void pngExit_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void pngMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            
        }
        
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string Date;
            if (Control("Username : " + txtUsername.Text + "," + "Password : " + txtPassword.Text) == true)
            {
                timerLogin.Enabled = true;
                pbLogin.Visible = true;
                lblPleaseWait.Visible = true;
                lblProgBar.Visible = true;

                Customer myCustomer = new Customer();
                myCustomer = myCustomer.printCustomerDetails(txtUsername.Text);

                lblCustomerID.Text = myCustomer.getCustomerID().ToString();
                lblUsername.Text = myCustomer.getUsername();
                lblName.Text = myCustomer.getName();
                lblEmail.Text = myCustomer.getEmail();
                lblAddress.Text = myCustomer.getAddress();

                
                Date = String.Format("{0:r}", DateTime.Now) + "+";
                myCustomer.AppendDateToCustomer("CustomersLoginTimes.txt", "Username with " + myCustomer.getUsername(), Date);
                lblProgBar.Text += myCustomer.getUsername();

                CreateAndListForBook(listBook);
                CreateAndListForMusicCD(listMusicCD);
                CreateAndListForMagazine(listMagazine);
               
               
            }
            else
            {
                MessageBox.Show("Login Failed !!");
            }
        }
        private bool Control(string item)
        {
            StreamReader readcustomerfile;
            readcustomerfile = File.OpenText(@"CustomersInformations.txt");
            bool controlresult = false;
            string lines;
            int result;
            while ((lines = readcustomerfile.ReadLine()) != null) 
            {
                result = lines.IndexOf(item);
                if (result != -1)
                {
                    controlresult = true;
                    break;
                }
                else
                {
                    controlresult = false;
                }
            }
            readcustomerfile.Close();
            return controlresult;
        }
        private int PropertiesCountOfItem(string item)
        {
            int count = 0;
            StreamReader readcustomerfile;
            readcustomerfile = File.OpenText(@"Products.txt");
            string lines;
            int result;
            while ((lines = readcustomerfile.ReadLine()) != null)
            {
                result = lines.IndexOf(item);
                if (result != -1)
                {
                    foreach (char i in lines)
                    {
                        if (i == ',')
                            count++;
                    }
                }
                else
                {

                }
            }
            readcustomerfile.Close();
            return count+1;
        }
        private int ItemCount(string item)
        {
            int count = 0;
            StreamReader readcustomerfile;
            readcustomerfile = File.OpenText(@"Products.txt");
            string lines;
            int result;
            while ((lines = readcustomerfile.ReadLine()) != null)
            {
                result = lines.IndexOf(item);
                if (result != -1)
                {
                    count++;
                }
                else
                {
                    
                }
            }
            readcustomerfile.Close();
            return count;
        }
        private void btnRegister2_Click(object sender, EventArgs e)
        {
            if (Control("Username : " + txtregisterUsername.Text) == false && Control("E-Mail : " + txtregisterEmail.Text) == false)
            {
                timerRegister.Enabled = true;
                pbRegister.Visible = true;
                int customerID = 0;
                StreamReader readcustomerfile;
                readcustomerfile = File.OpenText(@"CustomersLoginTimes.txt");
                while (readcustomerfile.ReadLine() != null)
                {
                    customerID++;
                }
                readcustomerfile.Close();
                customerID += 100000;

                Customer myCustomer = new Customer(customerID, txtregisterUsername.Text, txtregisterPassword.Text, txtregisterName.Text, txtregisterEmail.Text, txtregisterAddress.Text);
                myCustomer.saveCustomer();

         
                
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            Customer.SelectedIndex = 1;
        }

        private void btnBackLogin_Click(object sender, EventArgs e)
        {
            Customer.SelectedIndex = 0;
        }

       

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            if (Customer.SelectedIndex == 0)
            {
                pbLogin.Value = 0;
                timerLogin.Enabled = false;
                txtUsername.Text = "";
                txtPassword.Text = "";
                lblProgBar.Visible = false;
                lblPleaseWait.Visible = false;
                pbLogin.Visible = false;
            }
            if (Customer.SelectedIndex == 3)
            {
                pbLogin.Value = 0;
                timerLogin.Enabled = false;
            }
            if (Customer.SelectedIndex == 1)
            {
                pbRegister.Value = 0;
                timerRegister.Enabled = false;
                pbRegister.Visible = false;
            }

            
        }

       

        private void btnLogout_Click(object sender, EventArgs e)
        {
     
            lblProgBar.Text = "Loginning With Username : ";
       
            Customer.SelectedIndex = 0;

        }

        private void btnUserLoginTimes_Click_1(object sender, EventArgs e)
        {
            int j = 0;
            int control;
            string line;
            string addedstr = "";
            StreamReader readcustomerfile;
            readcustomerfile = File.OpenText(@"CustomersLoginTimes.txt");
            while ((line = readcustomerfile.ReadLine()) != null)
            {
                control = line.IndexOf(lblUsername.Text);
                if (control != -1)
                {
                    for (int i = 0; i < line.Length; i++)
                    {
                        if (line[i] == '|')
                        {
                            j = i + 4;
                            while (line[j] != '|' && line[j] != line[line.Length - 1])
                            {
                                addedstr += line[j];
                                j++;
                                i++;
                            }

                            listBoxUserLoginTimes.Items.Add(addedstr);
                            addedstr = "";
                        }
                    }
                }
            }
            listBoxUserLoginTimes.Visible = true;
        }

        private void timerRegister_Tick(object sender, EventArgs e)
        {

            while (pbRegister.Value != 100)
            {
                if (pbRegister.Value == 0)
                {
                    lblRegisterProgBar.Text = "Please Wait For Control...";
                }
                else if (pbRegister.Value == 50)
                {
                    lblRegisterProgBar.Text = "Register Successfull ! ";
                }
                lblRegisterProgBar.Refresh();
                pbRegister.Value += 1;
                Thread.Sleep(25);
                pbRegister.Refresh();
                
            }
            Customer.SelectedIndex = 0;
        }
        private void timerLogin_Tick(object sender, EventArgs e)
        {
            while (pbLogin.Value != 100)
            {
                pbLogin.Value += 1;
                Thread.Sleep(25);
                pbLogin.Refresh();
            }
            Customer.SelectedIndex = 3;
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pbRegister.Visible = false;
            lblRegisterProgBar.Visible = false;
            listProducts.View = View.Details;
            listProducts.GridLines = true;
            listProducts.FullRowSelect = true;
            listProducts.Columns.Add("ID", 60);
            listProducts.Columns.Add("Name", 172);
            listProducts.Columns.Add("Price", 60);
            listProducts.Columns.Add("Quantity", 60); 
        }

 
        private void CreateAndListForBook(ListView listview)
        {
            CreatorProduct myProduct = new CreatorProduct();
            int count = ItemCount("Magazine") + ItemCount("Book") + ItemCount("MusicCD");
            Product[] myProducts = new Product[count];
            string[] properties = new string[PropertiesCountOfItem("Book")];
            int place = 1;
    
            for (int i = 0; i < ItemCount("Book"); i++)
            {
                
                myProducts[i] = myProduct.FactoryMethod(ProductType.Book);
                properties = myProducts[i].printProperties(place);
                string[] row = { properties[0], properties[1], properties[2], properties[3], properties[4], properties[5] };
                var line = new ListViewItem(row);
                listview.Items.Add(line);
                place++;
            }

            listview.View = View.Details;
            listview.GridLines = true;
            listview.FullRowSelect = true;
            listview.Columns.Add("ID", 30);
            listview.Columns.Add("Name", 80);
            listview.Columns.Add("Price", 40);
            listview.Columns.Add("ISBN", 50);
            listview.Columns.Add("Author", 80);
            listview.Columns.Add("Publisher", 70);
            
        }
        private void CreateAndListForMusicCD(ListView listview)
        {
            CreatorProduct myProduct = new CreatorProduct();
            int count = ItemCount("Magazine") + ItemCount("Book") + ItemCount("MusicCD");
            Product[] myProducts = new Product[count];
            string[] properties = new string[PropertiesCountOfItem("MusicCD")];
            int place = 1;

            for (int i = 0; i < ItemCount("MusicCD"); i++)
            {

                myProducts[i] = myProduct.FactoryMethod(ProductType.MusicCD);
                properties = myProducts[i].printProperties(place);
                string[] row = { properties[0], properties[1], properties[2], properties[3], properties[4] };
                var line = new ListViewItem(row);
                listview.Items.Add(line);
                place++;
            }

            listview.View = View.Details;
            listview.GridLines = true;
            listview.FullRowSelect = true;
            listview.Columns.Add("ID", 30);
            listview.Columns.Add("Name", 80);
            listview.Columns.Add("Price", 40);
            listview.Columns.Add("Singer", 100);
            listview.Columns.Add("Type", 100);
        }

        private void CreateAndListForMagazine(ListView listview)
        {
            CreatorProduct myProduct = new CreatorProduct();
            int count = ItemCount("Magazine") + ItemCount("Book") + ItemCount("MusicCD");
            Product[] myProducts = new Product[count];
            string[] properties = new string[PropertiesCountOfItem("Magazine")];
            int place = 1;

            for (int i = 0; i < ItemCount("Magazine"); i++)
            {

                myProducts[i] = myProduct.FactoryMethod(ProductType.Magazine);
                properties = myProducts[i].printProperties(place);
                string[] row = { properties[0], properties[1], properties[2], properties[3], properties[4] };
                var line = new ListViewItem(row);
                listview.Items.Add(line);
                place++;
            }

            listview.View = View.Details;
            listview.GridLines = true;
            listview.FullRowSelect = true;
            listview.Columns.Add("ID", 30);
            listview.Columns.Add("Name", 80);
            listview.Columns.Add("Price", 40);
            listview.Columns.Add("Issue", 100);
            listview.Columns.Add("Type", 100); 
        }

        private void btnAddBook_Click(object sender, EventArgs e)
        {
            listBook.Visible = true;
            listMusicCD.Visible = false;
            listMagazine.Visible = false;
        }

        private void btnAddMusicCD_Click(object sender, EventArgs e)
        {
            listBook.Visible = false;
            listMusicCD.Visible = true;
            listMagazine.Visible = false;
        }

        private void btnAddMagazine_Click(object sender, EventArgs e)
        {
            listBook.Visible = false;
            listMusicCD.Visible = false;
            listMagazine.Visible = true;
        }

        

        private string[] ProductTypeControl()
        {
            string str = "";
            int place = 0;
            string[] myArray = new string[2];
            if (listBook.Visible == true)
            {
                for (int i = 0; i < listBook.Items.Count; i++)
                {
                    if (listBook.Items[i].Selected == true)
                    {
                        str = "Book";
                        place = i;
                    }
                }
            }
            else if (listMusicCD.Visible == true)
            {
                for (int i = 0; i < listMusicCD.Items.Count; i++)
                {
                    if (listMusicCD.Items[i].Selected == true)
                    {
                        str = "MusicCD";
                        place = i;
                    }
                }
            }
            else
            {
                for (int i = 0; i < listMagazine.Items.Count; i++)
                {
                    if (listMagazine.Items[i].Selected == true)
                    {
                        str = "Magazine";
                        place = i;
                    }
                }
            }
            myArray[0] = str;
            myArray[1] = place.ToString();
            return myArray;
        }

        private void btnAddProduct_Click_1(object sender, EventArgs e)
        {
            if (cbProductQuantity.Text == "")
            {
                MessageBox.Show("Please Set Quantity!");
            }
            else
            {

                CreatorProduct myProduct = new CreatorProduct();
                string[] myArray = new string[2];
                myArray = ProductTypeControl();



                if (myArray[0] == "Book")
                {
                    Product myBook = myProduct.FactoryMethod(ProductType.Book);
                    myBook.setID(Convert.ToInt32(listBook.Items[Convert.ToInt32(myArray[1])].SubItems[0].Text));
                    myBook.setName(listBook.Items[Convert.ToInt32(myArray[1])].SubItems[1].Text);
                    myBook.setPrice(Convert.ToInt32(listBook.Items[Convert.ToInt32(myArray[1])].SubItems[2].Text));
                    ItemToPurchase item = new ItemToPurchase(myBook, Convert.ToInt32(cbProductQuantity.Text));
                    myShoppingCart.addProduct(item);
                    string[] row = myShoppingCart.printProducts(item);
                    var line = new ListViewItem(row);
                    listProducts.Items.Add(line);
                }
                else if (myArray[0] == "MusicCD")
                {
                    Product myMusicCD = myProduct.FactoryMethod(ProductType.MusicCD);
                    myMusicCD.setID(Convert.ToInt32(listMusicCD.Items[Convert.ToInt32(myArray[1])].SubItems[0].Text));
                    myMusicCD.setName(listMusicCD.Items[Convert.ToInt32(myArray[1])].SubItems[1].Text);
                    myMusicCD.setPrice(Convert.ToInt32(listMusicCD.Items[Convert.ToInt32(myArray[1])].SubItems[2].Text));
                    ItemToPurchase item = new ItemToPurchase(myMusicCD, Convert.ToInt32(cbProductQuantity.Text));
                    myShoppingCart.addProduct(item);
                    string[] row = myShoppingCart.printProducts(item);
                    var line = new ListViewItem(row);
                    listProducts.Items.Add(line);
                }
                else if (myArray[0] == "Magazine")
                {
                    Product myMagazine = myProduct.FactoryMethod(ProductType.Magazine);
                    myMagazine.setID(Convert.ToInt32(listMagazine.Items[Convert.ToInt32(myArray[1])].SubItems[0].Text));
                    myMagazine.setName(listMagazine.Items[Convert.ToInt32(myArray[1])].SubItems[1].Text);
                    myMagazine.setPrice(Convert.ToInt32(listMagazine.Items[Convert.ToInt32(myArray[1])].SubItems[2].Text));
                    ItemToPurchase item = new ItemToPurchase(myMagazine, Convert.ToInt32(cbProductQuantity.Text));
                    myShoppingCart.addProduct(item);
                    string[] row = myShoppingCart.printProducts(item);
                    var line = new ListViewItem(row);
                    listProducts.Items.Add(line);
                }
            }
        }

        private void btnRemoveProduct_Click(object sender, EventArgs e)
        {
            if (cbRemovedProductQuantity.Text == "")
            {
                MessageBox.Show("Please Set Quantity!");
            }
            else
            {
                string[] properties;
                int place = 0;
                for (int i = 0; i < listProducts.Items.Count; i++)
                {
                    if (listProducts.Items[i].Selected == true)
                    {
                        int newQuantity = 0;
                        if (Convert.ToInt32(listProducts.Items[i].SubItems[0].Text) > 100 && Convert.ToInt32(listProducts.Items[i].SubItems[0].Text) < 200)
                        {
                            properties = new string[PropertiesCountOfItem("Book")];
                            Book myBook = new Book();
                            place = Convert.ToInt32(listProducts.Items[i].SubItems[0].Text) - 100;
                            properties = myBook.printProperties(place);
                            myBook.setID(Convert.ToInt32(properties[0]));
                            myBook.setName(properties[1]);
                            myBook.setPrice(Convert.ToInt32(properties[2]));
                            myBook.setISBN(Convert.ToInt32(properties[3]));
                            myBook.setAuthor(properties[4]);
                            myBook.setPublisher(properties[5]);
                            ItemToPurchase item = new ItemToPurchase(myBook, Convert.ToInt32(cbRemovedProductQuantity.Text));
                            myShoppingCart.removeProduct(item);
                            if (Convert.ToInt32(cbRemovedProductQuantity.Text) >= Convert.ToInt32(listProducts.Items[i].SubItems[3].Text))
                            {
                                listProducts.Items[i].Remove();
                            }
                            else
                            {
                                newQuantity = Convert.ToInt32(listProducts.Items[i].SubItems[3].Text) - Convert.ToInt32(cbRemovedProductQuantity.Text);
                                listProducts.Items[i].SubItems[3].Text = newQuantity.ToString();
                                listProducts.Refresh();
                            }
                        }
                        else if (Convert.ToInt32(listProducts.Items[i].SubItems[0].Text) > 200 && Convert.ToInt32(listProducts.Items[i].SubItems[0].Text) < 300)
                        {
                            properties = new string[PropertiesCountOfItem("Magazine")];
                            Magazine myMagazine = new Magazine();
                            place = Convert.ToInt32(listProducts.Items[i].SubItems[0].Text) - 200;
                            properties = myMagazine.printProperties(place);
                            myMagazine.setID(Convert.ToInt32(properties[0]));
                            myMagazine.setName(properties[1]);
                            myMagazine.setPrice(Convert.ToInt32(properties[2]));
                            myMagazine.setissue(properties[3]);
                            myMagazine.setType(properties[4]);
                            ItemToPurchase item = new ItemToPurchase(myMagazine, Convert.ToInt32(cbRemovedProductQuantity.Text));
                            myShoppingCart.removeProduct(item);
                            if (Convert.ToInt32(cbRemovedProductQuantity.Text) >= Convert.ToInt32(listProducts.Items[i].SubItems[3].Text))
                            {
                                listProducts.Items[i].Remove();
                            }
                            else
                            {
                                newQuantity = Convert.ToInt32(listProducts.Items[i].SubItems[3].Text) - Convert.ToInt32(cbRemovedProductQuantity.Text);
                                listProducts.Items[i].SubItems[3].Text = newQuantity.ToString();
                                listProducts.Refresh();
                            }
                        }
                        else
                        {
                            properties = new string[PropertiesCountOfItem("MusicCD")];
                            MusicCD myMusicCD = new MusicCD();
                            place = Convert.ToInt32(listProducts.Items[i].SubItems[0].Text) - 300;
                            properties = myMusicCD.printProperties(place);
                            myMusicCD.setID(Convert.ToInt32(properties[0]));
                            myMusicCD.setName(properties[1]);
                            myMusicCD.setPrice(Convert.ToInt32(properties[2]));
                            myMusicCD.setsinger(properties[3]);
                            myMusicCD.setType(properties[4]);
                            ItemToPurchase item = new ItemToPurchase(myMusicCD, Convert.ToInt32(cbRemovedProductQuantity.Text));
                            myShoppingCart.removeProduct(item);
                            if (Convert.ToInt32(cbRemovedProductQuantity.Text) >= Convert.ToInt32(listProducts.Items[i].SubItems[3].Text))
                            {
                                listProducts.Items[i].Remove();
                            }
                            else
                            {
                                newQuantity = Convert.ToInt32(listProducts.Items[i].SubItems[3].Text) - Convert.ToInt32(cbRemovedProductQuantity.Text);
                                listProducts.Items[i].SubItems[3].Text = newQuantity.ToString();
                                listProducts.Refresh();
                            }
                        }
                    }
                }
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
