using System;

namespace GitWildcardIssues
{
    public class ModifyGitLabIssue : IScene
    {
        private int _iid;
        private string _title, _description;
        public string Description { get; } = "modify an existing issue";
        public void Enter()
        {
            if (GitLabIssue.IsRepoSelected())
                return;
            bool waitingForIid = true;
            do
            {
                try
                {
                    _iid = int.Parse(Program.GetUserInput("Input issues id: "));
                }
                catch (FormatException)
                {
                    Console.Out.WriteLine("Wrong type of input!");
                    continue;
                }
                waitingForIid = false;
            } while (waitingForIid);
            
            _title = null;
            _description = null;
            bool edit = true;
            do
            {
                string input = Program.GetUserInput("What do you want to modify? \ntitle/description / save/cancel");
                switch (input)
                {
                    case "title":
                        _title = Program.GetUserInput("Input issues new title: ");
                        break;
                    case "description":
                        _description = Program.GetUserInput("Input issues new description: ");
                        break;
                    case "save":
                        edit = false;
                        break;
                    case "cancel":
                        Console.Out.WriteLine("Returning, issue not modified!");
                        return;
                }
            } while (edit) ;

            var modifiedIssue = Program.GitLabHandler.ModifyIssue(_iid, _title, _description);
            GitLabIssue.Display(modifiedIssue);
        }
    }
}