using System;
using System.Threading.Tasks;
using Octokit;

namespace GitWildcardIssues
{
    public class SelectRepo : IScene
    {
        private string _repoAuthor, _repoName;

        public string Description { get; } = "select repo to operate on with username and reponame or with id";
        public void Enter()
        {
            //IDE krzyczy errorem że result może nie być zainicjalizowany mimo pętl niżej które powinny tego dopilnować
            long result = -1;
            var input = Program.GetUserInput("Would you like to use username and reponame or repo id? name/id");
            while (input != "name" && input != "id")
                input = Program.GetUserInput();
            if (input == "name")
            {
                Task<Repository> task;
                do
                {
                    _repoAuthor = Program.GetUserInput("Input repo owners username");
                    _repoName = Program.GetUserInput("Input repo name");
                    task = Program.GitHubHandler.Client.Repository.Get(_repoAuthor, _repoName);
                    task.Wait();
                } while (!task.IsCompleted);

                result = task.Result.Id;
            }
            else
            {
                bool waitingForId = true;
                while (waitingForId)
                {
                    try
                    {
                        result = long.Parse(Program.GetUserInput("Input repo id"));
                    }
                    catch (FormatException)
                    {
                        continue;
                    }
                    waitingForId = false;
                }
            }
            Program.GitHubHandler.SelectedRepo = result;
        }
    }
}