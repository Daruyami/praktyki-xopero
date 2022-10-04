using System;
using Octokit;

namespace GitWildcardIssues
{
    public static class GitHubIssue
    {
        public static bool IsRepoSelected()
        {
            if (Program.GitHubHandler.SelectedRepo == -1)
            {
                Console.Out.WriteLine("First you need to select a repo!");
                return true;
            }
            else return false;
        }
        
        public static void Display(Issue issue)
        {
            Console.Out.WriteLine("Issue: "+issue.Title+" (number: "+issue.Number+" )");
            //z jakiegoś powodu issue.User.Name po prostu nie działa????
            if(!String.IsNullOrEmpty(issue.User.Name))
                Console.Out.WriteLine("Author: "+issue.User.Name+" (id: "+issue.User.Id+" )");
            else
                Console.Out.WriteLine("Authors id: "+issue.User.Id+" (couldnt fetch name!)");
            Console.Out.WriteLine("Creation Date: "+issue.CreatedAt);
            Console.Out.WriteLine("State: "+issue.State);
            if(!String.IsNullOrEmpty(issue.Body))
                Console.Out.WriteLine("\nDescription: \n\t"+issue.Body);
            Console.Out.WriteLine("— — — — — — ");
        }
    }
}