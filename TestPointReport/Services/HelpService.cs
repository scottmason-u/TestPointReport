namespace TestPointReport.Services
{
    public class HelpService
    {
        public static void DisplayHelp()
        {
            Console.WriteLine("");
            Console.WriteLine("Usage: TestPointReport <Path> <Filename>");
            Console.WriteLine("");
            Console.WriteLine("Example: programname C:\\Folder TestPointReport.csv");
            Console.WriteLine("");
            Console.WriteLine("This program requires a PAT token for authorization.");
            Console.WriteLine("");
            Console.WriteLine("First step is to get a valid PAT from Azure DevOps");
            Console.WriteLine("      PAT's expire after a certain number of days.");
            Console.WriteLine("");
            Console.WriteLine("Second Step: After you have a valid PAT you must store that PAT in an environmental variable");
            Console.WriteLine("             To store your PAT in an environmental variable on Windows do this: ");
            Console.WriteLine("In Windows type: setx PATTestPointReport YOUR_PAT_VALUE ");
            Console.WriteLine("");
            Console.WriteLine("             To store your PAT in an environmental variable on Linux do this: ");
            Console.WriteLine("");
            Console.WriteLine("In Linux do the following:");
            Console.WriteLine("");
            Console.WriteLine("Edit the .bashrc or .bash_profile file in your home directory using a text editor like nano or vi.For example:");
            Console.WriteLine("");
            Console.WriteLine("nano ~/.bashrc");
            Console.WriteLine("");
            Console.WriteLine("Type the following line at the end of the file:");
            Console.WriteLine("");
            Console.WriteLine("export PATTestPointReport=YOUR_PAT_VALUE");
            Console.WriteLine("");
            Console.WriteLine("Replace YOUR_PAT_VALUE with your actual PAT value.");
            Console.WriteLine("");
            Console.WriteLine("Save the file and exit the text editor.");
            Console.WriteLine("");
            Console.WriteLine("Run source ~/.bashrc to apply the changes to the current session.");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Options:");
            Console.WriteLine("  /?, /h, /help    Show help text");
        }
    }
}
