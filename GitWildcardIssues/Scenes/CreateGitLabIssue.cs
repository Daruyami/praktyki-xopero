namespace GitWildcardIssues
{
    public class CreateGitLabIssue : IScene
    {
        private string _title, _description;
        
        public string Description { get; } = "create a new issue in selected project";
        public void Enter()
        {
            if (GitLabIssue.IsRepoSelected())
                return;
            _title = Program.GetUserInput("Input issues title: ");
            _description = Program.GetUserInput("Input issues description");
            var createdIssue = Program.GitLabHandler.CreateIssue(_title, _description);
            GitLabIssue.Display(createdIssue);
        }
    }
}