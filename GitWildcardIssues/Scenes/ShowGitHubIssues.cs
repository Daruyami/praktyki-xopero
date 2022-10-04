using System;
using System.Linq;

namespace GitWildcardIssues
{
    public class ShowGitHubIssues : IScene
    {
        public string Description { get; } = "displays repo issues grouped in 10s as pages";
        public void Enter()
        {
            if (GitHubIssue.IsRepoSelected())
                return;
            var input = Program.GetUserInput("Input page: ");
            int page = int.Parse(input);
            var issues = Program.GitHubHandler.GetIssues(page);
            Console.Out.WriteLine("\n— — — — — — ");
            foreach (var issue in issues.Reverse())
            {
                GitHubIssue.Display(issue);
            }
        }
    }
}