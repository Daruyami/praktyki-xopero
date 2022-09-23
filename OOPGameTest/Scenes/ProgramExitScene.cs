using System;

namespace OOPGameTest
{
    public class ProgramExitScene : IScene
    {
        private string _name = "Exit";
        public string Name => _name;
        public void Enter()
        {
            Console.Out.WriteLine("bye bye !");
            Environment.Exit(0);
        }
    }
}