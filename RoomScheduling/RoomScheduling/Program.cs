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
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            DBConnector.InitializeDB();
            //Application.Run(new Form);

            //Attempt to do a login
            VerifyControl verifyControl = new VerifyControl();
            verifyControl.login("bryangarris@university.edu","H@rdcore99");
            verifyControl.login("admin@university.edu", "Password1!");

        }

    }

}