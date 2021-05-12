using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class formCalculator : Form
    {

        private const string resulttext = "0";

        private TextBox izhod() => this.txtrezultat;
        private bool novrezultat= false;
        private bool mem_flg = false;
        private double stoynost = 0;
        private string poslednaOP= "+";  
        double pamet = 0;



        public formCalculator()
        {
            InitializeComponent();
            izhod().Text = resulttext;
        }

        public void AppendDecimal()
        {
            if (novrezultat)
            {
                novrezultat = false;
                izhod().Text = resulttext;
            }

           
            if (izhod().Text.Contains("."))
            {
                return;
            }
            else
            {
                izhod().AppendText(".");
            }
        }

        public void AppendNum(string num)
        {
            if (novrezultat)
            {
                novrezultat = false;
                izhod().Text = resulttext;
            }
            izhod().AppendText(num);
            if (izhod().Text.Contains("."))
            {
                return;
            }
            else
            {
                izhod().Text = Double.Parse(izhod().Text).ToString();
            }
        }

        public void Calculate(string op) 
        {
            izhod().Text.TrimEnd('.');
            txtoperacii.AppendText($" {izhod().Text} {op}");
            double operand = Double.Parse(izhod().Text);
            novrezultat = true;

            
            if (poslednaOP == "/" && izhod().Text == "0")
            {
                MessageBox.Show("Не делим на 0");
                izhod().Text = resulttext;
                return;
            }

            if (poslednaOP == "+") 
            {
                stoynost += operand;
            }
            if (poslednaOP == "-") 
            {
                stoynost -= operand;
            }
            if (poslednaOP == "*") 
            { 
                stoynost *= operand; 
            }
            if (poslednaOP == "/")
            { 
                stoynost /= operand; 
            }


            poslednaOP = op;
            izhod().Text = stoynost.ToString();
        }

        public void Clear()
        {
            stoynost = 0;
            txtoperacii.Clear();
            izhod().Text = resulttext;
            novrezultat = false;
        }

       

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        
        private void buttonNum_Click(object sender, EventArgs e) => AppendNum(((Button)sender).Text);

       
        private void buttonDecimal_Click(object sender, EventArgs e) => AppendDecimal();

       
        private void buttonClear_Click(object sender, EventArgs e) => Clear();
        private void buttonDelete_Click(object sender, EventArgs e)

        {
            izhod().Text = izhod().Text.Substring(0, izhod().Text.Length - 1);
            if (String.IsNullOrEmpty(izhod().Text)) { izhod().Text = resulttext; }
        }

        
        private void buttonDivide_Click(object sender, EventArgs e) => Calculate("/");
        private void buttonMultiply_Click(object sender, EventArgs e) => Calculate("*");
        private void buttonSubtract_Click(object sender, EventArgs e) => Calculate("-");
        private void buttonAdd_Click(object sender, EventArgs e) => Calculate("+");
        private void buttonEquals_Click(object sender, EventArgs e)
        {
            Calculate("+");

            string rezultat = izhod().Text;
            Clear();
            novrezultat = true;
            izhod().Text = rezultat;

        }

        private void buttonSign_Click(object sender, EventArgs e)
        {
            bool desettichno = izhod().Text.EndsWith(".");
            izhod().Text.TrimEnd('.');
            izhod().Text = (Double.Parse(izhod().Text) * -1).ToString();
            if (desettichno)
            {
                izhod().AppendText(".");
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(izhod().Text);
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string txt = Clipboard.GetText();
            double num;

            if (Double.TryParse(txt, out num))
            {
                izhod().Text = txt;
            }
        }

        private void buttonPercent_Click(object sender, EventArgs e)
        {
            string rslt = izhod().Text;
            rslt = double.Parse(txtrezultat.Text).ToString();
            double rslt2 = double.Parse(rslt);
            double x= Math.Sqrt(rslt2);

            txtrezultat.Text = x.ToString();

        }

        private void buttonMequals_Click(object sender, EventArgs e)
        {
            pamet = Double.Parse(txtrezultat.Text);
            buttonMR.Enabled = true;
            buttonMC.Enabled = true;
            mem_flg= true;
          
        }

        private void buttonMminus_Click(object sender, EventArgs e)
        {
            pamet -= Double.Parse(txtrezultat.Text);
        }

        private void buttonMR_Click(object sender, EventArgs e)
        {
            txtrezultat.Text = pamet.ToString();
             mem_flg = true;
        }

        private void buttonMC_Click(object sender, EventArgs e)
        {
            txtrezultat.Text = "0";
            pamet = 0;
            buttonMC.Enabled = false;
            buttonMR.Enabled = false;
        }

        private void buttonMplus_Click(object sender, EventArgs e)
        {
            pamet += Double.Parse(txtrezultat.Text);ToString();
        }
    }
}
