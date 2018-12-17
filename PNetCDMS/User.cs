using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PNetCDMS
{
    public class user
    {
        int id = 0;
        int rank = 0;
        string fName = "Unknown";
        string lName = "Unknown";
        string status = "Employed";
        string username = "Unknown";

        public void set(int uid, int urank, string ufname, string ulname, string ustatus, string userName)
        {
            id = uid;
            rank = urank;
            fName = ufname;
            lName = ulname;
            status = ustatus;
            userName = username;
        }

        //setters
        public void setFname(string f) { fName = f; }
        public void setLname(string f) { lName = f; }
        public void setID(int f) { id = f; }
        public void setRank(int f) { rank = f; }
        public void setStatus(string f) { status = f; }

        //getters
        public string getFullName() { return fName + " " + lName; }
        public string getfName() { return fName; }
        public string getUsername() { return username; }
        public string getlName() { return lName; }
        public string getStatus() { return status; }
        public int getRank() { return rank; }
        public int getID() { return id; }

        public bool verifyRank(int reqRank, Form form)
        {
            if (reqRank > rank)
            {
                MessageBox.Show("Unable to access this form. Contact an administrator for more help.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Information);
                form.Close();
                return false;
            }
            return true;
        }
    }
}
