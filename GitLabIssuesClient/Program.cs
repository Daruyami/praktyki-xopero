using System;
using System.Linq;
using System.Threading.Tasks;
using GitLabApiClient;
using GitLabApiClient.Internal.Paths;
using GitLabApiClient.Models.Issues.Responses;
using GitLabApiClient.Models.Notes.Requests;

namespace GitLabIssuesClient
{
    internal static class Program
    {
        public static GitLabClient Client;
        public static ProjectId SelectedProject;
        public static int SelectedIssue;
        
        public static async Task Init()
        {
            string input = GetUserInput("Input gitlab url: ");
            while (!Uri.IsWellFormedUriString(input, UriKind.Absolute))
                input = GetUserInput("Bad url! Try again: ");
            string gitlabUri = input;
            
            input = GetUserInput("Would you like to use a private token or username and password? token/login");
            while (input != "token" && input != "login")
                input = GetUserInput();
            if (input == "token")
            {
                input = GetUserInput("Input your private token: ");
                Client = new GitLabClient(gitlabUri, input);
            }
            else
            {
                Client = new GitLabClient(gitlabUri);
                await Client.LoginAsync(GetUserInput("Please input username: "), GetUserInput("Please input password: "));
            }
        }

        public static void SelectRepo()
        {
            var input = GetUserInput("Please input the repo (project) id you want to operate on: ");
            SelectedProject = input;
        }

        public static void SelectIssue()
        {
            SelectedIssue = int.Parse(GetUserInput("Please input the issue Iid you want to operate on: "));
        }
        
        public static async Task GetIssues()
        {
            var issues = await Client.Issues.GetAllAsync(SelectedProject, null, options => options.State = IssueState.All);
            Console.Out.WriteLine("Issues: \n");
            foreach (var issue in issues.Reverse())
            {
                Console.Out.WriteLine("Issue: "+issue.Title+" (Iid: "+issue.Iid+" )");
                Console.Out.WriteLine("Author: "+issue.Author.Name);
                Console.Out.WriteLine("State: "+issue.State);
                Console.Out.WriteLine("\nDescription: \n\t"+issue.Description);
                Console.Out.WriteLine("— — — — — — ");
            }
        }

        public static async Task GetComments()
        {
            var issue = await Client.Issues.GetAsync(SelectedProject, SelectedIssue);
            Console.Out.WriteLine("— — — — — — ");
            Console.Out.WriteLine("Issue: "+issue.Title+" (Iid: "+issue.Iid+" )");
            Console.Out.WriteLine("Author: "+issue.Author.Name);
            Console.Out.WriteLine("State: "+issue.State);
            Console.Out.WriteLine("\nDescription: \n\t"+issue.Description);
            Console.Out.WriteLine("— — — — — — ");
            
            var comments = await Client.Issues.GetNotesAsync(SelectedProject, SelectedIssue);
            Console.Out.WriteLine("Comments: \n");
            foreach (var comment in comments.Reverse())
            {
                Console.Out.WriteLine("["+comment.CreatedAt+"] "+comment.Author.Name+": ");
                Console.Out.WriteLine("\t"+comment.Body);
                Console.Out.WriteLine("— — — — — — ");
            }
        }

        public static void SendComment()
        {
            string input = GetUserInput("Input your message: ");
            var task = Client.Issues.CreateNoteAsync(SelectedProject, SelectedIssue, new CreateIssueNoteRequest(input));
            task.Wait();
            var comment = task.Result;
            Console.Out.WriteLine("Sent a comment: \n");
            Console.Out.WriteLine("["+comment.CreatedAt+"] "+comment.Author.Name+": ");
            Console.Out.WriteLine("\t"+comment.Body);
            Console.Out.WriteLine("— — — — — — ");
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

        public static void DisplayHelp()
        {
            Console.Out.WriteLine("Available commands: ");
            Console.Out.WriteLine("?\t\tdisplays this message");
            Console.Out.WriteLine("select repo\tselects the repo to operate on");
            Console.Out.WriteLine("select issue\tselects the issue to operate on");
            Console.Out.WriteLine("issues\t\tdisplays issues in the selected repo");
            Console.Out.WriteLine("comments\tdisplays comments in the selected issue (correct repo also needs to be selected)");
            Console.Out.WriteLine("send\t\tsends comment to the selected issue (correct repo also needs to be selected)");
            Console.Out.WriteLine("exit\t\texits program");
        }
        
        public static bool InputCommands()
        {
            string input = GetUserInput("\nWaiting for user commands, to list available commands type: ?");
            Task task = null;
            try
            {
                switch (input)
                {
                    case "?":
                        DisplayHelp();
                        break;
                    case "select repo":
                        SelectRepo();
                        break;
                    case "select issue":
                        SelectIssue();
                        break;
                    case "issues":
                        task = GetIssues();
                        break;
                    case "comments":
                        task = GetComments();
                        break;
                    case "send":
                        SendComment();
                        break;
                    case "exit":
                        return false;
                    default:
                        DisplayHelp();
                        break;

                }
            }
            catch (GitLabException e)
            {
                Console.Out.WriteLine(e.Message);
            }

            if (task != null)
                task.Wait();
            return true;
        }

        public static void Main(string[] args)
        {
            var task = Init();
            task.Wait();
            while(InputCommands());
        }
    }
}