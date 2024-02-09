//Abdelkarim Jibril p888f995
//Operating Systems HW1 Using C#

using System;
using System.Diagnostics;

//Class for our shell commands
class ShellCommands
{
    //The main method
    static void Main()
    {
        while (true) // Loop indefinitely until user chooses to exit
        {
            //Tis will summon the menu options
            MenuDisplay();
            //Enter your choice command
            Console.WriteLine("Please enter the number of the command you would like to run(1-6): ");
            //Take in the number inputted
            string choice = Console.ReadLine();

            //If statement that will check if the inputted value is valid
            if (int.TryParse(choice, out int option))
            {
                //If it is valid we go to a switch statement that will take in the users choice
                switch (option)
                {
                    //First case executes the dir command
                    case 1:
                        ExecuteCommand("dir");
                        break;
                    //Second case executes the cd command
                    case 2:
                        ExecuteCommand("cd");
                        break;
                    //Third case executes the mkdir command. To make more sense of it I did this part by actually allowing the user to make a new file the replaces the old one
                    case 3:
                        //ExecuteCommand("mkdir"); I did not end up using the actual command here
                        
                        //Prompt the user to make a file name
                        Console.WriteLine("Enter the name of the new file:");
                        //Takes in the file name
                        string newFileName = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(newFileName))
                        {
                            try
                            {
                                //Creates a new file
                                System.IO.File.Create(newFileName);
                                Console.WriteLine($"File '{newFileName}' created successfully.");

                                //Here we will check if there's an old file to remove
                                if (System.IO.File.Exists("oldFile.txt"))
                                {
                                    //We remove the old file
                                    System.IO.File.Delete("oldFile.txt");
                                    Console.WriteLine("Old file 'oldFile.txt' removed.");
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error creating or deleting file: {ex.Message}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid file name.");
                        }
                        break;
                    //The fourth case we execute the echo command
                    case 4:
                        ExecuteCommand("echo");
                        break;
                    //In the fifth case we execute the type command
                    case 5:
                        Console.WriteLine("Enter the path of the file:");
                        string filePath = Console.ReadLine();
                        //We execute the type command to the new filepath (if it is valid)
                        if (!string.IsNullOrWhiteSpace(filePath))
                        {
                            ExecuteCommand($"type \"{filePath}\"");
                        }
                        else
                        {
                            Console.WriteLine("Invalid file path.");
                        }
                        break;
                    //Exit the program
                    case 6:
                        Console.WriteLine("Goodbye!");
                        return; // Exit the Main method, thus ending the program
                    //If the input is invalid we prompt the user to try again
                    default:
                        Console.WriteLine("Invalid choice!!! Please enter a number between 1 and 6. :D");
                        break;
                }
            }
            //This else statement is here if the first numver inoutted is invalid
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }
            Console.WriteLine();
        }
    }

    //This function simply gives the different options within the menu
    static void MenuDisplay()
    {
        Console.WriteLine("Menu:");
        Console.WriteLine("1. List directory contents");
        Console.WriteLine("2. Print directory");
        Console.WriteLine("3. Create a new directory");
        Console.WriteLine("4. Display a message");
        Console.WriteLine("5. Concatenate and display content of a file");
        Console.WriteLine("6. Exit");
    }

    //In this function we use the cmd
    static void ExecuteCommand(string command)
    {
        Console.Write($"Executing command: {command}...");
            //We create a new process. Process represents a system process
            Process process = new Process();
            //The command prompt
            process.StartInfo.FileName = "cmd.exe";
            //Thi will actually take the command we need to execute
            process.StartInfo.Arguments = $"/c {command}";
            //This captures the output of said command
            process.StartInfo.RedirectStandardOutput = true;
            //Starts process
            process.Start();

            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            //Display that it worked
            Console.WriteLine("Done");
            Console.WriteLine("Output:");
            Console.WriteLine(output);    
    }
}
