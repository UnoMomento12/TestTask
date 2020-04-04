using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
namespace TestTask
{
    /// <summary>
    /// Class <c>StringTransform</c> is used for reading text from the file, transforming and writing it to the file,
    /// as stated in test task's specification.
    /// </summary>
    class StringTransform
    {
        #region Texts and paths to the file
        /// <summary>
        /// Text retrieved from the file.
        /// </summary>
        private string textFromFile;

        /// <summary>
        /// Text to write to the file.
        /// </summary>
        private string textToFile;

        /// <summary>
        /// Path to the file the text will be read from.
        /// </summary>
        private string filePathToRead;

        /// <summary>
        /// Path to the file the text will be written into.
        /// </summary>
        private string filePathToWrite;

        #endregion

        #region Arrays which are needed for text deconstruction
        private List<string> lines;
        /// <summary>
        /// An array of sentences in the text.
        /// </summary>
        private string[] sentences;
        /// <summary>
        /// An array of words in the third sentence.
        /// </summary>
        private string[] thirdSentence;
        /// <summary>
        /// An array of words in the whole text.
        /// </summary>
        private string[] wordsInText;
        #endregion

        /// <summary>
        /// Constructor <c>StringTransform</c> initializes object and sets base values.
        /// </summary>
        public StringTransform()
        {
            textFromFile = "";
            textToFile = "";
            filePathToRead = "";
            filePathToWrite = "";
            lines = new List<string>();
            sentences = null;
            thirdSentence = null;
            wordsInText = null;
        }

        /// <summary>
        /// Method <c>Execute</c> is used to execute a set of instructions on file.
        /// </summary>
        /// <param name="pathToRead">Path to the file the text will be read from.</param>
        /// <param name="pathToWrite">Path to the file the text will be saves to. </param>
        public void Execute( string pathToRead , string pathToWrite )
        {
            
            filePathToRead = pathToRead;
            filePathToWrite = pathToWrite;
            ReadLinesFromFile(lines);
            textFromFile = lines.Aggregate((i, j) => i + " " + j);

            TextToSentences();

            TextToSortedArray();

            //foreach (string a in sentences)
            //{
            //    Console.WriteLine(a);
            //    Console.WriteLine();
            //}

            Console.WriteLine("Quantity of sentences: " + sentences.Length);
            Console.WriteLine("Quaintity of words: " + wordsInText.Length);

            thirdSentence = sentences[2].Remove(sentences[2].Length-1).Split(' ');

            for (int i = thirdSentence.Length - 1; i>=0; i--)
            {
                Console.Write(thirdSentence[i]+" ");
            }

            //Console.WriteLine();
            //for( int i = 0; i< wordsInText.Length; i++)
            //{
            //    Console.Write(wordsInText[i] + " ");
            //}

            textToFile = wordsInText.Aggregate((i, j) => i + " " + j);
           
            WriteTextToFile(filePathToWrite);

        }

        /// <summary>
        /// Method <c>ReadLinesFromFile</c> is used to read lines from the given file while skipping empty lines.
        /// Warning: there is high probability that this code won't read file properly if run in .NET Core framework,
        /// because .NET Core Encoding class recognizes smaller number of encodings
        /// Link : https://docs.microsoft.com/en-us/dotnet/api/system.text.encodinginfo.getencoding?view=netframework-4.8
        /// </summary>
        /// <param name="lines"></param>
        private void ReadLinesFromFile(List<string> lines)
        {
            string currentLine = null;
            try
            {
                using (StreamReader sr = new StreamReader(filePathToRead, Encoding.Default))
                {
                    while ((currentLine = sr.ReadLine()) != null)
                    {
                        if (!String.IsNullOrEmpty(currentLine))
                            lines.Add(currentLine);
                    }
                }
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Method <c>WriteTextToFile</c> is used for writing transformed text to the file.
        /// </summary>
        /// <param name="writePath"></param>
        private void WriteTextToFile(string writePath)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(writePath, false, Encoding.Default))
                {
                        sw.WriteLine(textToFile);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Method <c>TextToTheSentences</c> is used for transforming text from file into an array of strings (sentences),
        /// By using Regex engine text is split in cases where sequence of characters ends with sentence ending symbol or also a Quotation mark, 
        /// has white space after that AND the first char after white space is in the upper case.
        /// </summary>
        private void TextToSentences()
        {
            sentences = Regex.Split(textFromFile, @"(?<=[\.!\?]|[\.!\?\”?])\s+(?=[A-Z])").Select(x => x.Trim()).ToArray();
        }

        /// <summary>
        /// Method <c>TextToWords</c> is used for transforming text into a sorted array of strings (words).
        /// Using Regex text engine every not needed character is removed from the text, except for white spaces and apostrophes.
        /// Then, text string is split on white spaces, removing empty strings and deleting white spaces around every word string.
        /// Finally, the resulting collection is sorted and cast to an array.
        /// </summary>
        private void TextToSortedArray()
        {
            char[] whitespace = new char[] { ' ', '\t' };
            wordsInText = Regex.Replace(textFromFile, "[^a-zA-Z0-9 ’]+", "").Split(whitespace, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).OrderBy(x => x).ToArray();
        }

    }

}
