using System;

namespace GitWildcardIssues
{
    public class GitLabLogin : IScene
    {
        public string Description { get; } = "login to GitLab using a token or with username and password";
        
        public void Enter()
        {
            string input = Program.GetUserInput("Input gitlab url: ");
            while (!Uri.IsWellFormedUriString(input, UriKind.Absolute))
                input = Program.GetUserInput("Bad url! Try again: ");
            string gitlabUri = input;
            
            input = Program.GetUserInput("Would you like to use a token or username and password? token/login");
            while (input != "token" && input != "login")
                input = Program.GetUserInput();
            if (input == "token")
            {
                input = Program.GetUserInput("Input your private token: ");
                Program.GitLabHandler.Login(gitlabUri, input);

            }
            else
            {
                Program.GitLabHandler.Login(gitlabUri,Program.GetUserInput("Please input username: "), Program.GetUserInput("Please input password: "));
            }

            Program.SelectedService = 2;
        }
    }
}