namespace GitWildcardIssues
{
    public class SelectProject : IScene
    {
        public string Description { get; } = "select the project to operate on with id";
        public void Enter()
        {
            var input = Program.GetUserInput("Input projects id");
            Program.GitLabHandler.SelectedProject = int.Parse(input);
        }
    }
}