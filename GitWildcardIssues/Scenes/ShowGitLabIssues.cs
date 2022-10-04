using System;
using System.Linq;

namespace GitWildcardIssues
{
    public class ShowGitLabIssues : IScene
    {
        public string Description { get; } = "displays projects issues";
        public void Enter()
        {
            if (GitLabIssue.IsRepoSelected())
                return;
            var issues = Program.GitLabHandler.GetIssues();
            Console.Out.WriteLine("\n— — — — — — ");
            foreach (var issue in issues.Reverse())
            {
                GitLabIssue.Display(issue);
            }
        }
    }
}