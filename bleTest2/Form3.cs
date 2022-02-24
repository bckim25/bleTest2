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
    public partial class Form3 : Form
    {
        PaintEventArgs eArgs;

        public Form3()
        {
            InitializeComponent();


            //this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            //this.UpdateStyles();
        }

        private void picCanvas_Paint(object sender, PaintEventArgs e)
        {
            //eArgs = e;
            Graphics canvas = e.Graphics;
            Brush snakeColour;

            snakeColour = Brushes.Red;

            Random r = new Random();
            int height = r.Next(10, 100);
            int width = r.Next(10, 100);

            for (int i = 0; i < 500; i++)
            {
                canvas.FillEllipse(snakeColour, new Rectangle
                    (
                        height,
                        height,
                        width, width
                     ));
                Thread.Sleep(500);
            }


        }

        //화면 깜빡임 방지
        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            //Thread _thread = new Thread(new ThreadStart(run));
            //_thread.Start();

        }

        private void run()
        {
            while (true)
            {
                Random r = new Random();
                int height = r.Next(10, 100);
                int width = r.Next(10, 100);


                if (InvokeRequired)
                {
                    Invoke(new Action(delegate ()
                    {
                        Graphics canvas = eArgs.Graphics;
                        Brush snakeColour;

                        snakeColour = Brushes.Red;

                        canvas.FillEllipse(snakeColour, new Rectangle
                        (
                        height,
                        height,
                        width, width
                        ));                                             

                    }));
                }
            }
        }


    }
}
