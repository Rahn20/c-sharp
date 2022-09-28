using System;
using System.Windows.Forms;
using PersonRegistration.src;

namespace PersonRegistration
{
    public partial class Form1 : Form
    {
        RegisterPerson register = new RegisterPerson();

        public Form1()
        {
            InitializeComponent();

            register.AddToList();
            PrintData();
        }

        // reads the inputdata, validates it, checks the personnumber, registers the person if the data goes through validation and
        // if the person is not already registered and then reads the data and prints it in the "Registered persons box".
        private void BtnRegister_Click(object sender, EventArgs e)
        {
            string name = BoxName.Text;
            string lastname = BoxLastname.Text;
            string personnr = BoxPersonnr.Text;


            try // check the validation
            {
                Validations.ValidateData(name, lastname, personnr);             // raises exception if something goes wrong with validation
                bool control = register.CheckPersonnr(personnr);               // is person number correct ??

                if (control)
                {
                    bool repeatedPerson = register.CheckRepeatedPerson(personnr);   // is the person already registered ??

                    if (repeatedPerson is false)
                    {
                        register.AddPerson(name, lastname, personnr);       // register person
                        string gender = register.GetGender(personnr);
                        PrintData();

                        LblResult.Text = $"Förnamn: {name}\nEfternamn: {lastname}\nPersonnummer: {personnr}\nKön: {gender}";
                    }
                    else
                    {
                        LblResult.Text = $"Personen {personnr} finns redan registrerad!";
                    }
                }
                else
                {
                    LblResult.Text = "Personnummer är felaktigt. Försök igen!";
                }

            }
            catch (FormatException)
            {
                ShowWarningMsg("Ogiltig personnummer, Se till att personnummer innehåller 10 siffror, skrivs i formaten yymmdd-1111 och försök igen!");
            }
            catch (ValidatorsException ve)
            {
                ShowWarningMsg(ve.Message);
            }
        }



        // closes the program when AVSLUTA is pressed.
        private void MenuClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        //---------------------------------------------------------------------------------------------------
        // displays a message box if the data does not pass validation.
        private void ShowWarningMsg(string message)
        {
            string title = "Något gick fel!";

            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }


        // prints the person's first name and last name that are registered in the list/file.
        private void PrintData()
        {
            string res = "";

            foreach (Person person in register.Persons)
            {
                res += $"{person.Name} {person.Lastname}\r\n";
            }

            TxtBoxPersons.Text = res;
        }
    }
}
