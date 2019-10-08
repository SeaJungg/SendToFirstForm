using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SimioAPI;
using SimioAPI.Extensions;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Data;
using System.IO;
using System.Collections;
using System.Drawing;
using System.Data.SqlClient;



namespace SendMyBox
{
    class UserAddIn : IDesignAddIn
    {
        #region IDesignAddIn Members
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindowEx(IntPtr hWnd1, IntPtr hWnd2, string lpsz1, string lpsz2);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        public static extern int SendMessage(IntPtr hwnd, uint wMsg, int wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        public static extern int SendMessage(IntPtr hwnd, uint wMsg, uint wParam, int lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        public static extern int SendMessage(IntPtr hwnd, uint wMsg, int wParam, string lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        public static extern IntPtr PostMessage(IntPtr hwnd, uint wMsg, uint wParam, int lParam);

        [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "SendMessage", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern bool SendMessage(IntPtr hWnd, uint Msg, int wParam, StringBuilder lParam);

        IntPtr handle_parent;
        IntPtr handle_box;
        private const uint WM_SETTEXT = 0x000C;

        public string Name
        {
            get { return "SendMyBox"; }
        }
        
        public string Description
        {
            get { return "첫째창을 찾아서 보낼 것이다"; }
        }
        public System.Drawing.Image Icon
        {
            get { return null; }
        }

        IDesignContext _context;
        Form1 form;
        public void Execute(IDesignContext context)
        {
            _context = context;
            if (form == null || form.IsDisposed)
            {

                form = new Form1();
                form.button1.Click += sendrespose;
                form.Show();
            }
        }

        void sendrespose(object sender, EventArgs e)
        {
            string loc = form.textBox1.Text;
            handle_parent = FindWindow("WindowsForms10.Window.8.app.0.1983833_r9_ad1", "MainManager");
            handle_box = FindWindowEx(handle_parent, IntPtr.Zero, "WindowsForms10.EDIT.app.0.1983833_r9_ad1", loc);

            string sendtxt = form.mybox.Text;
            SendMessage(handle_box, WM_SETTEXT, 0, sendtxt);
        }
        #endregion
    }
}
