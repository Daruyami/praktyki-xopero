using System;
using System.Collections.Generic;

namespace GitWildcardIssues
{
    internal static class Program
    {
        public static string AppName = "GitWildcardIssues";
        
        public static GitLabHandler GitLabHandler = new GitLabHandler();
        public static GitHubHandler GitHubHandler = new GitHubHandler();

        public static byte SelectedService = 0;
        /*
         *  0 = nic
         *  1 = github
         *  2 = gitlab
         *  ?
         */

        public static Help Help = new Help();
        public static Exit Exit = new Exit("returns to service selection menu");
        public static Dictionary<string, IScene>[] Cmds = new[]
        {
            new Dictionary<string, IScene>()
            {
                { "?",  Help },
                { "login github", new GitHubLogin() },
                { "login gitlab", new GitLabLogin() },
                
                { "exit", new Exit() }
            },
            new Dictionary<string, IScene>()
            {
                { "?", Help },
                { "repo", new SelectRepo() },
                { "issues", new ShowGitHubIssues() },
                { "create", new CreateGitHubIssue() },
                { "modify", new ModifyGitHubIssue() },
                
                { "exit", Exit }
            },
            new Dictionary<string, IScene>()
            {
                { "?", Help },
                { "project", new SelectProject() },
                { "issues", new ShowGitLabIssues() },
                { "create", new CreateGitLabIssue() },
                { "modify", new ModifyGitLabIssue() },
                
                { "exit", Exit }
            }
        };

        public static string GetUserInput(string message=null)
        {
            if (message != null)
                Console.Out.WriteLine(message);
            var savedColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Out.Write("> ");
            Console.ForegroundColor = savedColor;
            string input;
            do
                input = Console.ReadLine();
            while (string.IsNullOrEmpty(input));
            return input;
        }

        public static bool InputCommands()
        {
            string input = GetUserInput("\nWaiting for user commands, to list available commands type: ?");
            if (input == "exit")
            {
                if (SelectedService !=0)
                    SelectedService = 0;
                else
                    return false;
            }
            bool foundKey = false;
            foreach (var key in Cmds[SelectedService].Keys)
                if (input == key)
                {
                    foundKey = true;
                    Cmds[SelectedService][key].Enter();
                }
            if (!foundKey)
                Console.Out.WriteLine("Unknown command, to list available commands type: ?");
            
            return true;
        }
        
        public static void Main(string[] args)
        {
            while (InputCommands());
        }
    }
}