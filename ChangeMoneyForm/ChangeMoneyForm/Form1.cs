using ChangeMoneyForm.src;
using System;
using System.Collections.Generic;
using System.Windows.Forms;



namespace ChangeMoneyForm
{
    public partial class Form1 : Form
    {
        // saves the price that the user has to pay
        private decimal savePrice;

        //saves the amount the user pays with
        private decimal savePaid;

        public Form1()
        {
            InitializeComponent();
        }


        // set the value of price to a private variable
        private void NrPrice_ValueChanged(object sender, EventArgs e)
        {
            savePrice = NrPrice.Value;
        }

        // set the value of the paid amount to a private variable
        private void NrPaid_ValueChanged(object sender, EventArgs e)
        {
            savePaid = NrPaid.Value;
        }

        // when KÖR is clicked, the GetChangeResult method is called and the result is printed on the screen (in a label)
        private void BtnRun_Click(object sender, EventArgs e)
        {

            try     // check if the paid is less than the price.
            {
                ChangeMoney changeMoney = new ChangeMoney((int)savePrice, (int)savePaid);
                var res = changeMoney.GetChangeResult();

                string printResult = BtnRunPrint(res);
                LblFinalResult.Text = printResult;
            }
            catch
            {
                ShowWarningMsg();
                return;
            }
        }

        // Close the program when user click AVSLUTA
        private void BtnEnd_Click(object sender, EventArgs e)
        {
            this.Close();
        }




        //-------------------------------------------------------------------------------
        // displays a messagebox if the user's paid is less than the price
        private void ShowWarningMsg()
        {
            string message = "Priset bör vara mindre än betalda beloppet, försök igen!";
            string title = "Något gick fel!";

            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }


        // loop the result (values and change), put the result in a string and return it.
        private static string BtnRunPrint(Dictionary<string, int> res)
        {
            string output = "";

            foreach (KeyValuePair<string, int> kv in res)
            {
                char getLastChar = kv.Key[kv.Key.Length - 1];

                if (kv.Value > 1 && getLastChar == 'p')
                {
                    output += kv.Value.ToString() + " " + kv.Key + "ar" + "\n";

                }
                else if (kv.Value > 1 && getLastChar == 'a')
                {
                    string singleToPlural = kv.Key.Substring(0, kv.Key.Length - 1) + "or";
                    output += kv.Value.ToString() + " " + singleToPlural + "\n";
                }
                else if (kv.Value == 1)
                {
                    output += kv.Value.ToString() + " " + kv.Key + "\n";
                }
            }

            return output;
        }
    }
}