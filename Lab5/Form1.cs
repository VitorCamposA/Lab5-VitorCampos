namespace Lab5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        /* Name: Vitor Campos
         * Date: November 2025
         * This program rolls one dice or calculates mark stats.
         * Link to your repo in GitHub: https://github.com/VitorCamposA/Lab5-VitorCampos
         * */

        //class-level random object
        Random rand = new Random();

        private void Form1_Load(object sender, EventArgs e)
        {
            radOneRoll.Checked = true;

            this.Text += " - Vitor Campos";

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearOneRoll();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ClearStats();
        }

        private void btnRollDice_Click(object sender, EventArgs e)
        {
            int dice1 = RollDice();
            int dice2 = RollDice();

            lblDice1.Text = dice1.ToString();
            lblDice2.Text = dice2.ToString();

            int total = dice1 + dice2;

            string rollName = GetName(total);
            lblRollName.Text = rollName;

        }

        private void radOneRoll_CheckedChanged(object sender, EventArgs e)
        {
            if (radOneRoll.Checked)
            {
                grpOneRoll.Visible = true;
                grpMarkStats.Visible = false;
                ClearOneRoll();
            }
            else
            {
                grpOneRoll.Visible = false;
                grpMarkStats.Visible = true;
                ClearOneRoll();
            }
        }

        /* Name: ClearOneRoll
        *  Sent: nothing
        *  Return: nothing
        *  Clear the labels */
        private void ClearOneRoll()
        {
            lblDice1.Text = "";
            lblDice2.Text = "";
            lblRollName.Text = "";
        }


        /* Name: ClearStats
        *  Sent: nothing
        *  Return: nothing
        *  Reset nud to minimum value, chkbox unselected, 
        *  clear labels and listbox */
        private void ClearStats()
        {
            nudNumber.Value = nudNumber.Minimum;
            chkSeed.Checked = false;

            lblAverage.Text = "";
            lblPass.Text = "";
            lblFail.Text = "";

            lstMarks.Items.Clear();
        }


        /* Name: RollDice
        * Sent: nothing
        * Return: integer (1-6)
        * Simulates rolling one dice */
        private int RollDice()
        {
            return rand.Next(1, 7);
        }


        /* Name: GetName
        * Sent: 1 integer (total of dice1 and dice2) 
        * Return: string (name associated with total) 
        * Finds the name of dice roll based on total.
        * Use a switch statement with one return only
        * Names: 2 = Snake Eyes
        *        3 = Litle Joe
        *        5 = Fever
        *        7 = Most Common
        *        9 = Center Field
        *        11 = Yo-leven
        *        12 = Boxcars
        * Anything else = No special name*/
        private string GetName(int total)
        {
            string name;

            switch (total)
            {
                case 2:
                    name = "Snake Eyes";
                    break;
                case 3:
                    name = "Little Joe";
                    break;
                case 5:
                    name = "Fever";
                    break;
                case 7:
                    name = "Most Common";
                    break;
                case 9:
                    name = "Center Field";
                    break;
                case 11:
                    name = "Yo-leven";
                    break;
                case 12:
                    name = "Boxcars";
                    break;
                default:
                    name = "No special name";
                    break;
            }

            return name;
        }

        private void btnSwapNumbers_Click(object sender, EventArgs e)
        {
            bool has1 = DataPresent(lblDice1.Text);
            bool has2 = DataPresent(lblDice2.Text);

            if (!has1 || !has2)
            {
                MessageBox.Show("Both numbers must be present.");
                return;
            }

            string a = lblDice1.Text;
            string b = lblDice2.Text;

            SwapData(ref a, ref b);

            lblDice1.Text = a;
            lblDice2.Text = b;
        }

        /* Name: DataPresent
        * Sent: string
        * Return: bool (true if data, false if not) 
        * See if string is empty or not*/
        private bool DataPresent(string value)
        {
            return value.Trim() != "";
        }


        /* Name: SwapData
        * Sent: 2 strings
        * Return: none 
        * Swaps the memory locations of two strings*/
        private void SwapData(ref string a, ref string b)
        {
            string temp = a;
            a = b;
            b = temp;
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            int size = (int) nudNumber.Value;
            int[] marks = new int[size];

            if (chkSeed.Checked)
                rand = new Random(1000);

            lstMarks.Items.Clear();

            int i = 0;
            while (i < size)
            {
                marks[i] = rand.Next(40, 101);
                lstMarks.Items.Add(marks[i]);
                i++;
            }

            int pass, fail;
            double avg = CalcStats(marks, out pass, out fail);

            lblAverage.Text = avg.ToString("F2");
            lblPass.Text = pass.ToString();
            lblFail.Text = fail.ToString();

        }

        private void chkSeed_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSeed.Checked)
            {
                DialogResult result = MessageBox.Show(
                    "Use seed value 1000?",
                    "Seed",
                    MessageBoxButtons.YesNo);

                if (result == DialogResult.No)
                {
                    chkSeed.Checked = false;
                }
            }
        }


        /* Name: CalcStats
        * Sent: array and 2 integers
        * Return: average (double) 
        * Run a foreach loop through the array.
        * Passmark is 60%
        * Calculate average and count how many marks pass and fail
        * The pass and fail values must also get returned for display*/
        private double CalcStats(int[] arr, out int pass, out int fail)
        {
            pass = 0;
            fail = 0;

            double sum = 0;

            foreach (int mark in arr)
            {
                sum += mark;

                if (mark >= 60)
                    pass++;
                else
                    fail++;
            }

            return sum / arr.Length;
        }
    }
}
