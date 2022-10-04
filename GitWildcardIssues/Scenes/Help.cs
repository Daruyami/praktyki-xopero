using System;

namespace GitWildcardIssues
{
    public class Help : IScene
    {
        public string Description { get; } = "display this message";
        public void Enter()
        {
            Console.Out.WriteLine("Available commands: ");
            foreach (var key in Program.Cmds[Program.SelectedService].Keys)
                Console.Out.WriteLine(key + "\t" + Program.Cmds[Program.SelectedService][key].Description);
        }
    }
}