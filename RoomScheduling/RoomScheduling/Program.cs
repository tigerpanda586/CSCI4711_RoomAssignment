using RoomScheduling.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RoomScheduling.Controllers
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            DBConnector.InitializeDB();
            LoginForm loginform = new LoginForm();
            loginform.ShowDialog();
        }

    }

}