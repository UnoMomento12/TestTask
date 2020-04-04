using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
namespace TestTask
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Default file names
            // Default file paths to be used when entry string is empty
            string dFileName = "02_textSample.txt";
            string dResultFileName = "ResultOf_" + dFileName;
            #endregion
            // Menu begins
            Console.WriteLine("Enter file name in the same folder as executable file;");
            Console.WriteLine("Or write full path including file name (Or drag and drop file on the console window).");
            Console.WriteLine("If text entry is empty, default file name will be used: 02_textSample.txt");

            string entryText;  // Text path retrieved from console.
            string filePathToRead; // Path to the file that will be read.
            string filePathToWrite; // Path to the file where sorted words will be saved.
            FileInfo fileInfo; // Used to check file existence.
           
            while (true)
            {
                entryText = Console.ReadLine();

                if (String.IsNullOrEmpty(entryText))
                {
                    filePathToRead = dFileName;
                    filePathToWrite = dResultFileName;
                    break;
                }

                Console.WriteLine(entryText);

                fileInfo = new FileInfo(entryText);

                if (fileInfo.Exists)
                {
                    filePathToRead = entryText;
                    filePathToWrite = "ResultOf_" + fileInfo.Name;
                    break;
                } else
                {
                    Console.WriteLine("File doesn't exist or path to file is wrong, try again.");
                }
            }
            
            StringTransform execOb = new StringTransform();

            execOb.Execute(filePathToRead, filePathToWrite);

            Console.ReadLine();
        }
    }
}
