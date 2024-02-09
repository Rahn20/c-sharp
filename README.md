# Programming with C#

This repository contains simple programs implemented in C#


## RegisterSeller 

This console application, utilizing the .NET Framework, reads data for a specified number of sellers in Salesforce. The user is prompted to enter the number of sellers to register, along with each seller's name, personnumber, district of work, and the number of items sold during the relevant period. Subsequently, the data is written to a file that the program can later read. The sellers are classified into four levels based on the number of items they have sold during the specified period.

- level 1: under 50 items
- level 2: 50-99 items
- level 3: 100-199 items
- level 4: over 199 items

![RegisterSeller Console Application](RegisterSellerConsole/doc/terminal.jpg)


## ChangeMoney

This Windows Forms application, utilizing .NET 8, calculates the amount of change a customer should receive after a shopping transaction. The program displays both the amount of change and the specific denominations of banknotes (500-, 200-, 100-, 50-, and 20-kronor) and coins (10-, 5-, and 1-kronor) that the customer should receive

![ChangeMoney Windows Forms Application](ChangeMoney/program.png)


## PersonRegistration

This Windows Forms application, utilizing the .NET Framework, is designed for registering individuals. The user inputs the person's first name, last name, and personnumber. A validator checks the entered data to ensure that the person is not already registered. If the data passes validation, the person is successfully registered. Registered individuals are displayed on the form under the ***Registrerade personer*** section. The person's data is also saved to a file, which the program can later read..

<img src="PersonRegistration/forms.jpg" width="60%" height="60%">


## MediaLibrary 

This Windows Forms program, utilizing the .NET Framework, is designed to register a collection of media items, including books (with title and page numbers), soundtracks (with title and playing time), and movies (with title, playing time, and resolution). The program can store media data in a file as well as retrieve media data from that file.

<img src="MediaLibrary/medialibrary.jpg" width="60%" height="60%">