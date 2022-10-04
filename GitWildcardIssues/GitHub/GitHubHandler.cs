using System.Collections.Generic;
using Octokit;

namespace GitWildcardIssues
{
    public class GitHubHandler
    {
        public GitHubClient Client;
        public long SelectedRepo = -1;

        private void Init()
        {
            Client = new GitHubClient(new ProductHeaderValue(Program.AppName));
        }
        
        public void Login(string token)
        {
            Init();
            var tokenAuth = new Credentials(token);
            Client.Credentials = tokenAuth;
        }

        public IReadOnlyList<Issue> GetIssues(int page)
        {
            return Client.Issue.GetAllForRepository(SelectedRepo,
                new RepositoryIssueRequest() { State = ItemStateFilter.All }, new ApiOptions(){PageCount = 1, PageSize = 10, StartPage = page}).Result;
        }
        
        public Issue CreateIssue(string title, string description)
        {
            var response = Client.Issue.Create(SelectedRepo, new NewIssue(title) { Body = description });
            return response.Result;
        }

        public Issue ModifyIssue(int issueNo, string title=null, string body=null)
        {
            IssueUpdate editedIssue = new IssueUpdate();
            if (title != null)
                editedIssue.Title = title;
            if (body != null)
                editedIssue.Body = body;
            var response = Client.Issue.Update(SelectedRepo, issueNo, editedIssue);
            return response.Result;
        }
    }
}