using RoomScheduling.Controllers;
using RoomScheduling.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RoomScheduling
{
    public partial class AdminForm : Form
    {
        public AdminForm()
        {
            InitializeComponent();
            this.Load += listView1_SelectedIndexChanged;
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
         
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sub;
            DateTime date;
            string usn;
            List<Request> req = DBConnector.getPendingRequests();
            foreach (Request item in req)
            {
                usn = item.getUsn();
                sub = item.getSubject();
                date = item.getDate();
                Console.WriteLine(usn);
                ListViewItem listViewItem = new ListViewItem();
                listViewItem.SubItems.Add(sub);
                listViewItem.SubItems.Add(date.ToString());
                listViewItem.SubItems.Add(usn.ToString());
                listView1.Items.Add(listViewItem);
            }
        }
    }
}
