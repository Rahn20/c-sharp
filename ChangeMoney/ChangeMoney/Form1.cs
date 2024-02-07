using ChangeMoney.src;

namespace ChangeMoney
{
    public partial class Form1 : Form
    {
        private decimal savePrice;   // The price the user has to pay.
        private decimal savePaid;   // The amount the user pays with.


        public Form1()
        {
            InitializeComponent();
        }


        // Store the value of the price.
        private void NrPrice_ValueChanged(object sender, EventArgs e)
        {
            savePrice = NrPrice.Value;
        }

        // Store the value of the paid amount.
        private void NrPaid_ValueChanged(object sender, EventArgs e)
        {
            savePaid = NrPaid.Value;
        }

        // When "KOR" is clicked, call GetChangeResult method and display the result in a label on the screen.
        private void BtnRun_Click(object sender, EventArgs e)
        {

            try     // Check if the paid amount is less than the price.
            {
                ChangeMoneySystem changeMoneySystem = new ChangeMoneySystem((int)savePrice, (int)savePaid);
                var result = changeMoneySystem.GetChangeResult();

                string printResult = BtnRunPrint(result);
                LblFinalResult.Text = printResult;
            }
            catch
            {
                ShowWarningMsg();
                return;
            }
        }

        // Close the program when the user clicks "AVSLUTA."
        private void BtnEnd_Click(object sender, EventArgs e)
        {
            Close();
        }



        //-------------------------------------------------------------------------------
        // Display a MessageBox if the user's payment is less than the price.
        private static void ShowWarningMsg()
        {
            string message = "Priset bör vara mindre än betalda beloppet, försök igen!";
            string title = "Något gick fel!";

            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }


        // Loop through the result (values and change), concatenate them into a string, and return it.
        private static string BtnRunPrint(Dictionary<string, int> result)
        {
            string output = "";

            foreach (KeyValuePair<string, int> kv in result)
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
