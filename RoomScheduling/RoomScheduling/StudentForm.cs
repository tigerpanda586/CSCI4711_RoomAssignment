using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RoomScheduling.Controllers;
using RoomScheduling.Entity;

namespace RoomScheduling
{
    public partial class StudentForm : Form
    {
        private string _usr;
        public StudentForm(string usr)
        {
            InitializeComponent();
            _usr = usr;
            this.Load += listView1_SelectedIndexChanged;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void StudentForm_Load(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sub;
            DateTime date;
            bool status;
            int room;
            List<Request> req = DBConnector.getRequests(_usr);
            Console.WriteLine(req);
            foreach (Request item in req)
            {
                status = item.getStatus();
                room = item.getRoomNo();
                sub = item.getSubject();
                date = item.getDate();
                ListViewItem listViewItem = new ListViewItem();
                listViewItem.SubItems.Add(status.ToString());
                listViewItem.SubItems.Add(sub);
                listViewItem.SubItems.Add(date.ToString()); 
                listViewItem.SubItems.Add(room.ToString());
                listView1.Items.Add(listViewItem);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
