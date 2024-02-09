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


        // Register a new person if validation passes and the person is not already registered.
        private void BtnRegister_Click(object sender, EventArgs e)
        {
            string name = BoxName.Text;
            string lastname = BoxLastname.Text;
            string personnr = BoxPersonnr.Text;

            try     // check the validation
            {
                Validations.ValidateData(name, lastname, personnr);             // Raise an exception if something goes wrong with validation.
                bool control = register.CheckPersonnr(personnr);

                if (control)
                {
                    bool repeatedPerson = register.CheckRepeatedPerson(personnr);

                    if (repeatedPerson is false)
                    {
                        register.AddPerson(name, lastname, personnr);
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



        // Close the program when "AVSLUTA" is pressed.
        private void MenuClose_Click(object sender, EventArgs e)
        {
            Close();
        }


        //---------------------------------------------------------------------------------------------------
        // Displays a MessageBox if the data does not pass validation.
        private void ShowWarningMsg(string message)
        {
            string title = "Något gick fel!";

            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }


        // Prints the registered person's first name and last name from the list/file.
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
