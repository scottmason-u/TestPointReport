# Introduction 
This program will use your personal DevOps PAT security key (token).

If you need help on the token, or how to run the program type the name of this program: **"TestPointReport.exe /?"**
to get help.

# Getting Started
1. Run the program **TestPointReport.exe**, follow the prompts

1. The program will call several MS DevOps API's, first getting a list of Test Plans, then a list of Points for each Plan

1. Then it will create a *.csv or *.txt file

1. The file will be delimited by the pipe "|" character, so follow the instructions below to import into Excel.


The best way to load a file delimited by "|" into Excel is to use Excel's built-in functionality to import data from a text file. You can follow these steps:

# Open Excel first

1. Go to the "**Data**" tab on the Excel ribbon.

1. Click on "**Get Data**" or "From Text/CSV" depending on your Excel version.

1. Navigate to the location of your "|" delimited text file and select it.

1. Excel will guide you through the import process where you can specify "|" as the delimiter.

1. By default, double-clicking on a "*.txt" file may not automatically detect "|" as the delimiter and load it properly into Excel. Usually, Excel will attempt to use a comma (",") or tab ("\t") as the default delimiter when opening a text file. However, you can configure Excel to use "|" as the delimiter during the import process, as described in step 1 above.

1. You can now use or manipulate the data any way you  want

1. Then, if you wish, you can export the changed data to another *.csv file

