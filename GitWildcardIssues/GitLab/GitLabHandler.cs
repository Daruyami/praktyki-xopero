using System.Collections.Generic;
using GitLabApiClient;
using GitLabApiClient.Internal.Paths;
using GitLabApiClient.Models.Issues.Requests;
using GitLabApiClient.Models.Issues.Responses;

namespace GitWildcardIssues
{
    public class GitLabHandler
    {
        public GitLabClient Client;
        public ProjectId SelectedProject = null;

        public void Login(string uri, string token)
        {
            Client = new GitLabClient(uri, token);
        }

        public void Login(string uri, string login, string password)
        {
            Client = new GitLabClient(uri);
            Client.LoginAsync(login, password).Wait();
        }

        public IList<Issue> GetIssues()
        {
            //brak supportu dla stron, na repo mają dwuletniego otwartego pra z dodanym wsparciem stron
            return Client.Issues.GetAllAsync(SelectedProject, null, 
                options => options.State = IssueState.All).Result;
        }

        public Issue CreateIssue(string title, string description)
        {
            var response = Client.Issues.CreateAsync(SelectedProject,
                new CreateIssueRequest(title) { Description = description });
            return response.Result;
        }

        public Issue ModifyIssue(int iid, string title=null, string description=null)
        {
            UpdateIssueRequest editedIssue = new UpdateIssueRequest();
            if (title != null)
                editedIssue.Title = title;
            if (description != null)
                editedIssue.Description = description;
            var response = Client.Issues.UpdateAsync(SelectedProject, iid, editedIssue);
            return response.Result;
        }
        
    }
}