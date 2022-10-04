namespace GitWildcardIssues
{
    public class GitHubLogin : IScene
    {
        public string Description { get; } = "login to GitHub using a token";
        public void Enter()
        {
            var input = Program.GetUserInput("Input your private token: ");
            Program.GitHubHandler.Login(input);

            Program.SelectedService = 1;
        }
    }
}