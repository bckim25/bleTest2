using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bleTest2
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void btnForm1_Click(object sender, EventArgs e)
        {
            Form1 frm1 = new Form1();
            frm1.ShowDialog();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            Thread myThread = new Thread(Func);
            myThread.Start();
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine(i + 1);
                Thread.Sleep(1000);
            }
            Console.WriteLine("메인쓰레드 종료");
        }

        private static void Func()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(i + 1);
                Thread.Sleep(1000);
            }
        }
    }
}
