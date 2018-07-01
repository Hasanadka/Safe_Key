using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;
namespace SafeKey
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            t_Tick(null, null);
            KeyGenerator();
            
        }
       
        Timer t = new Timer();
        Timer t2 = new Timer();
        Random rnd = new Random();

        private void KeyGenerator()
        {

            t.Start(); 
            t.Interval = 1000 * 20; 
            t.Tick += new EventHandler(this.t_Tick);
            t2.Interval = 200;
            t2.Tick += new EventHandler(this.timer1_Tick);
        }

        private void t_Tick(object sender, EventArgs e)
        {
            t2.Start();
            RandomCreate();
            
            progressBar1.Value = 0;
            progressBar1.Maximum = 100;
            
        }

        void timer1_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value != progressBar1.Maximum)
                progressBar1.Increment(1);
            else
            {
                t2.Stop();
            }
           
        }


        void RandomCreate()
        {
            string input = "Sagar@tcs.com";
            char[] values = input.ToCharArray();
            char[] delimiter = {'-',':','/',' '};
            string hexstring = string.Empty;
            string hexOutput = string.Empty;
            string hexFormat = string.Empty;
            string[] Interval;
            int value;
            string timestring = string.Empty;
            BigInteger Output = 0;
            foreach (char letter in values)
            {
                
                value = Convert.ToInt32(letter);
                
                hexOutput = String.Format("{0:X}", value);
                hexFormat= Convert.ToString(Convert.ToInt32(hexOutput, 16), 8);
                hexstring = hexstring + (hexFormat.Length == 2 ? "0" + hexFormat : hexFormat);
            }
            Interval = new string[8];
            Interval=DateTime.UtcNow.ToString().Split(delimiter);
            //Interval[5] = (Convert.ToInt32(Interval[5]) < 30 ? "17" : "47");
            if (Convert.ToInt32(Interval[5]) < 20)
                Interval[5] = "17";
            else if (Convert.ToInt32(Interval[5]) < 40)
                Interval[5] = "37";
            else 
                Interval[5] = "57";
            timestring = (Convert.ToInt32(Interval[4] + Interval[5] + Interval[3]) * Convert.ToInt32(Interval[4] + Interval[5])).ToString() + Interval[5];
            
            while (hexstring.Length > 12)
            {
                BigInteger var = 0;
                var = BigInteger.Parse(hexstring) / 12;
                hexstring = var.ToString();
            }

            while (hexstring.Length < 12)
            {
                hexstring = (BigInteger.Parse(hexstring)*3).ToString();
            }

            hexOutput = (((BigInteger.Parse(hexstring + Interval[5]) / BigInteger.Parse(timestring)) * Convert.ToInt32(Interval[4] + Interval[5])) / Convert.ToInt32(Interval[5] + Interval[4])).ToString();

            while (hexOutput.Length < 6)
            {
                Output = BigInteger.Parse(hexOutput)<< 1;
                hexOutput = Output.ToString();
            }

            textBox1.Text = hexOutput;
        }
    }
}
