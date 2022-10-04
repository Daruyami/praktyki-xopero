using System;
using GitLabApiClient.Models.Issues.Responses;

namespace GitWildcardIssues
{
    public static class GitLabIssue
    { 
        public static bool IsRepoSelected()
        {
            if (Program.GitLabHandler.SelectedProject == null)
            {
                Console.Out.WriteLine("First you need to select a project!");
                return true;
            }
            else return false;
        }
        
        public static void Display(Issue issue)
        {
            Console.Out.WriteLine("Issue: "+issue.Title+" (iid: "+issue.Iid+" )");
            Console.Out.WriteLine("Author: "+issue.Author.Name+" (id: "+issue.Author.Id+" )");
            Console.Out.WriteLine("Creation Date: "+issue.CreatedAt);
            Console.Out.WriteLine("State: "+issue.State);
            if(!String.IsNullOrEmpty(issue.Description))
                Console.Out.WriteLine("\nDescription: \n\t"+issue.Description);
            Console.Out.WriteLine("— — — — — — ");
        }
        
    }
}