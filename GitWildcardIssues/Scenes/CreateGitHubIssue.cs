namespace GitWildcardIssues
{
    public class CreateGitHubIssue : IScene
    {
        private string _title, _body;
        
        public string Description { get; } = "create a new issue in selected project";
        public void Enter()
        {
            if (GitHubIssue.IsRepoSelected())
                return;
            _title = Program.GetUserInput("Input issues title: ");
            _body = Program.GetUserInput("Input issues body");
            var createdIssue = Program.GitHubHandler.CreateIssue(_title, _body);
            GitHubIssue.Display(createdIssue);
        }
    }
}