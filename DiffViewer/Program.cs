using System;
using System.IO;
using DiffPlex.DiffBuilder;
using DiffPlex.DiffBuilder.Model;

namespace DiffViewer
{
    internal static class Program
    {

        public static string OriginalFile;
        public static string EditedFile;

        private static void ShowDiff()
        {
            var diff = InlineDiffBuilder.Diff(OriginalFile, EditedFile);

            var savedColor = Console.ForegroundColor;
            foreach (var line in diff.Lines)
            {
                switch (line.Type)
                {
                    //typ .Modified jest tylko przy side by side dfferze :(
                    case ChangeType.Inserted:
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("+ ");
                        break;
                    case ChangeType.Deleted:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("- ");
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write("  ");
                        break;
                }

                Console.WriteLine(line.Text);
            }
            Console.ForegroundColor = savedColor;
        }

        public static string GetUserInput(string message=null)
        {
            if (message != null)
                Console.Out.WriteLine(message);
            string input;
            do
                input = Console.ReadLine();
            while (string.IsNullOrEmpty(input));
            return input;
        }
        
        public static void Main(string[] args)
        {
            string originalPath = GetUserInput("Input path to the original file: ");
            while(!File.Exists(originalPath))
            {
                Console.Out.WriteLine("File with such path not found! Try again: ");
            }
            OriginalFile = File.ReadAllText(originalPath);
            string editedPath = GetUserInput("Input path to the revised file: ");
            while(!File.Exists(editedPath))
            {
                Console.Out.WriteLine("File with such path not found! Try again: ");
            }
            EditedFile = File.ReadAllText((editedPath));
            
            ShowDiff();
        }
    }
}